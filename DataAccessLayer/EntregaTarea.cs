using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class EntregaTarea
    {
        [Key]
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public int FacultadId { get; set; }
        public int ContenedorTareaId { get; set; }
        public string Calificacion { get; set; }

        public DateTime FechaEntrega { get; set; }

        public Archivo ArchivoEntrega { get; set; }

        [ForeignKey("UsuarioId, FacultadId")]
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("ContenedorTardeaId")]
        public virtual ContenedorTarea ContenedorTarea { get; set; }



    }
}
