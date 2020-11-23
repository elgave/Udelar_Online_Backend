using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using Utilidades.DTOs.Encuesta;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EntregaIndividual.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncuestaController : ControllerBase
    {
        private readonly IEncuestaManager _encuestaManager;

        public EncuestaController(IEncuestaManager encuestamanager)
        {
            _encuestaManager = encuestamanager;
        }

        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_encuestaManager.lists());
        }

        [Authorize(Roles = "admin,administrador,docente")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _encuestaManager.get(id));
        }

        [Authorize(Roles = "admin,administrador,docente,estudiante")]
        [HttpGet("encuestaSinRespuesta/{id}")]
        public async Task<IActionResult> GetSinResouesta(int id)
        {
            return Ok(await _encuestaManager.getSinRespuestas(id));
        }

        [Authorize(Roles = "admin,administrador,docente")]
        [HttpGet("encuestasXRol/{rol}")]
        public IActionResult Get(string rol)
        {
            return Ok(_encuestaManager.listarXRol(rol));
        }

        [Authorize(Roles = "admin,administrador,docente")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddEncuestaDTO encuesta)
        {
            return Ok(await _encuestaManager.add(encuesta));
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AddEncuestaDTO encuesta)
        {
            return Ok(await _encuestaManager.edit(id, encuesta));
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _encuestaManager.delete(id));
        }


        [Authorize(Roles = "estudiante")]
        [HttpPost("responderEncuesta")]
        public async  Task<IActionResult> Post([FromBody] AddRespuestaEncuestaDTO respuestaEncuesta)
        {
            return Ok(await _encuestaManager.responderEncuesta(respuestaEncuesta));
            
        }

        [Authorize(Roles = "admin,administrador,docente")]
        //EncuestaCurso
        [HttpPost("addEncuestaCurso")]
        public async Task<IActionResult> Post([FromBody] AddEncuestaCursoDTO encuestaCurso)
        {
            return Ok(await _encuestaManager.addEncuestaCurso(encuestaCurso));
        }

        [HttpGet("ListEncuestaCurso")]
        public async Task<IActionResult> GetEncuestaTocurso(int idCurso)
        {
            return Ok(await _encuestaManager.getEcuestaCurso(idCurso));
        }

        [HttpGet("ListAllEncuestaUsuario")]
        public IActionResult ListEncuestaUsuario()
        {
            return Ok(_encuestaManager.listAllEncuestaUsuario());
        }

        [HttpGet("ListEncuestaUsuario")]
        public async Task<IActionResult> GetEncuestaUsuario(string cedula)
        {
            return Ok(await _encuestaManager.getEcuestaUsuario(cedula));
        }


        //Encuesta-Facultad
        [HttpPost("addEncuestaFacultad")]
        public async Task<IActionResult> Post([FromBody] AddEncuestaFacultadDTO encuestaFacultad)
        {
            return Ok(await _encuestaManager.addEncuestaFacultad(encuestaFacultad));
        }

        [HttpGet("listEncuestaFacultad/{id}")]
        public  IActionResult GetEncuestaFacultad(int id)
        {
            return Ok(_encuestaManager.getEcuestaFacultad(id));
        }

    }
}
