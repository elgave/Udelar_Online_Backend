using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text;

namespace DataAccessLayer
{
    public class Usuario
    {
        
        public string Cedula { get; set; }
        
        public int FacultadId { get; set; }
        
        public string Nombre { get; set; }

        public string Apellido { get; set; }
        public string Correo { get; set; }

        public string Contrasena { get; set; }

        public DateTime FechaCreacion { get; set;}

        [ForeignKey("FacultadId")]
        public virtual Facultad Facultad { get; set; }
    
        public virtual List<UsuarioRol> Roles { get; set; }
        

        public Usuario() { }

        public Usuario(string cedula, int facultadId, string nombre, string apellido,string correo, string contrasena)
        {
            Cedula = cedula;
            FacultadId = facultadId;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Contrasena = contrasena;
            FechaCreacion = DateTime.Today;
        }

    }
}
