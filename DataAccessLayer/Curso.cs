using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }
        public int FacultadId { get; set; }
        public string Nombre { get; set; }
        public int CantCreditos { get; set; }
        public virtual ICollection<UsuarioCurso> UsuariosCursos { get; set; }
        public virtual ICollection<CursoDocente> CursosDocentes { get; set; }
        public virtual ICollection<SeccionCurso> SeccionesCurso { get; set; }
        public virtual Facultad Facultad { get; set; }
        
        public Curso(string nombre, int cantCreditos)
        {
            Nombre = nombre;
            CantCreditos = cantCreditos;
        }
    }
}