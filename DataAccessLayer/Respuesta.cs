using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class Respuesta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Texto { get; set; }
        public int EncuestaId { get; set; }
        public int PreguntaId { get; set; }

        
        public virtual Encuesta Encuesta { get; set; }
        public virtual Pregunta Pregunta { get; set; }

    }
}
