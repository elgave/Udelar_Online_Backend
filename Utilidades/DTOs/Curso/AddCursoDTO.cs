using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades.DTOs.Curso
{
    public class AddCursoDTO
    {
        public string Nombre { get; set; }
        public int CantCreditos { get; set; }
        public int FacultadId { get; set; }
        public int CarreraId { get; set; }
        public bool ConfirmaBedelia { get; set; }
    }
}
