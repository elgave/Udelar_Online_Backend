using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs.Archivo;

namespace Utilidades.DTOs.EntregaTarea
{
    public class GetEntregaTareaDTO
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public int FacultadId { get; set; }
        public int ContenedorTareaId { get; set; }
        public string Calificacion { get; set; }

        public DateTime FechaEntrega { get; set; }
        public GetArchivoDTO ArchivoEntrega { get; set; }
    }
}
