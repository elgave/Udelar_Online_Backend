using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades.DTOs.Comunicado
{
    public class GetComunicadoDTO
    {
        public int Id { get; set; }
        public int ComponenteId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
    }
}
