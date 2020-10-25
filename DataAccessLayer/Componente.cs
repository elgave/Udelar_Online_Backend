using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
   public abstract class Componente
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public virtual Curso Curso { get; set; }
    }
}
