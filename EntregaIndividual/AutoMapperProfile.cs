using AutoMapper;
using DataAccessLayer;
using System.Linq;
using Utilidades.DTOs.Curso;
using Utilidades.DTOs.Usuario;
using Utilidades.DTOs.Facultad;
using Utilidades.DTOs;
using Utilidades.DTOs.Archivo;
using Utilidades.DTOs.Encuesta;

namespace TSI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Rol, GetRolDTO>();
            CreateMap<Usuario, GetUsuarioDTO>()
                .ForMember(dto => dto.Roles, u => u.MapFrom(u => u.UsuariosRoles.Select(r => r.Rol)))
                .ForMember(dto => dto.Cursos, u => u.MapFrom(u => u.UsuariosCursos.Select(uc => uc.Curso)));
                
            CreateMap<AddUsuarioDTO, Usuario>();

            CreateMap<Curso, GetCursoDTO>();
            CreateMap<AddCursoDTO, Curso>();

            CreateMap<Facultad, GetFacultadDTO>();
            CreateMap<AddFacultadDTO, Facultad>();

            CreateMap<Archivo, GetArchivoDTO>();
            CreateMap<AddArchivoDTO, Archivo>();

            CreateMap<Encuesta, GetEncuestaDTO>();
            CreateMap<AddEncuestaDTO, Encuesta>();

            CreateMap<Pregunta, GetPreguntaDTO>();
            CreateMap<AddPreguntaDTO, Pregunta>();

            CreateMap<Respuesta, GetRespuestaDTO>();
            CreateMap<AddRespuestaDTO, Respuesta>();

            CreateMap<EncuestaCurso, GetEncuestaCursoDTO>();
            CreateMap<AddEncuestaCursoDTO, EncuestaCurso>();

        }
    }
}
