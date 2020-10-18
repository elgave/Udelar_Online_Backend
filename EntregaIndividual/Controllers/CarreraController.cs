using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using Utilidades.DTOs.Carrera;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EntregaIndividual.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarreraController : ControllerBase
    {
        private readonly ICarreraManager _carreraManager;

        public CarreraController(ICarreraManager carreramanager)
        {
            _carreraManager = carreramanager;
        }

        // GET: api/<FacultadController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_carreraManager.lists());
        }

        // GET api/<FacultadController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _carreraManager.get(id));
        }

        // POST api/<FacultadController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCarreraDTO carrera)
        {
            return Ok(await _carreraManager.add(carrera));
        }

        // PUT api/<FacultadController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AddCarreraDTO carrera)
        {
            return Ok(await _carreraManager.edit(id, carrera));
        }

        // DELETE api/<FacultadController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _carreraManager.delete(id));
        }
    }
}
