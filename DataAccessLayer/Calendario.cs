using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class Calendario
    {
        
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int IdCurso { get; set; }
        public int ComponenteId { get; set; }

        [ForeignKey("IdCurso")]
        public virtual Curso Curso { get; set; }

        [ForeignKey("ComponenteId")]
        public virtual Componente Componente { get; set; }

                
        public virtual ICollection<FechaCalendario> FechasCalendario { get; set; }

       
    }
}
