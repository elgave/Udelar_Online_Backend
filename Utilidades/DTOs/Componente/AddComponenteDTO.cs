using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs.Archivo;
using Utilidades.DTOs.Comunicado;
using Utilidades.DTOs.ContenedorTarea;
using Utilidades.DTOs.Encuesta;

namespace Utilidades.DTOs.Componente
{
    public class AddComponenteDTO
    {
        public int SeccionCursoId { get; set; }
        public string Nombre { get; set; }

        public string Tipo { get; set; }

        public int Indice { get; set; }

        public string Texto { get; set; }

        //public  AddArchivoDTO Archivo { get; set; }

        public  AddComunicadoDTO Comunicado { get; set; }

        public AddContenedorTareaDTO ContenedorTarea { get; set; }

        public AddEncuestaCursoDTO Encuesta { get; set; }
    }
}
