using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class EncuestaUsuario
    {
        public int IdEncuesta { get; set; }
        public int FacultadId { get; set; }

        public string Cedula { get; set; }
        public string Fecha { get; set; }

        [ForeignKey("IdEncuesta")]
        public virtual Encuesta Encuesta { get; set; }

        [ForeignKey("Cedula,FacultadId")]
        public virtual Usuario Usuario { get; set; }
    }

}
