using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs.EntregaTarea;

namespace Utilidades.DTOs.ContenedorTarea
{
    public class GetContenedorTareaDTO
    {
        public int Id { get; set; }

        public int ComponenteId { get; set; }
        public DateTime FechaCierre { get; set; }
        public virtual ICollection<GetEntregaTareaDTO> TareasEntregadas { get; set; }
    }
}
