using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades.DTOs.Encuesta
{
    public class AddEncuestaFacultadDTO
    {
        public int IdEncuesta { get; set; }

        public List<int> IdFacultad { get; set; }
        public string Fecha { get; set; }
    }
}
