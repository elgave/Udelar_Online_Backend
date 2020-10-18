using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades
{
    public class DTUsuario
    {
        public string Cedula { get; set; }
        public int FacultadId { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }
        public string Correo { get; set; }

        public string Contrasena { get; set; }

        public DTUsuario() { }

        public DTUsuario(string cedula, int facultadId, string nombre, string apellido, string correo, string contrasena )
        {
            Cedula = cedula;
            FacultadId = facultadId;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Contrasena = contrasena;
            
        }
    }
}
