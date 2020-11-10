using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class EncuestaFacultad
    {
        public int IdEncuesta { get; set; }

        public int IdFacultad { get; set; }
        public string Fecha { get; set; }

        [ForeignKey("IdEncuesta")]
        public virtual Encuesta Encuesta { get; set; }

        [ForeignKey("IdFacultad")]
        public virtual Facultad Facultad { get; set; }
    }

}
