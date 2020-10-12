using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilidades;

namespace BusinessLayer
{
    public class CarreraManager
    {
        public List<DTCarrera> lists()
        {
            using (var ctx = new MyContext())
            {
                var carreras = ctx.Carreras.ToList();
                List<DTCarrera> dtCarreras = new List<DTCarrera>();
                foreach (Carrera c in carreras)
                {
                    DTCarrera carrera = new DTCarrera(c.Id,c.IdFacultad,c.Nombre);
                    dtCarreras.Add(carrera);
                }

                return dtCarreras;
            }

        }

        public void add(DTCarrera carrera)
        {
            try
            {
                using (var ctx = new MyContext())
                {
                    Carrera c = new Carrera(carrera.Nombre,carrera.IdFacultad);

                    ctx.Carreras.Add(c);
                    ctx.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void delete(int id)
        {
            using (var ctx = new MyContext())
            {
                var carrera = ctx.Carreras.SingleOrDefault(c => c.Id == id);
                ctx.Carreras.Remove(carrera);
                ctx.SaveChanges();
            }
        }

        public DTCarrera get(int id)
        {
            using (var ctx = new MyContext())
            {
                var carrera = ctx.Carreras.SingleOrDefault(c => c.Id == id);
                if (carrera != null)
                {
                    DTCarrera c = new DTCarrera(carrera.Id,carrera.IdFacultad, carrera.Nombre);
                    return c;
                }
                else
                    return null;
            }

        }
        public void edit(DTCarrera carrera)
        {
            using (var ctx = new MyContext())
            {
                //Facultad facu = new Facultad(facultad.Nombre);
                var carrera_update = ctx.Carreras.SingleOrDefault(c => c.Id == carrera.Id);
                carrera_update.Nombre = carrera.Nombre;
                ctx.SaveChanges();
            }

        }
    }
}
