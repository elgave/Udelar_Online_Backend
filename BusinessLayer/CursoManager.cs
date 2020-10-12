using DataAccessLayer;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilidades;

namespace BusinessLayer
{
    public class CursoManager
    {
       
        public List<DTCurso> lists()
        {
            using (var ctx = new MyContext())
            {
                var cursos = ctx.Cursos.ToList();
                List<DTCurso> dtCursos = new List<DTCurso>();
                foreach (Curso c in cursos)
                {
                    DTCurso curso = new DTCurso(c.Id,c.Nombre,c.CantCreditos);
                    dtCursos.Add(curso);
                }

                return dtCursos;
            }
        }

        public void add(DTCurso curso)
        {
            try
            {
                using (var ctx = new MyContext())
                {
                    Curso c = new Curso(curso.Nombre,curso.CantCreditos);

                    ctx.Cursos.Add(c);
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
                var curso = ctx.Cursos.SingleOrDefault(c => c.Id == id);
                ctx.Cursos.Remove(curso);
                ctx.SaveChanges();
            }
        }

        public DTCurso get(int id)
        {
            using (var ctx = new MyContext())
            {
                var curso = ctx.Cursos.SingleOrDefault(c => c.Id == id);
                if (curso != null)
                {
                    DTCurso c = new DTCurso(curso.Id, curso.Nombre, curso.CantCreditos);
                    return c;
                }
                else
                    return null;
            }

        }
        public void edit(DTCurso curso)
        {
            using (var ctx = new MyContext())
            {
                //Facultad facu = new Facultad(facultad.Nombre);
                var curso_update = ctx.Cursos.SingleOrDefault(c => c.Id == curso.Id);
                curso_update.Nombre = curso.Nombre;
                ctx.SaveChanges();
            }

        }

        public bool matricularse(DTMatricula matricula)
        {
            IBedeliasApi _bedeliasApi = new BedeliasApi();
            return _bedeliasApi.MatricularseACurso(matricula);
        }
    }
}
