using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades.DTOs.Encuesta
{
    public class AddRespuestaEncuestaDTO
    {
        public int EncuestaId { get; set; }

        public string Cedula { get; set; }
        public int FacultadId { get; set; }
        public List<AddRespuestaDTO> respuestas { get; set; }


    }
}
