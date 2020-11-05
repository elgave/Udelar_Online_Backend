using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades.DTOs.Encuesta
{
    public class AddEncuestaUsuarioDTO
    {
        public int IdEncuesta { get; set; }
        public int FacultadId { get; set; }

        public string Cedula { get; set; }
        public string Fecha { get; set; }
    }
}
