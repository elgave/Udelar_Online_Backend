using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs.Curso;

namespace Utilidades.DTOs.Encuesta
{
    public class GetRespuestaDTO
    {
        public int Id { get; set; }
        //public int EncuestaId { get; set; }
        public int PreguntaId { get; set; }
        public string Texto { get; set; }
        
    }
}
