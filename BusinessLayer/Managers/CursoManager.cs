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

        public async Task<ApiResponse<bool>> matricularse(DTMatricula matricula)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                GetCursoDTO curso = _mapper.Map<GetCursoDTO>(_context.Cursos.FirstOrDefaultAsync(c => c.Id == matricula.IdCurso));

                if (curso.ConfirmaBedelia)
                {
                    IBedeliasApi _bedeliasApi = new BedeliasApi();
                    response.Data = _bedeliasApi.MatricularseACurso(matricula);

                    if (response.Data)
                    {
                        UsuarioCurso usuarioCurso = new UsuarioCurso();
                        usuarioCurso.CursoId = matricula.IdCurso;
                        usuarioCurso.FacultadId = matricula.IdFacultad;
                        usuarioCurso.UsuarioId = matricula.Cedula;

                        _context.UsuarioCurso.Add(usuarioCurso);
                        await _context.SaveChangesAsync();
                    }
                }
                else 
                {
                    response.Data = true;
                    UsuarioCurso usuarioCurso = new UsuarioCurso();
                    usuarioCurso.CursoId = matricula.IdCurso;
                    usuarioCurso.FacultadId = matricula.IdFacultad;
                    usuarioCurso.UsuarioId = matricula.Cedula;

                    _context.UsuarioCurso.Add(usuarioCurso);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ApiResponse<bool>> darBajaMatricula(DTMatricula matricula)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                UsuarioCurso usuarioCurso = _context.UsuarioCurso.First(c => c.CursoId == matricula.IdCurso && c.FacultadId == matricula.IdFacultad && c.UsuarioId == matricula.Cedula);
                _context.UsuarioCurso.Remove(usuarioCurso);
                await _context.SaveChangesAsync();
                response.Data = true;
            }
            catch (Exception e)
            {
                response.Data = false;
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }

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
        public async Task<ApiResponse<GetSeccionCursoDTO>> editSeccion(int idSeccion, AddSeccionCursoDTO seccion)
        {
            ApiResponse<GetSeccionCursoDTO> response = new ApiResponse<GetSeccionCursoDTO>();
            try
            {
                SeccionCurso seccionUpdate = _context.SeccionesCursos.SingleOrDefault(sc => sc.Id == idSeccion);
                seccionUpdate.Titulo = seccion.Titulo;
                seccionUpdate.Indice = seccion.Indice;
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetSeccionCursoDTO>(seccionUpdate);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }

            return response;
        }
        public async Task<ApiResponse<List<GetSeccionCursoDTO>>> deleteSeccion(int idSeccion)
        {
            ApiResponse<List<GetSeccionCursoDTO>> response = new ApiResponse<List<GetSeccionCursoDTO>>();
            try
            {
                SeccionCurso seccionCurso = _context.SeccionesCursos.First(sc => sc.Id == idSeccion);
                _context.SeccionesCursos.Remove(seccionCurso);
                await _context.SaveChangesAsync();
                response.Data = _context.SeccionesCursos.Select(sc => _mapper.Map<GetSeccionCursoDTO>(sc)).Where(sc => sc.CursoId == seccionCurso.CursoId).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<AddComponenteDTO>> addComponente(AddComponenteDTO componente, IFormFile archivo)
        {
            ApiResponse<AddComponenteDTO> response = new ApiResponse<AddComponenteDTO>();
            try
            {
                Componente c = new Componente
                {
                    Indice = componente.Indice,
                    Nombre = componente.Nombre,
                    SeccionCursoId = componente.SeccionCursoId,
                    Tipo = componente.Tipo
                };
                _context.Componentes.Add(c);
                await _context.SaveChangesAsync();

                int idComponente = c.Id;

                switch (c.Tipo)
                {
                    case "texto":
                        c.Texto = componente.Texto;
                        Console.WriteLine(c.Texto);
                        break;
                    case "imagen":
                        Archivo a = new Archivo
                        {
                            ComponenteId = idComponente,
                            Extension = Path.GetExtension(archivo.FileName).Substring(1),
                            Nombre = Path.GetFileNameWithoutExtension(archivo.FileName)
                        };

                        _context.UploadS3(archivo, "componentFile", a.Nombre + "." + a.Extension);
                        a.Ubicacion = "componentFile/" + a.Nombre + "." + a.Extension;

                        _context.Archivos.Add(a);
                        break;
                    case "archivo":
                        Archivo i = new Archivo
                        {
                            ComponenteId = idComponente,
                            Extension = Path.GetExtension(archivo.FileName).Substring(1),
                            Nombre = Path.GetFileNameWithoutExtension(archivo.FileName)
                        };

                        _context.UploadS3(archivo, "componentFile", i.Nombre + "." + i.Extension);
                        i.Ubicacion = "componentFile/" + i.Nombre + "." + i.Extension;

                        _context.Archivos.Add(i);
                        break;
                    case "contenedor":
                        ContenedorTarea contenedorTarea = new ContenedorTarea();

                        contenedorTarea.ComponenteId = idComponente;
                        contenedorTarea.FechaCierre = componente.ContenedorTarea.FechaCierre;
                        break;
                    case "comunicado":
                        Comunicado comunicado = new Comunicado
                        {
                            ComponenteId = idComponente,
                            Descripcion = componente.Comunicado.Descripcion,
                            Titulo = componente.Comunicado.Titulo
                        };

                        _context.Comunicados.Add(comunicado);
                        break;
                    case "encuesta":
                        EncuestaCurso encuesta = new EncuestaCurso
                        {
                            ComponenteId = idComponente,
                            //Fecha = componente.Encuesta.Fecha,
                            IdCurso = componente.Encuesta.IdCursos.First(),
                            IdEncuesta = componente.Encuesta.IdEncuesta
                        };

                        _context.EncuestaCursos.Add(encuesta);
                        break;
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

        public async Task<ApiResponse<GetComponenteDTO>> editComponente(int idComponente, AddComponenteDTO componente)
        {
            ApiResponse<GetComponenteDTO> response = new ApiResponse<GetComponenteDTO>();
            try
            {
                Componente componenteUpdate = _context.Componentes.Include(c => c.Archivo)
                                                                  .Include(c => c.Comunicado)
                                                                  .Include(c => c.ContenedorTarea)
                                                                  .Include(c => c.Encuesta)
                                                                  .SingleOrDefault(c => c.Id == idComponente);
                
                componenteUpdate.Indice =  componente.Indice;
                componenteUpdate.Nombre = componente.Nombre;

                if (componente.Tipo.Equals("Archivo"))
                {
                   /* Archivo a = new Archivo();

                    a.ComponenteId = idComponente;
                    a.Extension = Path.GetExtension(archivo.FileName).Substring(1);
                    a.Nombre = Path.GetFileNameWithoutExtension(archivo.FileName);

                    _context.UploadS3(archivo, "ComponenteArchivo", a.Nombre + a.Extension);
                    a.Ubicacion = "https://dotnet-storage.s3.amazonaws.com/ComponenteArchivo/" + a.Nombre + a.Extension;

                    _context.Archivos.Add(a);
                   */ 
                }
                else if (componente.Tipo.Equals("Comunicado"))
                {
                    //Comunicado comunicadoUpdate = _context.Comunicados.SingleOrDefault(comuni => comuni.ComponenteId == componenteUpdate.Id);

                    //comunicadoUpdate.Descripcion = componente.Comunicado.Descripcion;
                    //comunicadoUpdate.Titulo = componente.Comunicado.Titulo;
                    componenteUpdate.Comunicado.Descripcion = componente.Comunicado.Descripcion;
                    componenteUpdate.Comunicado.Titulo = componente.Comunicado.Titulo;

                }
                else if (componente.Tipo.Equals("Encuesta"))
                {
                    //EncuestaCurso encuestaUpdate = _context.EncuestaCursos.SingleOrDefault(e => e.ComponenteId == componenteUpdate.Id);

                    //encuestaUpdate.Fecha = componente.Encuesta.Fecha;
                    //componenteUpdate.Encuesta.Fecha = componente.Encuesta.Fecha;
                }
                else if (componente.Tipo.Equals("ContenedorTarea"))
                {
                    //ContenedorTarea contenedorTareaUpdate = _context.ContenedoresTareas.SingleOrDefault(con => con.ComponenteId == componenteUpdate.Id);

                    //contenedorTareaUpdate.FechaCierre = componente.ContenedorTarea.FechaCierre;

                    componenteUpdate.ContenedorTarea.FechaCierre = componente.ContenedorTarea.FechaCierre;
                }

                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetComponenteDTO>(componenteUpdate);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }

            return response;
        }
        public async Task<ApiResponse<List<GetComponenteDTO>>> deleteComponente(int idComponente)
        {
            ApiResponse<List<GetComponenteDTO>> response = new ApiResponse<List<GetComponenteDTO>>();
            try
            {
                Componente componente = _context.Componentes.First(c => c.Id == idComponente);
                _context.Componentes.Remove(componente);
                await _context.SaveChangesAsync();
                response.Data = _context.Componentes.Select(c => _mapper.Map<GetComponenteDTO>(c)).Where(c => c.SeccionCursoId == componente.SeccionCursoId).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
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
