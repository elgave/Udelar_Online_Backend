using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class UsuarioRol
    {
        public string UsuarioId { get; set; }
        public int FacultadId { get; set; }
        public int RolId { get; set; }
        [ForeignKey("RolId")]
        public Rol Rol { get; set; }
        [ForeignKey("UsuarioId, FacultadId")]
        public Usuario Usuario { get; set; }
    }
}
