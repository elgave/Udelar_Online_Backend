using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilidades;
using Utilidades.DTOs.Facultad;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EntregaIndividual.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultadController : ControllerBase
    {
        private readonly IFacultadManager _facultadManager;

        public FacultadController(IFacultadManager facultadmanager)
        {
            _facultadManager = facultadmanager;
        }

        // GET: api/<FacultadController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_facultadManager.lists());
        }

        [HttpGet("soloFacultad")]
        public IActionResult GetSolofacultad()
        {
            return Ok(_facultadManager.listsSolofacultad());
        }

        [HttpGet("usuariosXfacultad")]
        public IActionResult GetUsuariosXFacultad()
        {
            return Ok(_facultadManager.UsuariosXFacultad());
        }

        // GET api/<FacultadController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _facultadManager.get(id));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AddFacultadDTO facultad)
        {
            return Ok(await _facultadManager.add(facultad, Request.Form.Files[0]));
        }

        [Authorize(Roles = "admin, administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AddFacultadDTO facultad)
        {
            return Ok(await _facultadManager.edit(id, facultad));
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _facultadManager.delete(id));
        }
    }
}
