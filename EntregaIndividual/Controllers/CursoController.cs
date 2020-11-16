using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilidades;
using Utilidades.DTOs.Componente;
using Utilidades.DTOs.Curso;
using Utilidades.DTOs.EntregaTarea;
using Utilidades.DTOs.SeccionCurso;
using Utilidades.DTOs.Usuario;

namespace EntregaIndividual.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly ICursoManager _cursoManager;

        public CursoController(ICursoManager cursomanager)
        {
            _cursoManager = cursomanager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_cursoManager.lists());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _cursoManager.get(id));
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCursoDTO curso)
        {
            return Ok(await _cursoManager.add(curso));
        }

        [Authorize(Roles = "admin")]
        [HttpPost("{id}")]
        public async Task<IActionResult> AddDocente(int id, [FromBody] AddUsuarioDTO user)
        {
            return Ok(await _cursoManager.addDocente(id, user));
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AddCursoDTO curso)
        {
            return Ok(await _cursoManager.edit(id, curso));
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _cursoManager.delete(id));
        }

        [Authorize(Roles = "usuario")]
        [HttpPost("matricula")]
        public IActionResult Post([FromBody] DTMatricula matricula)
        {
            return Ok(_cursoManager.matricularse(matricula));
        }

        [HttpDelete("matricula")]
        public IActionResult Delete([FromBody] DTMatricula matricula)
        {
            return Ok(_cursoManager.darBajaMatricula(matricula));
        }

        [HttpPost("seccion")]
        public async Task<IActionResult> Post([FromBody] AddSeccionCursoDTO seccion)
        {
            return Ok(await _cursoManager.addSeccion(seccion));
        }

        //? Edit lleva a AddSeccion???

        [HttpPut("seccion/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AddSeccionCursoDTO seccion)
        {
            return Ok(await _cursoManager.addSeccion(seccion));
        }

        [HttpDelete("seccion/{id}")]
        public async Task<IActionResult> DeleteSeccion(int id)
        {
            return Ok(await _cursoManager.deleteSeccion(id));
        }

        [HttpPost("componente")]
        public async Task<IActionResult> Post([FromForm] AddComponenteDTO componente)
        {
            if (Request.Form.Files.Count > 0) return Ok(await _cursoManager.addComponente(componente, Request.Form.Files[0]));
            else return Ok(await _cursoManager.addComponente(componente, null));
        }

        [HttpPut("componente/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AddComponenteDTO componente)
        {
            return Ok(await _cursoManager.editComponente(id,componente));
        }

        [HttpDelete("componente/{id}")]
        public async Task<IActionResult> DeleteComponente(int id)
        {
            return Ok(await _cursoManager.deleteComponente(id));
        }

        [HttpPost("addEntregaTarea")]
        public async Task<IActionResult> Post([FromForm] AddEntregaTareaDTO entregaTarea)
        {

            return Ok(await _cursoManager.addEntregaTarea(entregaTarea, Request.Form.Files[0]));
        }


    }
}
