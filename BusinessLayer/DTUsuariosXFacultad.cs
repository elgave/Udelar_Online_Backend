using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class DTUsuariosXFacultad
    {
        public int IdFacultad { get; set; }
        public string NombreFacultad { get; set; }
        public int CantUsuarios { get; set; }

        public DTUsuariosXFacultad() { }
        
        public DTUsuariosXFacultad(int idFacultad,string nombreFacultad, int cantUsuarios)
        {
            IdFacultad = idFacultad;
            NombreFacultad = nombreFacultad;
            CantUsuarios = cantUsuarios;
        }
    }
}
