using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilidades;

namespace BusinessLayer
{
    public class UsuarioManager
    { 
        public List<DTUsuario> lists()
        {
            using (var ctx = new MyContext())
            {
                var usuarios = ctx.Usuarios.ToList();
                List<DTUsuario> dtUsuarios = new List<DTUsuario>();
                foreach (Usuario u in usuarios)
                {
                    DTUsuario usu = new DTUsuario(u.Cedula, u.FacultadId,u.Nombre,u.Apellido,u.Correo, u.Contrasena);
                    dtUsuarios.Add(usu);
                }

                return dtUsuarios;
            }
        }
        public void add(DTUsuario usuario)
        {
            try
            {
                using (var ctx = new MyContext())
                {
                    Usuario u = new Usuario(usuario.Cedula,usuario.FacultadId, usuario.Nombre,usuario.Apellido,usuario.Correo,usuario.Contrasena);
                    ctx.Usuarios.Add(u);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void delete(string cedula, int facultadId)
        {
            using (var ctx = new MyContext())
            {
                var usuario = ctx.Usuarios.SingleOrDefault(u => u.Cedula == cedula &&  u.FacultadId == facultadId) ;
                ctx.Usuarios.Remove(usuario);
                ctx.SaveChanges();
            }
        }

        public DTUsuario get(string cedula, int facultadId)
        {
            using (var ctx = new MyContext())
            {
                var usuario = ctx.Usuarios.SingleOrDefault(u => u.Cedula == cedula && u.FacultadId == facultadId);
                if (usuario != null)
                {
                    DTUsuario usu = new DTUsuario(usuario.Cedula, usuario.FacultadId, usuario.Nombre, usuario.Apellido, usuario.Correo,usuario.Contrasena);
                    return usu;
                }
                else
                    return null;
            }

        }
        public void edit(DTUsuario usuario)
        {
            using (var ctx = new MyContext())
            {
                //Usuario usu = new Usuario(usuario.Cedula,usuario.IdFacultad, usuario.Nombre, usuario.Apellido, usuario.Tipo);
                var usuario_update = ctx.Usuarios.SingleOrDefault(u => u.Cedula == usuario.Cedula && u.FacultadId == usuario.FacultadId);
                usuario_update.Nombre = usuario.Nombre;
                usuario_update.Apellido = usuario.Apellido;
                usuario_update.Contrasena = usuario.Contrasena;
                usuario_update.Correo = usuario.Correo;
                ctx.SaveChanges();
            }

        }

    }
}
