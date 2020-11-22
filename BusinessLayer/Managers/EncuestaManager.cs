using AutoMapper;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using Utilidades.DTOs.Encuesta;

namespace BusinessLayer
{
    public class EncuestaManager : IEncuestaManager
    {
        private readonly IMapper _mapper;
        private readonly MyContext _context;
        public EncuestaManager(IMapper mapper, MyContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public ApiResponse<List<GetEncuestaDTO>> lists()
        {
            ApiResponse<List<GetEncuestaDTO>> response = new ApiResponse<List<GetEncuestaDTO>>();
            try
            {
                //response.Data =  _context.Encuestas.Include(f => f.Preguntas).ThenInclude(f => f.Respuestas).Select(f => _mapper.Map<GetEncuestaDTO>(f)).ToList();
                response.Data = _context.Encuestas.Select(f => _mapper.Map<GetEncuestaDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 204;
                response.Message = e.Message;
            }
            return response;
        }

        public ApiResponse<List<GetEncuestaDTO>> listarXRol(string rol)
        {
            ApiResponse<List<GetEncuestaDTO>> response = new ApiResponse<List<GetEncuestaDTO>>();
            try
            {
                //response.Data =  _context.Encuestas.Include(f => f.Preguntas).ThenInclude(f => f.Respuestas).Select(f => _mapper.Map<GetEncuestaDTO>(f)).ToList();
                response.Data = _context.Encuestas.Where(e => e.CreadaPor == rol).Select(f => _mapper.Map<GetEncuestaDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 204;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<List<GetEncuestaDTO>>> add(AddEncuestaDTO encuesta)
        {
            ApiResponse<List<GetEncuestaDTO>> response = new ApiResponse<List<GetEncuestaDTO>>();
            try
            {
                Encuesta e = new Encuesta();
                //e.Fecha = encuesta.Fecha;
                e.Titulo = encuesta.Titulo;
                e.CreadaPor = encuesta.CreadaPor;

                _context.Encuestas.Add(e);
                await _context.SaveChangesAsync();

                int idEncuesta = e.Id;

                foreach(AddPreguntaDTO p in encuesta.Preguntas)
                {
                    Pregunta preg = new Pregunta();

                    preg.EncuestaId = idEncuesta;
                    preg.Texto = p.Texto;
                    _context.Preguntas.Add(preg);
                }

                await _context.SaveChangesAsync();
                //_context.Encuestas.Add(_mapper.Map<Encuesta>(encuesta));
                //await _context.SaveChangesAsync();
                response.Data = _context.Encuestas.Select(f => _mapper.Map<GetEncuestaDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<List<GetEncuestaDTO>>> delete(int id)
        {
            ApiResponse<List<GetEncuestaDTO>> response = new ApiResponse<List<GetEncuestaDTO>>();
            try
            {
                Encuesta encuesta = _context.Encuestas.First(f => f.Id == id);

                _context.Encuestas.RemoveRange(encuesta);
                await _context.SaveChangesAsync();
                response.Data = _context.Encuestas.Select(f => _mapper.Map<GetEncuestaDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<GetEncuestaDTO>> get(int id)
        {
            ApiResponse<GetEncuestaDTO> response = new ApiResponse<GetEncuestaDTO>();
            try
            {
                response.Data = _mapper.Map<GetEncuestaDTO>(await _context.Encuestas.Include(f => f.Preguntas).ThenInclude(f => f.Respuestas).FirstAsync(f => f.Id == id));
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<GetEncuestaDTO>> getSinRespuestas(int id)
        {
            ApiResponse<GetEncuestaDTO> response = new ApiResponse<GetEncuestaDTO>();
            try
            {
                response.Data = _mapper.Map<GetEncuestaDTO>(await _context.Encuestas.Include(f => f.Preguntas).FirstAsync(f => f.Id == id));
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<GetEncuestaDTO>> edit(int id, AddEncuestaDTO encuesta)
        {
            ApiResponse<GetEncuestaDTO> response = new ApiResponse<GetEncuestaDTO>();
            try
            {
                Encuesta encuestaUpdate = _context.Encuestas.First(f => f.Id == id);
                encuestaUpdate.Titulo = encuesta.Titulo;
                //encuestaUpdate.Fecha = encuesta.Fecha; 
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetEncuestaDTO>(encuestaUpdate);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }

            return response;
        }

        public void responderEncuesta(AddRespuestaEncuestaDTO respuestaEncuesta)
        {
            foreach(AddRespuestaDTO r in respuestaEncuesta.respuestas)
            {
                Respuesta respuesta = new Respuesta();
                respuesta.PreguntaId = r.PreguntaId;
                respuesta.Texto = r.Texto;

                _context.Respuestas.Add(respuesta);
            }

            EncuestaUsuario encuestaUsuario = new EncuestaUsuario();

            encuestaUsuario.Cedula = respuestaEncuesta.Cedula;
            encuestaUsuario.FacultadId = respuestaEncuesta.FacultadId;
            encuestaUsuario.Fecha = DateTime.Today.ToString();
            encuestaUsuario.IdEncuesta = respuestaEncuesta.EncuestaId;

            _context.EncuestaUsuarios.Add(encuestaUsuario);

            _context.SaveChangesAsync();
        }


        /*EncuestasCurso */
        public ApiResponse<List<GetEncuestaCursoDTO>> listAllEncuestaCurso()
        {
            ApiResponse<List<GetEncuestaCursoDTO>> response = new ApiResponse<List<GetEncuestaCursoDTO>>();
            try
            {
                response.Data = _context.EncuestaCursos.Include(f => f.Encuesta).ThenInclude(f => f.Preguntas).ThenInclude(f => f.Respuestas)
                    .Select(f => _mapper.Map<GetEncuestaCursoDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 204;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<List<GetEncuestaCursoDTO>>> addEncuestaCurso(AddEncuestaCursoDTO encuestaCurso)
        {
            ApiResponse<List<GetEncuestaCursoDTO>> response = new ApiResponse<List<GetEncuestaCursoDTO>>();
            
            try
            {
                
                    SeccionCurso sc = await _context.SeccionesCursos.Where(sc => sc.Titulo == "Encuestas" && sc.CursoId == encuestaCurso.IdCurso).FirstOrDefaultAsync();
                    Encuesta e = _context.Encuestas.Find(encuestaCurso.IdEncuesta);

                    if (sc == null)
                    {
                        sc = new SeccionCurso();
                        sc.CursoId = encuestaCurso.IdCurso;
                        sc.Indice = 0;
                        sc.Titulo = "Encuestas";

                        _context.SeccionesCursos.Add(sc);
                        await _context.SaveChangesAsync();
                    }

                    Componente comp = new Componente();
                    comp.Indice = 0;
                    comp.Nombre = e.Titulo;
                    comp.SeccionCursoId = sc.Id;
                    comp.Tipo = "encuesta";

                    _context.Componentes.Add(comp);

                    await _context.SaveChangesAsync();

                    int idComponenete = comp.Id;

                    EncuestaCurso enc = new EncuestaCurso();

                    enc.ComponenteId = idComponenete;
                    //enc.Fecha = encuestaCurso.Fecha;
                    enc.IdCurso = encuestaCurso.IdCurso;
                    enc.IdEncuesta = encuestaCurso.IdEncuesta;

                    _context.EncuestaCursos.Add(enc);

                    await _context.SaveChangesAsync();
                
                response.Data = _context.EncuestaCursos.Select(f => _mapper.Map<GetEncuestaCursoDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<GetEncuestaCursoDTO>> getEcuestaCurso(int idCurso)
        {
            ApiResponse<GetEncuestaCursoDTO> response = new ApiResponse<GetEncuestaCursoDTO>();
            try
            {
                response.Data = _mapper.Map<GetEncuestaCursoDTO>(await _context.EncuestaCursos.Include(f => f.Encuesta).ThenInclude(f => f.Preguntas).ThenInclude(f => f.Respuestas).FirstAsync(f => f.IdCurso == idCurso));
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            return response;
        }



        /*Encuesta-Usuario*/
        public ApiResponse<List<GetEncuestaUsuarioDTO>> listAllEncuestaUsuario()
        {
            ApiResponse<List<GetEncuestaUsuarioDTO>> response = new ApiResponse<List<GetEncuestaUsuarioDTO>>();
            try
            {
                response.Data = _context.EncuestaUsuarios.Select(f => _mapper.Map<GetEncuestaUsuarioDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 204;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<GetEncuestaUsuarioDTO>> getEcuestaUsuario(string cedula)
        {
            ApiResponse<GetEncuestaUsuarioDTO> response = new ApiResponse<GetEncuestaUsuarioDTO>();
            try
            {
                response.Data = _mapper.Map<GetEncuestaUsuarioDTO>(await _context.EncuestaUsuarios.FirstAsync(f => f.Cedula == cedula));
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            return response;
        }

        /*Encuestas-Facultad */
        public ApiResponse<List<GetEncuestaFacultadDTO>> listAllEncuestaFacultad()
        {
            ApiResponse<List<GetEncuestaFacultadDTO>> response = new ApiResponse<List<GetEncuestaFacultadDTO>>();
            try
            {
                response.Data = _context.encuestaFacultad.Include(f => f.Encuesta).ThenInclude(f => f.Preguntas).ThenInclude(f => f.Respuestas)
                    .Select(f => _mapper.Map<GetEncuestaFacultadDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 204;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<List<GetEncuestaFacultadDTO>>> addEncuestaFacultad(AddEncuestaFacultadDTO encuestaFacultad)
        {
            ApiResponse<List<GetEncuestaFacultadDTO>> response = new ApiResponse<List<GetEncuestaFacultadDTO>>();

            try
            {
                
                    Encuesta e = _context.Encuestas.Find(encuestaFacultad.IdEncuesta);           

                    EncuestaFacultad enc = new EncuestaFacultad();

                    //enc.Fecha = encuestaFacultad.Fecha;
                    enc.IdFacultad = encuestaFacultad.IdFacultad;
                    enc.IdEncuesta = encuestaFacultad.IdEncuesta;

                    _context.encuestaFacultad.Add(enc);

                    await _context.SaveChangesAsync();
                
                response.Data = _context.encuestaFacultad.Select(f => _mapper.Map<GetEncuestaFacultadDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }
            return response;
           
        }

        public ApiResponse<List<GetEncuestaFacultadDTO>> getEcuestaFacultad(int idFacultad)
        {
            ApiResponse<List<GetEncuestaFacultadDTO>> response = new ApiResponse<List<GetEncuestaFacultadDTO>>();
            try
            {
                response.Data =  _context.encuestaFacultad.Where(f => f.IdFacultad == idFacultad).Include(f => f.Encuesta).Select(f => _mapper.Map<GetEncuestaFacultadDTO>(f)).ToList();
                
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            return response;
        }
    }
}
