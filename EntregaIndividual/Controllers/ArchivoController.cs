using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilidades.DTOs.Archivo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UdelarOnlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoController : ControllerBase
    {

        private readonly IArchivoManager _archivoManager;


        public ArchivoController(IArchivoManager archivoManager)
        {
            _archivoManager = archivoManager;
        }
        // GET: api/<ArchivoController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }


        // POST api/<ArchivoController>
        [HttpPost]
        public IActionResult Post(IFormFile file, int cursoId, string tipo, string usuarioId)
        {   
            
            return Ok(_archivoManager.Add(file,cursoId, tipo, usuarioId));
        }
    }
}
