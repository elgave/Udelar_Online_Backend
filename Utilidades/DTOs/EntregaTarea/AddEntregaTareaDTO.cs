using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs.Archivo;

namespace Utilidades.DTOs.EntregaTarea
{
    public class AddEntregaTareaDTO
    {
        public string UsuarioId { get; set; }
        public int FacultadId { get; set; }
        public int ContenedorTareaId { get; set; }
        //public string Calificacion { get; set; }
        public string FechaEntrega { get; set; }

        public int ArchivoId { get; set; }
        //public AddArchivoDTO ArchivoEntrega { get; set; }
    }
}
