using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class Archivo
    {
        [Key]
        public int Id { get; set; }
        //public string Tipo { get; set; } //Video, Material educativo o entrega de trabajo
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public string Ubicacion { get; set; }

        public int? ComponenteId { get; set; }
        [ForeignKey("ComponenteId")]
        public virtual Componente Componente { get; set; }


        public int? EntregaTareaId { get; set; }
        [ForeignKey("EntregaTareaId")]
        public virtual EntregaTarea EntregaTarea { get; set; }
    }
}
