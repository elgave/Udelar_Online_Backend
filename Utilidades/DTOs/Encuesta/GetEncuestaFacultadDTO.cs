﻿using System;
using System.Collections.Generic;
using System.Text;
using Utilidades.DTOs.Curso;

namespace Utilidades.DTOs.Encuesta
{
    public class GetEncuestaFacultadDTO
    {
        public int IdEncuesta { get; set; }

        public int IdFacultad { get; set; }
        //public string Fecha { get; set; }
        public GetEncuestaDTO Encuesta { get; set; }


    }
}
