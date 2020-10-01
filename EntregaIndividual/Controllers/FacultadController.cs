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
    public class FacultadController : ControllerBase
    {
        private FacultadManager _facultadManager;

        public FacultadController()
        {
            _facultadManager = new FacultadManager();
        }

        // GET: api/<FacultadController>
        [HttpGet]
        public IEnumerable<DTFacultad> Get()
        {
            var facultades = _facultadManager.lists();
            return facultades;
        }

        [HttpGet("usuariosXfacultad")]
        public IEnumerable<DTUsuariosXFacultad> GetUsuariosXFacultad()
        {
            var resultado = _facultadManager.UsuariosXFacultad();
            return resultado;
        }

        // GET api/<FacultadController>/5
        [HttpGet("{id}")]
        public DTFacultad Get(int id)
        {
            var facultad = _facultadManager.get(id);
            
            return facultad;
            
        }

        // POST api/<FacultadController>
        [HttpPost]
        public ActionResult Post([FromBody] DTFacultad facultad)
        {
            try
            {
                _facultadManager.add(facultad);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            
        }

        // PUT api/<FacultadController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] DTFacultad facultad)
        {
            try
            {
                _facultadManager.edit(facultad);
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
                _facultadManager.delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
