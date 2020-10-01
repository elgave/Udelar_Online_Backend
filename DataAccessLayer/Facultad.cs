using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer
{
    public class Facultad
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public Facultad() { }
        public Facultad(string nombre)
        {
            Nombre = nombre;
        }
    }
}