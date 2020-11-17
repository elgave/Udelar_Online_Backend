using System.Collections.Generic;

namespace Utilidades.DTOs.UsuarioCurso
{
    public class AddUsuarioNotaDTO
    {
        public string Cedula { get; set; }
        public int FacultadId { get; set; }
        public int CursoId { get; set; }
        public int Calificacion { get; set; }
        public string Comentario { get; set; }
       
    }
}
