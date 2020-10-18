using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer
{
    public class UsuarioRol
    {
        public string IdUsuario { get; set; }

        public int IdFacultad { get; set; }
        public int IdRol { get; set; }

        [ForeignKey("IdRol")]
        public virtual Roles Rol { get; set; }
    }

}
