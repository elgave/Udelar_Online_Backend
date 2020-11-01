using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs.Curso;

namespace Utilidades.DTOs.Carrera
{
    public class GetCarreraDTO
    {
        public int Id { get; set; }
        public int FacultadId { get; set; }
        public string Nombre { get; set; }
        public virtual List<GetCursoDTO> Cursos { get; set; }
    }
}
