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




        //Preguntas
        /*[HttpPost("addPregunta")]
        public async Task<IActionResult> Post([FromBody] AddPreguntaDTO pregunta)
        {
            return Ok(await _encuestaManager.addPregunta(pregunta));
        }

        [HttpPut("editPregutna{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AddPreguntaDTO pregunta)
        {
            return Ok(await _encuestaManager.editPregunta(id, pregunta));
        }

        // DELETE api/<FacultadController>/5
        [HttpDelete("deletePregunta{id}")]
        public async Task<IActionResult> DeletePregunta(int id)
        {
            return Ok(await _encuestaManager.deletePregunta(id));
        }*/



        /*//Respuestas
        [HttpPost("addRespuesta")]
        public async Task<IActionResult> Post([FromBody] AddRespuestaDTO respuesta)
        {
            return Ok(await _encuestaManager.addRespuesta(respuesta));
        
        }*/
        [Authorize(Roles = "estudiante")]
        [HttpPost("responderEncuesta")]
        public  IActionResult Post([FromBody] AddRespuestaEncuestaDTO respuestaEncuesta)
        {
            _encuestaManager.responderEncuesta(respuestaEncuesta);
            return Ok();

        }

        [Authorize(Roles = "admin,administrador,docente")]
        //EncuestaCurso
        [HttpPost("addEncuestaCurso")]
        public async Task<IActionResult> Post([FromBody] AddEncuestaCursoDTO encuestaCurso)
        {
            return Ok(await _encuestaManager.addEncuestaCurso(encuestaCurso));
        }

        [HttpGet("ListAllEncuestaCurso")]
        public IActionResult List()
        {
            return Ok(_encuestaManager.listAllEncuestaCurso());
        }

        [HttpGet("ListEncuestaCurso")]
        public async Task<IActionResult> GetEncuestaTocurso(int idCurso)
        {
            return Ok(await _encuestaManager.getEcuestaCurso(idCurso));
        }


        //EncuestaUsuario
        /*[HttpPost("addEncuestaUsuario")]
        public async Task<IActionResult> Post([FromBody] AddEncuestaUsuarioDTO encuestaUsuario)
        {
            return Ok(await _encuestaManager.addEncuestaUsuario(encuestaUsuario));
        }*/

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

        [HttpGet("ListAllEncuestaFAcultad")]
        public IActionResult ListEncuestaFacultad()
        {
            return Ok(_encuestaManager.listAllEncuestaFacultad());
        }

        [HttpGet("listEncuestaFacultad/{id}")]
        public  IActionResult GetEncuestaFacultad(int id)
        {
            return Ok(_encuestaManager.getEcuestaFacultad(id));
        }

    }
}
