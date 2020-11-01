using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades.DTOs.Archivo
{
    public class GetArchivoDTO
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public string Ubicacion { get; set; }
        public string UsuarioId { get; set; }
    }
}
