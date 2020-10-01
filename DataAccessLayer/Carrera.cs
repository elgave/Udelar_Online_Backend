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

        public int IdFacultad { get; set; }
        public string Nombre { get; set; }

        public List<Curso> Cursos { get; set; }

        public Carrera() { }

        public Carrera(string nombre,int idFacultad)
        {
            Nombre = nombre;
            IdFacultad = idFacultad;
            Cursos = new List<Curso>();
        }
    }
}
