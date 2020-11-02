using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace DataAccessLayer
{
    public class Comunicado
    {
        [Key]
        public int Id { get; set; }

        public int ComponenteId { get; set; }
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        [ForeignKey("ComponenteId")]
        public virtual Componente Componente { get; set; }

    }
}
