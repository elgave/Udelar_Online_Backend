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
using Utilidades.DTOs.Template;
using Utilidades.DTOs.Template.SeccionTemplate;
using Utilidades.DTOs.Usuario;
using Utilidades.DTOs.UsuarioCurso;

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

        [Authorize(Roles = "admin,administrador")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCursoDTO curso)
        {
            return Ok(await _cursoManager.add(curso));
        }

        [Authorize(Roles = "admin,administrador,docente")]
        [HttpPost("{id}")]
        public async Task<IActionResult> AddDocente(int id, [FromBody] AddUsuarioDTO user)
        {
            return Ok(await _cursoManager.addDocente(id, user));
        }

        [Authorize(Roles = "admin,administrador,docente")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AddCursoDTO curso)
        {
            return Ok(await _cursoManager.edit(id, curso));
        }

        [Authorize(Roles = "admin,administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _cursoManager.delete(id));
        }

        [Authorize(Roles = "estudiante")]
        [HttpPost("matricularse")]
        public IActionResult Post([FromBody] DTMatricula matricula)
        {
            return Ok(_cursoManager.matricularse(matricula));
        }

        [Authorize(Roles = "administrador,docente")]
        [HttpDelete("matricula")]
        public IActionResult Delete([FromBody] DTMatricula matricula)
        {
            return Ok(_cursoManager.darBajaMatricula(matricula));
        }

        [Authorize(Roles = "docente")]
        [HttpPost("seccion")]
        public async Task<IActionResult> Post([FromBody] AddSeccionCursoDTO seccion)
        {
            return Ok(await _cursoManager.addSeccion(seccion));
        }

        [Authorize(Roles = "docente")]
        [HttpPut("seccion/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AddSeccionCursoDTO seccion)
        {
            return Ok(await _cursoManager.editSeccion(id, seccion));
        }

        [Authorize(Roles = "docente")]
        [HttpDelete("seccion/{id}")]
        public async Task<IActionResult> DeleteSeccion(int id)
        {
            return Ok(await _cursoManager.deleteSeccion(id));
        }

        [Authorize(Roles = "docente")]
        [HttpPost("componente")]
        public async Task<IActionResult> Post([FromForm] AddComponenteDTO componente)
        {
            if (Request.Form.Files.Count > 0) return Ok(await _cursoManager.addComponente(componente, Request.Form.Files[0]));
            else return Ok(await _cursoManager.addComponente(componente, null));
        }

        [Authorize(Roles = "docente")]
        [HttpPut("componente/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AddComponenteDTO componente)
        {
            return Ok(await _cursoManager.editComponente(id,componente));
        }

        [Authorize(Roles = "docente")]
        [HttpDelete("componente/{id}")]
        public async Task<IActionResult> DeleteComponente(int id)
        {
            return Ok(await _cursoManager.deleteComponente(id));
        }

        [Authorize(Roles = "estudiante")]
        [HttpPost("addEntregaTarea")]
        public async Task<IActionResult> Post([FromForm] AddEntregaTareaDTO entregaTarea)
        {

            return Ok(await _cursoManager.addEntregaTarea(entregaTarea, Request.Form.Files[0]));
        }

       // [Authorize(Roles = "docente")]
        [HttpPost("Calificacion")]
        public async Task<IActionResult> Post([FromBody] AddUsuarioNotaDTO usuarioNota)
        {
            return Ok(await _cursoManager.addUsuarioNota(usuarioNota));
        }

        [HttpGet ("idCurso")]
        public async Task<IActionResult> GetUsuarioNotas(int idCurso)
        {
            return Ok( _cursoManager.getUsuariosNota(idCurso));
        }
    }
}
