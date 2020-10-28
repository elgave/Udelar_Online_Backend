﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Utilidades;
using Utilidades.DTOs.Usuario;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EntregaIndividual.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioManager _usuarioManager;

        public UsuarioController(IConfiguration configuration,IUsuarioManager usuariomanager)
        {
            _configuration = configuration;
            _usuarioManager = usuariomanager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_usuarioManager.lists());
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUser usuario)
        {
            Task <ApiResponse < GetUsuarioDTO >> usu =  _usuarioManager.login(usuario);
            ApiResponse<string> response = new ApiResponse<string>();
            if (usu.Result.Data != null)
            {
                var token = GenerarToken(usuario);
                response.Data = new JwtSecurityTokenHandler().WriteToken(token);

            }
            else
            {
                response.Success = false;
                response.Status = 204;
                response.Message = "Usuario y/o contrasena incorrectos";
            }

            return Ok(response);

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

        private JwtSecurityToken GenerarToken(LoginUser login)
        {
            string ValidIssuer = _configuration["ApiAuth:Issuer"];
            string ValidAudience = _configuration["ApiAuth:Audience"];
            SymmetricSecurityKey IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApiAuth:SecretKey"]));

            //La fecha de expiracion sera el mismo dia a las 12 de la noche
            DateTime dtFechaExpiraToken;
            DateTime now = DateTime.Now;
            dtFechaExpiraToken = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59, 999);

            //Agregamos los claim nuestros
            var claims = new[]
            {
                new Claim(Constantes.JWT_CLAIM_USUARIO, login.Cedula)
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
