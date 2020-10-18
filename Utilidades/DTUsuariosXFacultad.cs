using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades
{
    public class DTUsuariosXFacultad
    {
        public int FacultadId { get; set; }
        public string NombreFacultad { get; set; }
        public int CantUsuarios { get; set; }

        public DTUsuariosXFacultad() { }
        
        public DTUsuariosXFacultad(int facultadId,string nombreFacultad, int cantUsuarios)
        {
            FacultadId = facultadId;
            NombreFacultad = nombreFacultad;
            CantUsuarios = cantUsuarios;
        }
    }
}
