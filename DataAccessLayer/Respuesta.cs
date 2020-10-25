using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class Respuesta
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public int EncuestaId { get; set; }
        public virtual Encuesta Encuesta { get; set; }
    }
}
