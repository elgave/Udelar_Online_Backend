using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

namespace DataAccessLayer
{
    public class Usuario
    {
        [Key]
        public string Cedula { get; set; }
        [Key]
        public int IdFacultad { get; set; }
        [Key]
        public string Tipo { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }
        public string Correo { get; set; }

        public string Contrasena { get; set; }

        public DateTime FechaCreacion { get; set;}
        

        public Usuario() { }

        public Usuario(string cedula, int idFacultad, string tipo, string nombre, string apellido,string correo, string contrasena)
        {
            Cedula = cedula;
            IdFacultad = idFacultad;
            Tipo = tipo;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Contrasena = contrasena;
            FechaCreacion = DateTime.Today;
            
        }

    }
}
