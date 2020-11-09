using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs.Componente;

namespace Utilidades.DTOs.SeccionCurso
{
    public class GetSeccionCursoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int CursoId { get; set; }

        public int Indice { get; set; }
        public List<GetComponenteDTO> Componentes { get; set; }
    }
}
