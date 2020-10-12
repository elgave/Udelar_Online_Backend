using System;
using System.Collections.Generic;
using System.Text;

namespace Utilidades
{
    public class DTCurso
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int CantCreditos { get; set; }

        public DTCurso() { }

        public DTCurso(int id, string nombre, int cantCreditos)
        {
            Id = id;
            Nombre = nombre;
            CantCreditos = cantCreditos;
        }
    }
}
