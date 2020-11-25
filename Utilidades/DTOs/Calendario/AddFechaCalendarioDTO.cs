using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades.DTOs.Calendario
{
    public class AddFechaCalendarioDTO
    {
        public int CalendarioId { get; set; }
        public string Fecha { get; set; }

        public string Texto { get; set; }

        /*public int PreguntaId { get; set; }
        public string Texto { get; set; }*/
    }
}
