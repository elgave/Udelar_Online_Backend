using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class EncuestaCurso
    {
        public int IdEncuesta { get; set; }

        public int IdCurso { get; set; }
        public string Fecha { get; set; }

        public int ComponenteId { get; set; }

        [ForeignKey("IdEncuesta")]
        public virtual Encuesta Encuesta { get; set; }

        [ForeignKey("IdCurso")]
        public virtual Curso Curso { get; set; }

        [ForeignKey("ComponenteId")]
        public virtual Componente Componente { get; set; }
    }

}
