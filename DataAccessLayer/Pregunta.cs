using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class Pregunta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Texto { get; set; }
        public int EncuestaId { get; set; }
        [ForeignKey("EncuestaId")]
        public virtual Encuesta Encuesta { get; set; }
    }
}
    
