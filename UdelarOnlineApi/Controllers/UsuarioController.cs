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
    public class UsuarioController : ControllerBase
    {

        private UsuarioManager _usuarioManager;

        public UsuarioController()
        {
            _usuarioManager = new UsuarioManager();
        }
        // GET: api/<UsuarioController>
        [HttpGet]
        public IEnumerable<DTUsuario> Get()
        {
            var usuarios = _usuarioManager.lists();
            return usuarios;
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{cedula}/{idFacultad}/{tipo}")]
        public DTUsuario Get(string cedula, int idFacultad, string tipo)
        {
            var usuario = _usuarioManager.get(cedula,idFacultad,tipo);

            return usuario;
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public ActionResult Post([FromBody] DTUsuario usuario)
        {
            try
            {
                var usu = _usuarioManager.get(usuario.Cedula,usuario.IdFacultad, usuario.Tipo);

                if (usu == null)
                {
                    _usuarioManager.add(usuario);
                    return Ok();
                }
                else
                    return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("")]
        public ActionResult Put([FromBody] DTUsuario usuario)
        {
            try
            {
                _usuarioManager.edit(usuario);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{cedula}/{idFacultad}/{tipo}")]
        public ActionResult Delete(string cedula,int idFacultad,string tipo)
        {
            try
            {
                _usuarioManager.delete(cedula, idFacultad,tipo);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
