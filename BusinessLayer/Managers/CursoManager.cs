using AutoMapper;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using Utilidades.DTOs.Componente;
using Utilidades.DTOs.Curso;
using Utilidades.DTOs.EntregaTarea;
using Utilidades.DTOs.SeccionCurso;
using Utilidades.DTOs.Usuario;

namespace BusinessLayer
{
    public class CursoManager : ICursoManager
    {
        private readonly IMapper _mapper;
        private readonly MyContext _context;
        public CursoManager(IMapper mapper, MyContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public ApiResponse<List<GetCursoDTO>> lists()
        {
            ApiResponse<List<GetCursoDTO>> response = new ApiResponse<List<GetCursoDTO>>();

            try
            {
                response.Data = _context.Cursos
                    .Include(c => c.CursosDocentes).ThenInclude(cd => cd.Usuario)
                    .Include(c => c.UsuariosCursos).ThenInclude(uc => uc.Usuario)
                    
                    .Select(c => _mapper.Map<GetCursoDTO>(c)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 204;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<List<GetCursoDTO>>> add(AddCursoDTO curso)
        {
            ApiResponse<List<GetCursoDTO>> response = new ApiResponse<List<GetCursoDTO>>();
            try
            {
                _context.Cursos.Add(_mapper.Map<Curso>(curso));
                await _context.SaveChangesAsync();
                response.Data = _context.Cursos.Select(c => _mapper.Map<GetCursoDTO>(c)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }
            return response;
        }
        public async Task<ApiResponse<List<GetCursoDTO>>> delete(int id)
        {
            ApiResponse<List<GetCursoDTO>> response = new ApiResponse<List<GetCursoDTO>>();
            try
            {
                Curso curso = _context.Cursos.First(c => c.Id == id);
                _context.Cursos.Remove(curso);
                await _context.SaveChangesAsync();
                response.Data = _context.Cursos.Select(c => _mapper.Map<GetCursoDTO>(c)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<GetCursoDTO>> get(int id)
        {
            ApiResponse<GetCursoDTO> response = new ApiResponse<GetCursoDTO>();
            try
            {
                response.Data = _mapper.Map<GetCursoDTO>(
                    await _context.Cursos
                    .Include(c => c.CursosDocentes).ThenInclude(cd => cd.Usuario)
                    .Include(c => c.UsuariosCursos).ThenInclude(uc => uc.Usuario)
                    .Include(c => c.SeccionesCurso).ThenInclude(sc => sc.Componentes).ThenInclude(co => co.Comunicado)
                    .Include(c => c.SeccionesCurso).ThenInclude(sc => sc.Componentes).ThenInclude(co => co.Archivo)
                    .Include(c => c.SeccionesCurso).ThenInclude(sc => sc.Componentes).ThenInclude(co => co.Encuesta)
                    .Include(c => c.SeccionesCurso).ThenInclude(sc => sc.Componentes).ThenInclude(co => co.ContenedorTarea)
                    .FirstAsync(c => c.Id == id)
                );
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }
            return response;
        }
        public async Task<ApiResponse<GetCursoDTO>> edit(int id, AddCursoDTO curso)
        {
            ApiResponse<GetCursoDTO> response = new ApiResponse<GetCursoDTO>();
            try
            {
                Curso cursoUpdate = _context.Cursos.SingleOrDefault(c => c.Id == id);
                cursoUpdate.Nombre = curso.Nombre;
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCursoDTO>(cursoUpdate);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }

            return response;
        }

        public ApiResponse<bool> matricularse(DTMatricula matricula)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            GetCursoDTO curso = _mapper.Map<GetCursoDTO>(_context.Cursos.FirstOrDefaultAsync(c => c.Id == matricula.IdCurso));

            if (curso.ConfirmaBedelia)
            {
                IBedeliasApi _bedeliasApi = new BedeliasApi();
                response.Data = _bedeliasApi.MatricularseACurso(matricula);
            }
            else
                response.Data = true;

            return response;
        }

        public async Task<ApiResponse<GetCursoDTO>> addDocente(int id, AddUsuarioDTO user)
        {
            ApiResponse<GetCursoDTO> response = new ApiResponse<GetCursoDTO>();
            try
            {
                CursoDocente cd = new CursoDocente();
                cd.CursoId = id;
                cd.FacultadId = user.FacultadId;
                cd.UsuarioId = user.Cedula;
                _context.CursoDocente.Add(cd);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCursoDTO>(_context.Cursos.SingleOrDefault(c => c.Id == id));
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ApiResponse<AddSeccionCursoDTO>> addSeccion(AddSeccionCursoDTO seccion)
        {
            ApiResponse<AddSeccionCursoDTO> response = new ApiResponse<AddSeccionCursoDTO>();
            try
            {
                SeccionCurso sc = new SeccionCurso();
                sc.CursoId = seccion.CursoId;
                sc.Indice = seccion.Indice;
                sc.Titulo = seccion.Titulo;

                _context.SeccionesCursos.Add(sc);
                await _context.SaveChangesAsync();
                response.Data = seccion;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ApiResponse<AddComponenteDTO>> addComponente(AddComponenteDTO componente, IFormFile archivo)
        {
            ApiResponse<AddComponenteDTO> response = new ApiResponse<AddComponenteDTO>();
            try
            {
                Componente c = new Componente();

                c.Indice = componente.Indice;
                c.Nombre = componente.Nombre;
                c.SeccionCursoId = componente.SeccionCursoId;
                c.Tipo = componente.Tipo;
                _context.Componentes.Add(c);
                await _context.SaveChangesAsync();

                int idComponente = c.Id;

                if (c.Tipo.Equals("Archivo"))
                {
                    Archivo a = new Archivo();
                    
                    a.ComponenteId = idComponente;
                    a.Extension = Path.GetExtension(archivo.FileName).Substring(1);
                    a.Nombre = Path.GetFileNameWithoutExtension(archivo.FileName);

                    _context.UploadS3(archivo, "ComponenteArchivo", a.Nombre + a.Extension );
                    a.Ubicacion = "https://dotnet-storage.s3.amazonaws.com/ComponenteArchivo/" + a.Nombre + a.Extension;

                    _context.Archivos.Add(a);
                   
                }
                else if (c.Tipo.Equals("Comunicado"))
                {
                    Comunicado comunicado = new Comunicado();

                    comunicado.ComponenteId = idComponente;
                    comunicado.Descripcion = componente.Comunicado.Descripcion;
                    comunicado.Titulo = componente.Comunicado.Titulo;

                    _context.Comunicados.Add(comunicado);
                   
                }else if (c.Tipo.Equals("Encuesta"))
                {
                    EncuestaCurso encuesta = new EncuestaCurso();

                    encuesta.ComponenteId = idComponente;
                    encuesta.Fecha = componente.Encuesta.Fecha;
                    encuesta.IdCurso = componente.Encuesta.IdCurso;
                    encuesta.IdEncuesta = componente.Encuesta.IdEncuesta;

                    _context.EncuestaCursos.Add(encuesta);
                }else if (c.Tipo.Equals("ContenedorTarea"))
                {
                    ContenedorTarea contenedorTarea = new ContenedorTarea();

                    contenedorTarea.ComponenteId = idComponente;
                    contenedorTarea.FechaCierre = componente.ContenedorTarea.FechaCierre;
                }

                await _context.SaveChangesAsync();
                response.Data = componente;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ApiResponse<AddEntregaTareaDTO>> addEntregaTarea(AddEntregaTareaDTO entregaTarea, IFormFile archivoEntrega)
        {
            ApiResponse<AddEntregaTareaDTO> response = new ApiResponse<AddEntregaTareaDTO>();
            try
            {
                EntregaTarea entrega = new EntregaTarea();

                entrega.Calificacion = 0;
                entrega.ContenedorTareaId = entregaTarea.ContenedorTareaId;
                entrega.UsuarioId = entregaTarea.UsuarioId;
                entrega.FacultadId = entregaTarea.FacultadId;
                entrega.FechaEntrega = entregaTarea.FechaEntrega;

                _context.EntregasTarea.Add(entrega);
                await _context.SaveChangesAsync();

                int idEntregaTarea = entrega.Id;

                Archivo a = new Archivo();

                a.EntregaTareaId = idEntregaTarea;
                a.Extension = Path.GetExtension(archivoEntrega.FileName).Substring(1);
                a.Nombre = Path.GetFileNameWithoutExtension(archivoEntrega.FileName);

                _context.UploadS3(archivoEntrega, "EntregasTarea", a.Nombre + a.Extension);
                a.Ubicacion = "https://dotnet-storage.s3.amazonaws.com/EntregasTarea/" + a.Nombre + a.Extension;

                _context.Archivos.Add(a);

                await _context.SaveChangesAsync();
                response.Data = entregaTarea;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }

            return response;
        }
    }
}
