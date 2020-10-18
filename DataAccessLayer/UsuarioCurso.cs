using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class UsuarioCurso
    {
        public string CursoId { get; set; }
        public int UsuarioId { get; set; }
        public Curso Curso { get; set; }
        public Usuario Usuario { get; set; }
    }
}
