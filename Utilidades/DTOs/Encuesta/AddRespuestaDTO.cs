﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades.DTOs.Encuesta
{
    public class AddRespuestaDTO
    {
       // public int Id { get; set; }
       // public int EncuestaId { get; set; }
        public int PreguntaId { get; set; }
        public string Texto { get; set; }
        
    }
}
