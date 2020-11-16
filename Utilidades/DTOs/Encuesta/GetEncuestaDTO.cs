using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs.Curso;

namespace Utilidades.DTOs.Encuesta
{
    public class GetEncuestaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        //public string Fecha { get; set; }
        public string CreadaPor { get; set; }
        public /*virtual*/ List<GetPreguntaDTO> Preguntas { get; set; }



       

    }
}
