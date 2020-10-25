using AutoMapper;
using BCrypt.Net;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using Utilidades.DTOs.Usuario;

namespace BusinessLayer
{
    public class UsuarioManager : IUsuarioManager
    {
        private readonly IMapper _mapper;
        private readonly MyContext _context;

        public UsuarioManager(IMapper mapper, MyContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public ApiResponse<List<GetUsuarioDTO>> lists()
        {
            ApiResponse<List<GetUsuarioDTO>> response = new ApiResponse<List<GetUsuarioDTO>>();
            try
            {
                response.Data = _context.Usuarios.Include(u => u.UsuariosCursos).ThenInclude(uc => uc.Curso).Select(u => _mapper.Map<GetUsuarioDTO>(u)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 204;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<ApiResponse<List<GetUsuarioDTO>>> add(AddUsuarioDTO usuario)
        {
            ApiResponse<List<GetUsuarioDTO>> response = new ApiResponse<List<GetUsuarioDTO>>();
            try
            {
                usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);
                _context.Usuarios.Add(_mapper.Map<Usuario>(usuario));
                await _context.SaveChangesAsync();
                response.Data = _context.Usuarios.Select(u => _mapper.Map<GetUsuarioDTO>(u)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<List<GetUsuarioDTO>>> delete(string cedula, int idFacultad)
        {
            ApiResponse<List<GetUsuarioDTO>> response = new ApiResponse<List<GetUsuarioDTO>>();
            try
            {
                Usuario usuario = _context.Usuarios.SingleOrDefault(u => u.Cedula == cedula && u.FacultadId == idFacultad);
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                response.Data = _context.Usuarios.Select(u => _mapper.Map<GetUsuarioDTO>(u)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<GetUsuarioDTO>> get(string cedula, int idFacultad)
        {
            ApiResponse<GetUsuarioDTO> response = new ApiResponse<GetUsuarioDTO>();

            try
            {
                response.Data = _mapper.Map<GetUsuarioDTO>(await _context.Usuarios.Include(u => u.UsuariosCursos).ThenInclude(uc => uc.Curso).FirstOrDefaultAsync(u => u.Cedula == cedula && u.FacultadId == idFacultad));
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }
            return response;

        }
        public async Task<ApiResponse<GetUsuarioDTO>> edit(AddUsuarioDTO usuario)
        {
            ApiResponse<GetUsuarioDTO> response = new ApiResponse<GetUsuarioDTO>();
            try
            {
                Usuario usuarioUpdate = _context.Usuarios.First(u => u.Cedula == usuario.Cedula && u.FacultadId == usuario.FacultadId);
                usuarioUpdate.Nombre = usuario.Nombre;
                usuarioUpdate.Apellido = usuario.Apellido;
                usuarioUpdate.Contrasena = usuario.Contrasena;
                usuarioUpdate.Correo = usuario.Correo;
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetUsuarioDTO>(usuarioUpdate);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<GetUsuarioDTO>> login(LoginUser usuario)
        {
            ApiResponse<GetUsuarioDTO> response = new ApiResponse<GetUsuarioDTO>();

            try
            {
                Usuario user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Cedula == usuario.Cedula);

                bool isValidPassword = BCrypt.Net.BCrypt.Verify(usuario.Password, user.Contrasena);

                if (isValidPassword)
                {
                    response.Data = _mapper.Map<GetUsuarioDTO>(await _context.Usuarios.Include(u => u.UsuariosCursos).ThenInclude(uc => uc.Curso).FirstOrDefaultAsync(u => u.Cedula == user.Cedula && u.FacultadId == user.FacultadId));
                }
                else
                    response.Data = null;

            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }
            return response;
        }
    }
}
