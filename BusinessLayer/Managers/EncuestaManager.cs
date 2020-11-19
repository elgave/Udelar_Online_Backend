﻿using AutoMapper;
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




        /*Preguntas*/
        /*public async Task<ApiResponse<List<GetPreguntaDTO>>> addPregunta(AddPreguntaDTO pregunta)
        {
            ApiResponse<List<GetPreguntaDTO>> response = new ApiResponse<List<GetPreguntaDTO>>();
            try
            {
                _context.Preguntas.Add(_mapper.Map<Pregunta>(pregunta));
                await _context.SaveChangesAsync();
                response.Data = _context.Preguntas.Select(f => _mapper.Map<GetPreguntaDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<List<GetPreguntaDTO>>> deletePregunta(int id)
        {  
            ApiResponse<List<GetPreguntaDTO>> response = new ApiResponse<List<GetPreguntaDTO>>();
            try
            {
                Pregunta pregunta = _context.Preguntas.First(f => f.Id == id);
                _context.Preguntas.Remove(pregunta);
                await _context.SaveChangesAsync();
                response.Data = _context.Preguntas.Select(f => _mapper.Map<GetPreguntaDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<GetPreguntaDTO>> editPregunta(int id, AddPreguntaDTO pregunta)
        {
            ApiResponse<GetPreguntaDTO> response = new ApiResponse<GetPreguntaDTO>();
            try
            {
                Pregunta preguntaUpdate = _context.Preguntas.First(f => f.Id == id);
                preguntaUpdate.EncuestaId = pregunta.EncuestaId;
                preguntaUpdate.Texto = pregunta.Texto;
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetPreguntaDTO>(preguntaUpdate);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }

            return response;
        }*/




        /*Respuestas*/

        /* public async Task<ApiResponse<List<GetRespuestaDTO>>> addRespuesta(AddRespuestaDTO respuesta)
         {
             ApiResponse<List<GetRespuestaDTO>> response = new ApiResponse<List<GetRespuestaDTO>>();
             try
             {
                 _context.Respuestas.Add(_mapper.Map<Respuesta>(respuesta));
                 await _context.SaveChangesAsync();
                 response.Data = _context.Respuestas.Select(f => _mapper.Map<GetRespuestaDTO>(f)).ToList();
             }
             catch (Exception e)
             {
                 response.Success = false;
                 response.Status = 500;
                 response.Message = e.Message;
             }
             return response;
         }

         public async Task<ApiResponse<List<GetRespuestaDTO>>> deleteRespuesta(int id)
         {
             throw new NotImplementedException();
         }

         public async Task<ApiResponse<GetRespuestaDTO>> editRespuesta(int id, AddRespuestaDTO respuesta)
         {
             throw new NotImplementedException();
         }*/

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
                foreach (int idCurso in encuestaCurso.IdCursos)
                {
                    SeccionCurso sc = _context.SeccionesCursos.Where(sc => sc.Titulo == "Encuestas").First();
                    Encuesta e = _context.Encuestas.Find(encuestaCurso.IdEncuesta);

                    if (sc == null)
                    {
                        sc.CursoId = idCurso;
                        sc.Indice = 0;
                        sc.Titulo = e.Titulo;

                        _context.SeccionesCursos.Add(sc);
                        await _context.SaveChangesAsync();
                    }

                    Componente comp = new Componente();
                    comp.Indice = 0;
                    comp.Nombre = e.Titulo;
                    comp.SeccionCursoId = sc.Id;
                    comp.Tipo = "Encuesta";

                    _context.Componentes.Add(comp);

                    await _context.SaveChangesAsync();

                    int idComponenete = comp.Id;

                    EncuestaCurso enc = new EncuestaCurso();

                    enc.ComponenteId = idComponenete;
                    //enc.Fecha = encuestaCurso.Fecha;
                    enc.IdCurso = idCurso;
                    enc.IdEncuesta = encuestaCurso.IdEncuesta;

                    _context.EncuestaCursos.Add(enc);

                    await _context.SaveChangesAsync();
                }
                response.Data = _context.EncuestaCursos.Select(f => _mapper.Map<GetEncuestaCursoDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }
            return response;
            
            //Lo q estaba antes
            //try
            //{

            //    _context.EncuestaCursos.Add(_mapper.Map<EncuestaCurso>(encuestaCurso));
            //    await _context.SaveChangesAsync();
            //    response.Data = _context.EncuestaCursos.Select(f => _mapper.Map<GetEncuestaCursoDTO>(f)).ToList();
            //}
            //catch (Exception e)
            //{
            //    response.Success = false;
            //    response.Status = 500;
            //    response.Message = e.Message;
            //}
            //return response;
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

        /*public async Task<ApiResponse<List<GetEncuestaUsuarioDTO>>> addEncuestaUsuario(AddEncuestaUsuarioDTO encuestaUsuario)
        {
            ApiResponse<List<GetEncuestaUsuarioDTO>> response = new ApiResponse<List<GetEncuestaUsuarioDTO>>();
            try
            {
                _context.EncuestaUsuarios.Add(_mapper.Map<EncuestaUsuario>(encuestaUsuario));
                await _context.SaveChangesAsync();
                response.Data = _context.EncuestaUsuarios.Select(f => _mapper.Map<GetEncuestaUsuarioDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }
            return response;
        }*/

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
                foreach (int idFacultad in encuestaFacultad.IdFacultad)
                {
                    Encuesta e = _context.Encuestas.Find(encuestaFacultad.IdEncuesta);           

                    EncuestaFacultad enc = new EncuestaFacultad();

                    enc.Fecha = encuestaFacultad.Fecha;
                    enc.IdFacultad = idFacultad;
                    enc.IdEncuesta = encuestaFacultad.IdEncuesta;

                    _context.encuestaFacultad.Add(enc);

                    await _context.SaveChangesAsync();
                }
                response.Data = _context.encuestaFacultad.Select(f => _mapper.Map<GetEncuestaFacultadDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }
            return response;
            //Lo que estaba antes
            /*
            ApiResponse<List<GetEncuestaFacultadDTO>> response = new ApiResponse<List<GetEncuestaFacultadDTO>>();
            try
            {
                _context.encuestaFacultad.Add(_mapper.Map<EncuestaFacultad>(encuestaFacultad));
                await _context.SaveChangesAsync();
                response.Data = _context.encuestaFacultad.Select(f => _mapper.Map<GetEncuestaFacultadDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }
            return response;*/
        }

        public async Task<ApiResponse<GetEncuestaFacultadDTO>> getEcuestaFacultad(int idFacultad)
        {
            ApiResponse<GetEncuestaFacultadDTO> response = new ApiResponse<GetEncuestaFacultadDTO>();
            try
            {
                response.Data = _mapper.Map<GetEncuestaFacultadDTO>(await _context.encuestaFacultad.Include(f => f.Encuesta).ThenInclude(f => f.Preguntas).ThenInclude(f => f.Respuestas).FirstOrDefaultAsync(f => f.IdFacultad == idFacultad));
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
