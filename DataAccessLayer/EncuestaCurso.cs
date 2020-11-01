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

        [ForeignKey("IdEncuesta")]
        public virtual Encuesta Encuesta { get; set; }

        [ForeignKey("IdCurso")]
        public virtual Curso Curso { get; set; }
    }

}
