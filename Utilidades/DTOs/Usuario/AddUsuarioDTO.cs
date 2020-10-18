using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades.DTOs.Usuario
{
    public class AddUsuarioDTO
    {
        public string Cedula { get; set; }
        public int FacultadId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
    }
}
