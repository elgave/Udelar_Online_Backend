using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
   public class Componente
    {
        [Key]
        public int Id { get; set; }
        public int SeccionCursoId { get; set; }
        public string Nombre { get; set; }

        public string Tipo { get; set; }

        public int Indice { get; set; }

        public string Texto { get; set; }


        [ForeignKey("SeccionCursoId")]
        public virtual SeccionCurso SeccionCurso { get; set; }

        /// <summary>
        /// Los atrubitos de abajo, solo uno sera el que contenga informacion
        /// </summary>
        public virtual Archivo Archivo { get; set; }

        public virtual ContenedorTarea ContenedorTarea { get; set; }

        public virtual Comunicado Comunicado { get; set; }

        public virtual EncuestaCurso Encuesta { get; set; }
        public virtual Calendario Calendario { get; set; }
    }
}
