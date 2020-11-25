using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class FechaCalendario
    {
       
        public int Id { get; set; }
        public string Fecha { get; set; }
        public string Texto { get; set; }
        public int CalendarioId { get; set; }
        [ForeignKey("CalendarioId")]
        public virtual Calendario Calendario { get; set; }



  
    }
}
