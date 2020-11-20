using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs;
using Utilidades.DTOs.Curso;

namespace Utilidades.DTOs.UsuarioCurso
{
    public class GetUsuarioNotaDTO
    {
        public string Cedula { get; set; }
        //public int CursoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Nota { get; set; }
        public string Comentario { get; set; }

    }
}
