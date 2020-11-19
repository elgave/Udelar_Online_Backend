using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using Utilidades.DTOs.Encuesta;

namespace BusinessLayer
{
    public interface IEncuestaManager
    {
        ApiResponse<List<GetEncuestaDTO>> lists();

        ApiResponse<List<GetEncuestaDTO>> listarXRol(string rol);
        Task<ApiResponse<List<GetEncuestaDTO>>> add(AddEncuestaDTO encuesta);
        Task<ApiResponse<List<GetEncuestaDTO>>> delete(int id);
        Task<ApiResponse<GetEncuestaDTO>> get(int id);

        Task<ApiResponse<GetEncuestaDTO>> getSinRespuestas(int id);
        Task<ApiResponse<GetEncuestaDTO>> edit(int id, AddEncuestaDTO encuesta);

        //preguntas
        /*Task<ApiResponse<List<GetPreguntaDTO>>> addPregunta(AddPreguntaDTO pregunta);
            
        Task<ApiResponse<List<GetPreguntaDTO>>> deletePregunta(int id);//no tendria que poderse si hay respuestas

        Task<ApiResponse<GetPreguntaDTO>> editPregunta(int id, AddPreguntaDTO pregunta);*/



        //respuestas
        /*Task<ApiResponse<List<GetRespuestaDTO>>> addRespuesta(AddRespuestaDTO respuesta);
        Task<ApiResponse<List<GetRespuestaDTO>>> deleteRespuesta(int id);

        Task<ApiResponse<GetRespuestaDTO>> editRespuesta(int id, AddRespuestaDTO respuesta);*/

        void responderEncuesta(AddRespuestaEncuestaDTO respuestaEncuesta);

       
        //Publicar una encuesta en un curso
        ApiResponse<List<GetEncuestaCursoDTO>> listAllEncuestaCurso();
        Task<ApiResponse<List<GetEncuestaCursoDTO>>> addEncuestaCurso(AddEncuestaCursoDTO encuestaCurso);
        Task<ApiResponse<GetEncuestaCursoDTO>> getEcuestaCurso(int idCurso);


        //Encuesta-Usuario 
        ApiResponse<List<GetEncuestaUsuarioDTO>> listAllEncuestaUsuario();
        //Task<ApiResponse<List<GetEncuestaUsuarioDTO>>> addEncuestaUsuario(AddEncuestaUsuarioDTO encuestaUsuario);
        Task<ApiResponse<GetEncuestaUsuarioDTO>> getEcuestaUsuario(string cedula);


        //Publicar una encuesta en una facultad
        ApiResponse<List<GetEncuestaFacultadDTO>> listAllEncuestaFacultad();
        Task<ApiResponse<List<GetEncuestaFacultadDTO>>> addEncuestaFacultad(AddEncuestaFacultadDTO encuestaFacultad);
        Task<ApiResponse<GetEncuestaFacultadDTO>> getEcuestaFacultad(int idFacultad);
    }
}
