
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class DTCarrera
    {
        public int Id { get; set; }

        public int IdFacultad { get; set; }
        public string Nombre { get; set; }


        public DTCarrera() { }

        public DTCarrera(int id,int idFacultad, string nombre)
        {
            Id = id;
            IdFacultad = idFacultad;
            Nombre = nombre;
        }
    }
}
