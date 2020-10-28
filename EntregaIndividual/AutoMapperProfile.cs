using AutoMapper;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilidades.DTOs.Curso;
using Utilidades.DTOs.Usuario;
using Utilidades.DTOs.Facultad;
using Utilidades.DTOs.Carrera;

namespace TSI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Usuario, GetUsuarioDTO>()
                .ForMember(dto => dto.Cursos, u => u.MapFrom(u => u.UsuariosCursos.Select(uc => uc.Curso)));
            CreateMap<AddUsuarioDTO, Usuario>();

            CreateMap<Curso, GetCursoDTO>();
            CreateMap<AddCursoDTO, Curso>();

            CreateMap<Facultad, GetFacultadDTO>();
            CreateMap<AddFacultadDTO, Facultad>();

           
        }
    }
}
