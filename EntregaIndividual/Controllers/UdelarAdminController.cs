using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace UdelarOnlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UdelarAdminController : ControllerBase
    {
        private readonly IUdelarAdminManager _uadminManager;
        private readonly IConfiguration _configuration;

        public UdelarAdminController(IUdelarAdminManager uadminManager, IConfiguration configuration)
        {
            _uadminManager = uadminManager;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_uadminManager.GetAll());
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] IdPassword usuario)
        {
            ApiResponse<bool> usu = _uadminManager.Login(usuario);
            ApiResponse<string> response = new ApiResponse<string>();
            if (usu.Data)
            {
                var token = GenerarToken(usuario);
                response.Data = new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                response.Success = false;
                response.Status = 204;
                response.Message = "Usuario y/o password incorrectos";
            }
            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Post([FromBody] IdPassword usuario)
        {
            return Ok(_uadminManager.AddKey(usuario));
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return Ok(_uadminManager.DeleteKey(id));
        }

        private JwtSecurityToken GenerarToken(IdPassword login)
        {
            string ValidIssuer = _configuration["ApiAuth:Issuer"];
            string ValidAudience = _configuration["ApiAuth:Audience"];
            SymmetricSecurityKey IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApiAuth:SecretKey"]));

            DateTime dtFechaExpiraToken;
            DateTime now = DateTime.Now;
            dtFechaExpiraToken = DateTime.UtcNow.AddHours(24);

            var claims = new[]
            {
                new Claim(Constantes.JWT_CLAIM_USUARIO, login.Id),
                new Claim(ClaimTypes.Role, "admin")
            };

            return new JwtSecurityToken
            (
                issuer: ValidIssuer,
                audience: ValidAudience,
                claims: claims,
                expires: dtFechaExpiraToken,
                notBefore: now,
                signingCredentials: new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }
    }
}
