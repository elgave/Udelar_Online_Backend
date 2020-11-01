using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class Archivo
    {
        [Key]
        public int Id { get; set; }

        public int CursoId { get; set; }
        public string Tipo { get; set; } //Video, Material educativo o entrega de trabajo
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public string Ubicacion { get; set; }

        public string UsuarioId { get; set; }

        [ForeignKey("CursoId")]
        public virtual Curso Curso { get; set; }
    }
}
