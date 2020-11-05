using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs.Curso;

namespace Utilidades.DTOs.Encuesta
{
    public class GetEncuestaUsuarioDTO
    {
        public int IdEncuesta { get; set; }
        public int FacultadId { get; set; }
        public string Cedula { get; set; }
        public string Fecha { get; set; }
       // public virtual GetEncuestaDTO Encuesta { get; set; }


    }
}
