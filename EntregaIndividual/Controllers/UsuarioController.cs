using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Utilidades;
using Utilidades.DTOs.Usuario;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EntregaIndividual.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioManager _usuarioManager;

        public UsuarioController(IUsuarioManager usuariomanager)
        {
            _usuarioManager = usuariomanager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_usuarioManager.lists());
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUser usuario)
        {
            return Ok(await _usuarioManager.login(usuario));
        }

        [HttpGet("{cedula}/{idFacultad}")]
        public async Task<IActionResult> Get(string cedula, int idFacultad)
        {
            return Ok(await _usuarioManager.get(cedula, idFacultad));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddUsuarioDTO usuario)
        {
            return Ok(await _usuarioManager.add(usuario));
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("")]
        public async Task<IActionResult> Put([FromBody] AddUsuarioDTO usuario)
        {
            return Ok(await _usuarioManager.edit(usuario));
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{cedula}/{idFacultad}")]
        public async Task<IActionResult> Delete(string cedula, int idFacultad)
        {
            ApiResponse<List<GetUsuarioDTO>> response = await _usuarioManager.delete(cedula, idFacultad);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }
    }
}
