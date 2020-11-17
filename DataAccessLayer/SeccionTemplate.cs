using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class SeccionTemplate
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int TemplateId { get; set; }

        public int Indice { get; set; }

        [ForeignKey("TemplateId")]
        public virtual Template Template { get; set; }
    }
}
