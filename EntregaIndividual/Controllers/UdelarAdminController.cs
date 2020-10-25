using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilidades;

namespace UdelarOnlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UdelarAdminController : ControllerBase
    {
        private readonly IUdelarAdminManager _uadminManager;

        public UdelarAdminController(IUdelarAdminManager uadminManager)
        {
            _uadminManager = uadminManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_uadminManager.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromBody] IdPassword usuario)
        {
            return Ok(_uadminManager.Check(usuario));
        }

        [HttpPost]
        public IActionResult Post([FromBody] IdPassword usuario)
        {
            return Ok(_uadminManager.AddKey(usuario));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return Ok(_uadminManager.DeleteKey(id));
        }
    }
}
