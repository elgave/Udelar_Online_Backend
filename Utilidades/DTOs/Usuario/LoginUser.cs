using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades.DTOs.Usuario
{
    public class LoginUser
    {
        public string Cedula { get; set; }
        public int FacultadId { get; set; }
        public string Password { get; set; }
    }
}
