using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class Encuesta : Componente
    {
        public string Pregunta { get; set; }
        public virtual ICollection<Respuesta> Respuestas { get; set; }
    }
}
