using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Utilidades;
using Utilidades.DTOs.Curso;

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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddCursoDTO curso)
        {
            return Ok(await _cursoManager.add(curso));
        }

        [HttpPost("matricularse")]
        public IActionResult Post([FromBody] DTMatricula matricula)
        {
            return Ok(_cursoManager.matricularse(matricula));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AddCursoDTO curso)
        {
            return Ok(await _cursoManager.edit(id, curso));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _cursoManager.delete(id));
        }
    }
}
