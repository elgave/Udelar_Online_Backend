using AutoMapper;
using DataAccessLayer;
using System.Linq;
using Utilidades.DTOs.Curso;
using Utilidades.DTOs.Usuario;
using Utilidades.DTOs.Facultad;
using Utilidades.DTOs;
using Utilidades.DTOs.Archivo;
using Utilidades.DTOs.Encuesta;
using Utilidades.DTOs.SeccionCurso;
using Utilidades.DTOs.Componente;
using Utilidades.DTOs.Comunicado;
using Utilidades.DTOs.EntregaTarea;
using Utilidades.DTOs.ContenedorTarea;
using Utilidades;
using Utilidades.DTOs.UsuarioCurso;

namespace TSI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Rol, GetRolDTO>();
            CreateMap<Usuario, GetUsuarioDTO>()
                .ForMember(dto => dto.Roles, u => u.MapFrom(u => u.UsuariosRoles.Select(r => r.Rol)));

            CreateMap<AddUsuarioDTO, Usuario>();

            CreateMap<Curso, GetCursoDTO>()
                .ForMember(dto => dto.Usuarios, u => u.MapFrom(u => u.UsuariosCursos.Select(uc => uc.Usuario)))
                .ForMember(dto => dto.Docentes, d => d.MapFrom(d => d.CursosDocentes.Select(cd => cd.Usuario)))
                .ForMember(dto => dto.Secciones, s => s.MapFrom(s => s.SeccionesCurso));
            CreateMap<AddCursoDTO, Curso>();

            CreateMap<SeccionCurso, GetSeccionCursoDTO>()
                .ForMember(dto => dto.Componentes, u => u.MapFrom(u => u.Componentes));
            CreateMap<AddSeccionCursoDTO, SeccionCurso>();

            CreateMap<ContenedorTarea, GetContenedorTareaDTO>()
                .ForMember(dto => dto.TareasEntregadas, u => u.MapFrom(u => u.TareasEntregadas));
            CreateMap<AddContenedorTareaDTO, ContenedorTarea>();

            CreateMap<Componente, GetComponenteDTO>();
            CreateMap<AddComponenteDTO, Componente>();

            CreateMap<Comunicado, GetComunicadoDTO>();
            CreateMap<AddComunicadoDTO, Comunicado>();

            CreateMap<EntregaTarea, GetEntregaTareaDTO>();
            CreateMap<AddEntregaTareaDTO, EntregaTarea>();

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

            CreateMap<EncuestaUsuario, GetEncuestaUsuarioDTO>();
            CreateMap<AddEncuestaUsuarioDTO, EncuestaUsuario>();

            CreateMap<EncuestaFacultad, GetEncuestaFacultadDTO>();
            CreateMap<AddEncuestaFacultadDTO, EncuestaFacultad>();

            CreateMap<IdPasswordModel, IdPassword>();
            CreateMap<IdPassword, IdPasswordModel>();

            CreateMap<UsuarioCurso, GetUsuarioNotaDTO>();
            CreateMap<AddUsuarioNotaDTO, UsuarioCurso>();
        }
    }
}
