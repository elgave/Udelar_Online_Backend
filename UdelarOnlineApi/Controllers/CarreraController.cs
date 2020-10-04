using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EntregaIndividual.Controllers
{
    [Route("EntregaIndividualApi/[controller]")]
    [ApiController]
    public class CarreraController : ControllerBase
    {
        private CarreraManager _carreraManager;

        public CarreraController()
        {
            _carreraManager = new CarreraManager();
        }

        // GET: api/<FacultadController>
        [HttpGet]
        public IEnumerable<DTCarrera> Get()
        {
            var carreras = _carreraManager.lists();
            return carreras;
        }

        // GET api/<FacultadController>/5
        [HttpGet("{id}")]
        public DTCarrera Get(int id)
        {
            var carrera = _carreraManager.get(id);

            return carrera;

        }

        // POST api/<FacultadController>
        [HttpPost]
        public ActionResult Post([FromBody] DTCarrera carrera)
        {
            try
            {
                _carreraManager.add(carrera);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        // PUT api/<FacultadController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] DTCarrera carrera)
        {
            try
            {
                _carreraManager.edit(carrera);
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
                _carreraManager.delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
