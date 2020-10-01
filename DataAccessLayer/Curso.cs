using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int CantCreditos { get; set; }

        public Curso(string nombre, int cantCreditos)
        {
            Nombre = nombre;
            CantCreditos = cantCreditos;
        }
    }
}