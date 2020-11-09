using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs.SeccionCurso;
using Utilidades.DTOs.Usuario;

namespace Utilidades.DTOs.Curso
{
    public class GetCursoDTO
    {
        public int Id { get; set; }
        public int FacultadId { get; set; }
        public string Nombre { get; set; }
        public int CantCreditos { get; set; }
        public List<GetUsuarioDTO> Usuarios { get; set; }
        public List<GetUsuarioDTO> Docentes { get; set; }
        public List<GetSeccionCursoDTO> Secciones { get; set; }
    }
}
