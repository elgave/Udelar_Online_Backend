using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades.DTOs.Encuesta
{
    public class AddEncuestaDTO
    {
        //public int EncuestaId { get; set; }
        public string Titulo { get; set; }
        public string Fecha { get; set; }

        public string CreadaPor { get; set; }

        public List<AddPreguntaDTO> Preguntas { get; set; }
    }
}
