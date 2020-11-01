using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs.Curso;

namespace Utilidades.DTOs.Encuesta
{
    public class GetEncuestaCursoDTO
    {
        public int IdEncuesta { get; set; }

        public int IdCurso { get; set; }
        public string Fecha { get; set; }
        public virtual GetEncuestaDTO Encuesta { get; set; }


    }
}
