using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs.Archivo;

namespace Utilidades.DTOs.EntregaTarea
{
    public class GetEntregaTareaDTO
    {
        public int Id { get; set;}
        public string UsuarioId { get; set;}

        public string NombreUsuario { get; set; }

        public string ApellidoUsuario { get; set; }

        public int FacultadId { get; set;}
        public int ContenedorTareaId { get; set;}
        public string Calificacion { get; set;}
        public string Estado { get; set;}
        public DateTime FechaEntrega { get; set;}

        public string NombreArchivo { get; set; }
        public string ExtensionArchivo { get; set; }

        public string UbicacionArchivo { get; set; }
        public GetArchivoDTO ArchivoEntrega { get; set;}
    }
}
