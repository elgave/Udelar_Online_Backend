using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs.Curso;
using Utilidades.DTOs.Usuario;

namespace Utilidades.DTOs.Facultad
{
    public class GetFacultadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<GetCursoDTO> Cursos { get; set; }
        public List<GetUsuarioDTO> Usuarios { get; set; }
    }
}
