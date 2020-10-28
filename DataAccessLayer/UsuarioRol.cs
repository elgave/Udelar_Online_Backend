using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class UsuarioRol
    {
        public string UsuarioCedula { get; set; }
        public int UsuarioFacultadId { get; set; }
        public int RolId { get; set; }
        [ForeignKey("RolId")]
        public virtual Rol Rol { get; set; }
    }
}
