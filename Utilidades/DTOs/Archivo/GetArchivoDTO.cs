using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades.DTOs.Archivo
{
    public class GetArchivoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public string Ubicacion { get; set; }

        public int? ComponenteId { get; set; }
        public int? EntregaTareaId { get; set; }
        
    }
}
