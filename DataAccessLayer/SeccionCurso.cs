using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class SeccionCurso
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int CursoId { get; set; }

        public int Indice { get; set; }

        public List<Componente> Componentes { get; set; }
  
        [ForeignKey("CursoId")]
        public virtual Curso Curso { get; set; }
    }
}
