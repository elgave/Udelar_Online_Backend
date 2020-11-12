using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs.Archivo;
using Utilidades.DTOs.Comunicado;
using Utilidades.DTOs.ContenedorTarea;
using Utilidades.DTOs.Encuesta;

namespace Utilidades.DTOs.Componente
{
    public class GetComponenteDTO
    {
        public int Id { get; set; }
        public int SeccionCursoId { get; set; }
        public string Nombre { get; set; }

        public string Tipo { get; set; }

        public int Indice { get; set; }

        public string Texto { get; set; }

        /// <summary>
        /// Los atrubitos de abajo, solo uno sera el que contenga informacion
        /// </summary>
        public GetArchivoDTO Archivo { get; set; }
        public  GetComunicadoDTO Comunicado { get; set; }

        public  GetContenedorTareaDTO ContenedorTarea { get; set; }

        public GetEncuestaCursoDTO Encuesta { get; set; }
    }
}
