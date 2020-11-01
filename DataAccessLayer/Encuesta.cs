using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class Encuesta : Componente
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Titulo { get; set; }
        public string Fecha { get; set; }
        public virtual ICollection<Pregunta> Preguntas { get; set; }
        public virtual ICollection<Respuesta> Respuestas { get; set; }
    }
}
