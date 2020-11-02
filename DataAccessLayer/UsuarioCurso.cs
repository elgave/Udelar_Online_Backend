using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class UsuarioCurso
    {
        public string UsuarioId { get; set; }
        public int FacultadId { get; set; }
        public int CursoId { get; set; }
        [ForeignKey("CursoId")]
        public Curso Curso { get; set; }
        [ForeignKey("UsuarioId, FacultadId")]
        public Usuario Usuario { get; set; }
    }
}