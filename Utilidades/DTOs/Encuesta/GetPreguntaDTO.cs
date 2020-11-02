using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs.Curso;

namespace Utilidades.DTOs.Encuesta
{
    public class GetPreguntaDTO
    {
        public int Id { get; set; }
        public int EncuestaId { get; set; }
        public string Texto { get; set; }
       

        public /*virtual*/ List<GetRespuestaDTO> Respuestas { get; set; }

    }
}
