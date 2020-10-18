using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer
{
    public class Carrera
    {
        [Key]
        public int Id { get; set; }
        public int FacultadId { get; set; }
        public string Nombre { get; set; }
        public virtual Facultad Facultad { get; set; }
        public virtual ICollection<Curso> Cursos { get; set; }
        public Carrera() { }
        public Carrera(string nombre,int idFacultad)
        {
            Nombre = nombre;
            FacultadId = idFacultad;
            Cursos = new List<Curso>();
        }
    }
}
