using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades.DTOs.Calendario
{
    public class GetCalendarioDTO
    {
        public int Id { get; set; }
        public int ComponenteId { get; set; }
        public string Titulo { get; set; }
        public List<GetFechaCalendarioDTO> FechasCalendario{ get; set; }
    }
}
