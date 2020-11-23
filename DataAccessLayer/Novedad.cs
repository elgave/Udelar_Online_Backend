using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class Novedad
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public int FacultadId { get; set; }
        public virtual Facultad Facultad { get; set; }
    }
}
