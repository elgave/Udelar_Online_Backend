using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades.DTOs.Encuesta
{
    public class AddEncuestaCursoDTO
    {
        public int IdEncuesta { get; set; }

        public List<int> IdCursos { get; set; }
        //public string Fecha { get; set; }
    }
}
