using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades
{
    public class DTMatricula
    {
        public string Cedula { get; set; }

        public int IdFacultad { get; set; }

        public int IdCurso { get; set; }

        public DTMatricula() { }

        public DTMatricula(string cedula, int idFacultad, int idCurso)
        {
            Cedula = cedula;
            IdFacultad = idFacultad;
            IdCurso = IdCurso;
        }
        
    }
}
