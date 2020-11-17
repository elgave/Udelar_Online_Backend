using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades.DTOs.Template.SeccionTemplate
{
    public class GetSeccionTemplateDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int TemplateId { get; set; }

        public int Indice { get; set; }
    }
}
