using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades.DTOs.Calendario
{
    public class GetFechaCalendarioDTO
    {
        public int Id { get; set; }
        
        public string Fecha { get; set; }
        public string Texto { get; set; }
        public int CalendarioId { get; set; }
    }
}
/*public int Id { get; set; }
        public int EncuestaId { get; set; }
        public string Texto { get; set; }*/