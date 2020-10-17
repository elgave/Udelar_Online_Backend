using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades
{
    public class DTUsuario
    {
        public string Cedula { get; set; }
        public int IdFacultad { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }
        public string Correo { get; set; }

        public string Contrasena { get; set; }

        public DTUsuario() { }

        public DTUsuario(string cedula, int idFacultad, string nombre, string apellido, string correo, string contrasena )
        {
            Cedula = cedula;
            IdFacultad = idFacultad;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Contrasena = contrasena;
            
        }
    }
}
