﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
}
