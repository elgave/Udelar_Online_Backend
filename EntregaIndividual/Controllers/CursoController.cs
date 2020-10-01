using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EntregaIndividual.Controllers
{
    [Route("EntregaIndividualApi/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private CursoManager _cursoManager;

        public CursoController()
        {
            _cursoManager = new CursoManager();
        }

        // GET: api/<FacultadController>
        [HttpGet]
        public IEnumerable<DTCurso> Get()
        {
            var cursos = _cursoManager.lists();
            return cursos;
        }

        // GET api/<FacultadController>/5
        [HttpGet("{id}")]
        public DTCurso Get(int id)
        {
            var curso = _cursoManager.get(id);

            return curso;

        }

        // POST api/<FacultadController>
        [HttpPost]
        public ActionResult Post([FromBody] DTCurso curso)
        {
            try
            {
                var c = _cursoManager.get(curso.Id);

                if (c == null)
                {
                    _cursoManager.add(curso);
                    return Ok();
                }
                else
                    return BadRequest();
            }
            catch
            {
                return BadRequest();
            }

        }

        // PUT api/<FacultadController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] DTCurso  curso)
        {
            try
            {
                _cursoManager.edit(curso);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE api/<FacultadController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _cursoManager.delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
