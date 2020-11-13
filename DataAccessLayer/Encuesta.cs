using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class Encuesta 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Fecha { get; set; }
        public string CreadaPor { get; set; }  // Para saber si la creo un Udelar, un administrador o un docente 
        public virtual ICollection<Pregunta> Preguntas { get; set; }
        
    }
}
