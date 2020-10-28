using AutoMapper;
using DataAccessLayer;
using System.Linq;
using Utilidades.DTOs.Curso;
using Utilidades.DTOs.Usuario;
using Utilidades.DTOs.Facultad;
using Utilidades.DTOs.Carrera;
using Utilidades.DTOs;

namespace TSI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuario, GetUsuarioDTO>()
                .ForMember(dto => dto.Cursos, u => u.MapFrom(u => u.UsuariosCursos.Select(uc => uc.Curso)))
                .ForMember(dto => dto.Roles, u => u.MapFrom(u => u.Roles.Select(r => r.Rol)));
            CreateMap<AddUsuarioDTO, Usuario>();

            CreateMap<Curso, GetCursoDTO>();
            CreateMap<AddCursoDTO, Curso>();

            CreateMap<Facultad, GetFacultadDTO>();
            CreateMap<AddFacultadDTO, Facultad>();

            CreateMap<Rol, GetRolDTO>();
        }
    }
}
