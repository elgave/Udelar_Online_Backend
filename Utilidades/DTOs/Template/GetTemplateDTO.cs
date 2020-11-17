using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs.Template.SeccionTemplate;

namespace Utilidades.DTOs.Template
{
    public class GetTemplateDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<GetSeccionTemplateDTO> Secciones { get; set; }

    }
}
