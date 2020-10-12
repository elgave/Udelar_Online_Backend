using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Utilidades;

namespace BusinessLayer
{
    public class FacultadManager
    {
        public List<DTFacultad> lists()
        {
            using (var ctx = new MyContext())
            {
                var facultades = ctx.Facultades.ToList();
                List<DTFacultad> dtFacultades = new List<DTFacultad>();
                foreach (Facultad f in facultades)
                {
                    DTFacultad facu = new DTFacultad(f.Id,f.Nombre);
                    dtFacultades.Add(facu);
                }

                return dtFacultades;
            }

        }

        public void add(DTFacultad facultad)
        {
            try
            {
                using (var ctx = new MyContext())
                {
                    Facultad f = new Facultad(facultad.Nombre);
                    
                    ctx.Facultades.Add(f);
                    ctx.SaveChanges();

                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void delete(int id)
        {   
            using (var ctx = new MyContext())
            {
                var facultad = ctx.Facultades.SingleOrDefault(f => f.Id == id);
                ctx.Facultades.Remove(facultad);
                ctx.SaveChanges();
            }
        }

        public DTFacultad get(int id)
        {
            using (var ctx =new MyContext())
            {
                var facultad = ctx.Facultades.SingleOrDefault(f => f.Id == id);
                if (facultad != null)
                {
                    DTFacultad facu = new DTFacultad(facultad.Id, facultad.Nombre);
                    return facu;
                }
                else
                    return null;
            }

        }
        public void edit(DTFacultad facultad)
        {
            using (var ctx = new MyContext())
            {
                //Facultad facu = new Facultad(facultad.Nombre);
                var facultad_update = ctx.Facultades.SingleOrDefault(f => f.Id == facultad.Id);
                facultad_update.Nombre = facultad.Nombre;
                ctx.SaveChanges();
            }

        }

        public List<DTUsuariosXFacultad> UsuariosXFacultad()
        {
            using (var ctx = new MyContext())
            {
                //Facultad facu = new Facultad(facultad.Nombre);

                List<Usuario> usuarios = ctx.Usuarios.ToList();
                List<Facultad> facultades = ctx.Facultades.ToList();
                List<DTUsuariosXFacultad> resultado = new List<DTUsuariosXFacultad>();

                foreach (Facultad f in facultades)
                {
                    DTUsuariosXFacultad uxf = new DTUsuariosXFacultad(f.Id,f.Nombre,0);
                    resultado.Add(uxf);
                }
                foreach (Usuario u in usuarios)
                {
                    foreach(DTUsuariosXFacultad r in resultado)
                    {
                        if(u.IdFacultad == r.IdFacultad)
                        {
                            r.CantUsuarios += 1;
                        }
                    }
                }

                return resultado;
            }
        }
    }
}
