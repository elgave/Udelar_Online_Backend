using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs;
using Utilidades.DTOs.Curso;

namespace Utilidades.DTOs.Usuario
{
    public class GetUsuarioDTO
    {
        public string Cedula { get; set; }
        public int FacultadId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<GetUsuarioRolDTO> Roles { get; set; }
        public List<GetCursoDTO> Cursos { get; set; }
    }
}
