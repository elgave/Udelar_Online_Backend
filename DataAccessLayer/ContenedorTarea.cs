using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class ContenedorTarea
    {
        [Key]
        public int Id { get; set; }

        public int ComponenteId { get; set; }
        public DateTime FechaCierre { get; set; }
        public ICollection<EntregaTarea> TareasEntregadas { get; set; }

        [ForeignKey("ComponenteId")]
        public virtual Componente Componente { get; set; }

        
    }
}
