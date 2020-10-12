using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades
{
    public class DTFacultad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public DTFacultad() { }
        public DTFacultad(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
