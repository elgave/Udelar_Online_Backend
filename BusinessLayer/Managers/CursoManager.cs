using AutoMapper;
using BusinessLayer.Managers;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using Utilidades.DTOs.Calendario;
using Utilidades.DTOs.Componente;
using Utilidades.DTOs.Curso;
using Utilidades.DTOs.EntregaTarea;
using Utilidades.DTOs.SeccionCurso;
using Utilidades.DTOs.Template;
using Utilidades.DTOs.Template.SeccionTemplate;
using Utilidades.DTOs.Usuario;
using Utilidades.DTOs.UsuarioCurso;

namespace BusinessLayer
{
    public class CursoManager : ICursoManager
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly MyContext _context;
        private object reader;

        public CursoManager(IConfiguration config, IMapper mapper, MyContext context)
        {
            _mapper = mapper;
            _context = context;
            _config = config;
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
                Curso c = _mapper.Map<Curso>(curso);
                _context.Cursos.Add(c);
                await _context.SaveChangesAsync();

                int idCurso = c.Id;

                if (curso.TemplateId != null && curso.TemplateId != 0)
                {
                    Template temp = await _context.Template.Include(st => st.SeccionesTemplate).FirstAsync(t => t.Id == curso.TemplateId);

                    foreach(SeccionTemplate st in temp.SeccionesTemplate)
                    {
                        SeccionCurso sc = new SeccionCurso
                        {
                            Titulo = st.Titulo,
                            CursoId = idCurso,
                            Indice = st.Indice
                            
                        };
                        _context.SeccionesCursos.Add(sc);
                       
                    }
                    await _context.SaveChangesAsync();
                }
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
                    .Include(c => c.SeccionesCurso).ThenInclude(sc => sc.Componentes).ThenInclude(co => co.Calendario)
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
                cursoUpdate.CantCreditos = curso.CantCreditos;
                cursoUpdate.ConfirmaBedelia = curso.ConfirmaBedelia;
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
                int id = matricula.IdCurso;
                Curso curso = _context.Cursos.SingleOrDefault(c => c.Id == id);

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
                    else
                    {
                        response.Message = "Bedelías no aprueba la matriculación";
                    }
                    response.Status = 200;
                    response.Success = true;
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
                        contenedorTarea.FechaCierre = Convert.ToDateTime(componente.FechaCierre);
                        _context.ContenedoresTareas.Add(contenedorTarea);
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
                            IdCurso = componente.CursoId,
                            IdEncuesta = componente.EncuestaId,
                        };

                        _context.EncuestaCursos.Add(encuesta);
                        break;
                    case "calendario":
                        Calendario calendario = new Calendario
                        {
                            ComponenteId = idComponente,
                            IdCurso = componente.CursoId,
                            Titulo = componente.Nombre,
                        };

                        _context.Calendarios.Add(calendario);
                        break;
                }

                await _context.SaveChangesAsync();
                response.Data = componente;

                GetCursoDTO curso = _mapper.Map<GetCursoDTO>(_context.Cursos
                    .Include(c => c.UsuariosCursos).ThenInclude(uc => uc.Usuario)
                    .Include(c => c.SeccionesCurso)
                    .First(c => c.SeccionesCurso.Any(s => s.Id == componente.SeccionCursoId)));

                const string q = "\"";

                foreach (GetUsuarioDTO u in curso.Usuarios)
                {
                    EmailManager.SendMail(u.Nombre+":\nSe ha agregado nuevo material al curso "+curso.Nombre+"\n\nUdelar Online.", u.Correo, _config);
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

        public async Task<ApiResponse<GetComponenteDTO>> editComponente(int idComponente, AddComponenteDTO componente)
        {
            ApiResponse<GetComponenteDTO> response = new ApiResponse<GetComponenteDTO>();
            try
            {
                Componente componenteUpdate = _context.Componentes.Include(c => c.Archivo)
                                                                  .Include(c => c.Comunicado)
                                                                  .Include(c => c.ContenedorTarea)
                                                                  .Include(c => c.Encuesta)
                                                                  .Include(c => c.Calendario)
                                                                  .SingleOrDefault(c => c.Id == idComponente);
                
                componenteUpdate.Indice =  componente.Indice;
                componenteUpdate.Nombre = componente.Nombre;

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
        public ApiResponse<List<GetTemplateDTO>> getAllTemplate()
        {
            ApiResponse<List<GetTemplateDTO>> response = new ApiResponse<List<GetTemplateDTO>>();

            try
            {
                response.Data = _context.Template.Include(t => t.SeccionesTemplate).Select(t => _mapper.Map<GetTemplateDTO>(t)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 204;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<GetTemplateDTO>> getTemplate(int id)
        {
            ApiResponse<GetTemplateDTO> response = new ApiResponse<GetTemplateDTO>();
            try
            {
                response.Data = _mapper.Map<GetTemplateDTO>(
                    await _context.Template
                    .Include(t => t.SeccionesTemplate).FirstAsync(t => t.Id == id)
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
        public async Task<ApiResponse<AddTemplateDTO>> addTemplate(AddTemplateDTO template)
        {
            ApiResponse<AddTemplateDTO> response = new ApiResponse<AddTemplateDTO>();
            try
            {
                Template t = new Template();
                t.Nombre = template.Nombre;

                _context.Template.Add(t);
                await _context.SaveChangesAsync();
                response.Data = template;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }

            return response;
        }
        public async Task<ApiResponse<GetTemplateDTO>> editTemplate(int idTemplate, AddTemplateDTO template)
        {
            ApiResponse<GetTemplateDTO> response = new ApiResponse<GetTemplateDTO>();
            try
            {
                Template templateUpdate = _context.Template.SingleOrDefault(t => t.Id == idTemplate);
                templateUpdate.Nombre = template.Nombre;
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetTemplateDTO>(templateUpdate);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }

            return response;
        }
        public async Task<ApiResponse<List<GetTemplateDTO>>> deleteTemplate(int idTemplate)
        {
            ApiResponse<List<GetTemplateDTO>> response = new ApiResponse<List<GetTemplateDTO>>();
            try
            {
                Template template = _context.Template.First(t => t.Id == idTemplate);
                _context.Template.Remove(template);
                await _context.SaveChangesAsync();
                response.Data = _context.Template.Select(t => _mapper.Map<GetTemplateDTO>(t)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<AddSeccionTemplateDTO>> addSeccionTemplate(AddSeccionTemplateDTO seccionTemplate)
        {
            ApiResponse<AddSeccionTemplateDTO> response = new ApiResponse<AddSeccionTemplateDTO>();
            try
            {
                SeccionTemplate st = new SeccionTemplate();
                st.TemplateId = seccionTemplate.TemplateId;
                st.Indice = seccionTemplate.Indice;
                st.Titulo = seccionTemplate.Titulo;

                _context.SeccionesTemplate.Add(st);
                await _context.SaveChangesAsync();
                response.Data = seccionTemplate;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }

            return response;
        }
        public async Task<ApiResponse<GetSeccionTemplateDTO>> editSeccionTemplate(int idSeccionTemplate, AddSeccionTemplateDTO seccionTemplate)
        {
            ApiResponse<GetSeccionTemplateDTO> response = new ApiResponse<GetSeccionTemplateDTO>();
            try
            {
                SeccionTemplate seccionTemplateUpdate = _context.SeccionesTemplate.SingleOrDefault(st => st.Id == idSeccionTemplate);
                seccionTemplateUpdate.Titulo = seccionTemplate.Titulo;
                seccionTemplateUpdate.Indice = seccionTemplate.Indice;
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetSeccionTemplateDTO>(seccionTemplateUpdate);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }

            return response;
        }
        public async Task<ApiResponse<List<GetSeccionTemplateDTO>>> deleteSeccionTemplate(int idSeccionTemplate)
        {
            ApiResponse<List<GetSeccionTemplateDTO>> response = new ApiResponse<List<GetSeccionTemplateDTO>>();
            try
            {
                SeccionTemplate seccionTemplate = _context.SeccionesTemplate.First(st => st.Id == idSeccionTemplate);
                _context.SeccionesTemplate.Remove(seccionTemplate);
                await _context.SaveChangesAsync();
                response.Data = _context.SeccionesTemplate.Where(st => st.TemplateId == seccionTemplate.TemplateId).Select(st => _mapper.Map<GetSeccionTemplateDTO>(st)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }
            return response;
        }
        public async Task<ApiResponse<GetEntregaTareaDTO>> getEntregaTarea(string cedula, int facultadId, int contendorId)
        {
            ApiResponse<GetEntregaTareaDTO> response = new ApiResponse<GetEntregaTareaDTO>();

            try
            {
                EntregaTarea tarea = await _context.EntregasTarea.Include(a => a.ArchivoEntrega).FirstOrDefaultAsync(et => et.UsuarioId == cedula && et.FacultadId == facultadId && et.ContenedorTareaId == contendorId);

                response.Data = _mapper.Map<GetEntregaTareaDTO>(tarea);
                
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
                EntregaTarea entregaUpdate = _context.EntregasTarea.SingleOrDefault(et => et.UsuarioId == entregaTarea.UsuarioId && et.FacultadId == entregaTarea.FacultadId && et.ContenedorTareaId == entregaTarea.ContenedorTareaId);
                if (entregaUpdate != null)
                {
                    entregaUpdate.FechaEntrega = Convert.ToDateTime(entregaTarea.FechaEntrega);

                    Archivo archivoUpdate = await _context.Archivos.FirstAsync(a => a.Id == entregaTarea.ArchivoId);

                    archivoUpdate.Extension = Path.GetExtension(archivoEntrega.FileName).Substring(1);
                    archivoUpdate.Nombre = Path.GetFileNameWithoutExtension(archivoEntrega.FileName);

                    _context.UploadS3(archivoEntrega, "componentFile", archivoUpdate.Nombre + "." + archivoUpdate.Extension);
                    archivoUpdate.Ubicacion = "componentFile/" + archivoUpdate.Nombre + "." + archivoUpdate.Extension;

                    await _context.SaveChangesAsync();
                    response.Data = entregaTarea;
                }
                else
                {


                    EntregaTarea entrega = new EntregaTarea();

                    entrega.Calificacion = 0;
                    entrega.ContenedorTareaId = entregaTarea.ContenedorTareaId;
                    entrega.UsuarioId = entregaTarea.UsuarioId;
                    entrega.FacultadId = entregaTarea.FacultadId;
                    entrega.Calificacion = 0;
                    entrega.FechaEntrega = Convert.ToDateTime(entregaTarea.FechaEntrega);
                    entrega.Estado = "Entregado";


                    _context.EntregasTarea.Add(entrega);
                    await _context.SaveChangesAsync();

                    int idEntregaTarea = entrega.Id;

                    Archivo a = new Archivo();

                    a.EntregaTareaId = idEntregaTarea;
                    a.Extension = Path.GetExtension(archivoEntrega.FileName).Substring(1);
                    a.Nombre = Path.GetFileNameWithoutExtension(archivoEntrega.FileName);

                    _context.UploadS3(archivoEntrega, "componentFile", a.Nombre + "." + a.Extension);
                    a.Ubicacion = "componentFile/" + a.Nombre + "." + a.Extension;

                    _context.Archivos.Add(a);

                    await _context.SaveChangesAsync();
                    response.Data = entregaTarea;
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


        public async Task<ApiResponse<GetUsuarioNotaDTO>> addUsuarioNota(AddUsuarioNotaDTO usuarioNota)
        {
            ApiResponse<GetUsuarioNotaDTO> response = new ApiResponse<GetUsuarioNotaDTO>();
            try
            {
                UsuarioCurso UsuarioCursoUpdate = _context.UsuarioCurso.SingleOrDefault(c => c.UsuarioId == usuarioNota.Cedula
                   && c.FacultadId == usuarioNota.FacultadId && c.CursoId == usuarioNota.CursoId);

                UsuarioCursoUpdate.Nota = usuarioNota.Nota;
                UsuarioCursoUpdate.comentario = usuarioNota.Comentario;

                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetUsuarioNotaDTO>(UsuarioCursoUpdate);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }

            return response;
        }

        public ApiResponse<List<GetUsuarioNotaDTO>> getUsuariosNota(int idCurso)
        {
            ApiResponse<List<GetUsuarioNotaDTO>> response = new ApiResponse<List<GetUsuarioNotaDTO>>();
            try
            {
                response.Data =
                     (from uc in _context.UsuarioCurso
                      join u in _context.Usuarios on uc.UsuarioId equals u.Cedula
                      where uc.CursoId == idCurso
                      
                      select new GetUsuarioNotaDTO
                      {
                          Cedula = u.Cedula,
                          Nombre = u.Nombre,
                          Apellido = u.Apellido,
                          Nota = (int)uc.Nota,
                          Comentario = uc.comentario

                      }).Distinct().ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<AddFechaCalendarioDTO>> addFecha(AddFechaCalendarioDTO fechaCalendario)
        {
            ApiResponse<AddFechaCalendarioDTO> response = new ApiResponse<AddFechaCalendarioDTO>();
            try
            {
                FechaCalendario fc = new FechaCalendario();
                fc.CalendarioId = fechaCalendario.CalendarioId;
                fc.Fecha = fechaCalendario.Fecha;
                fc.Texto = fechaCalendario.Texto;

                _context.FechaCalendarios.Add(fc);
                await _context.SaveChangesAsync();
                response.Data = fechaCalendario;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ApiResponse<GetCalendarioDTO>> getCalendario(int id)
        {
            ApiResponse<GetCalendarioDTO> response = new ApiResponse<GetCalendarioDTO>();

            try
            {
                response.Data = _mapper.Map<GetCalendarioDTO>(
                     await _context.Calendarios
                    .Include(t => t.FechasCalendario).FirstOrDefaultAsync(t => t.Id == id)
                );
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 204;
                response.Message = e.Message;
            }
            return response;
        }


    }
}


