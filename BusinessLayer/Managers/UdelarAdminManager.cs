using AutoMapper;
using DataAccessLayer;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace BusinessLayer.Managers
{
    public class UdelarAdminManager : IUdelarAdminManager
    {
        private readonly IMapper _mapper;
        private readonly MyContext _context;

        public UdelarAdminManager(IMapper mapper, MyContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public ApiResponse<List<IdPassword>> GetAll()
        {
            ApiResponse<List<IdPassword>> response = new ApiResponse<List<IdPassword>>();
            try
            {
                response.Data = _context.UdelarAdmins.Select(u => _mapper.Map<IdPassword>(u)).ToList();
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public ApiResponse<List<IdPassword>> AddKey(IdPassword usuario)
        {
            ApiResponse<List<IdPassword>> response = new ApiResponse<List<IdPassword>>();
            try
            {
                usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);
                IdPasswordModel user = new IdPasswordModel(usuario.Id, usuario.Password);
                _context.UdelarAdmins.Add(user);
                _context.SaveChanges();
                response.Data = _context.UdelarAdmins.Select(u => _mapper.Map<IdPassword>(u)).ToList();
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public ApiResponse<List<IdPassword>> DeleteKey(string id)
        {
            ApiResponse<List<IdPassword>> response = new ApiResponse<List<IdPassword>>();
            try
            {
                IdPasswordModel usuario = _context.UdelarAdmins.SingleOrDefault(u => u.Id == id);
                _context.UdelarAdmins.Remove(usuario);
                _context.SaveChanges();
                response.Data = _context.UdelarAdmins.Select(u => _mapper.Map<IdPassword>(u)).ToList();
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public ApiResponse<bool> Login(IdPassword usuario)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();
            try
            {
                IdPasswordModel user = _context.UdelarAdmins.SingleOrDefault(u => u.Id == usuario.Id);
                if (user != null)
                {
                    response.Data = BCrypt.Net.BCrypt.Verify(usuario.Password, user.Password);
                }
                else
                {
                    response.Data = false;
                    response.Status = 500;
                    response.Success = false;
                    response.Message = "Usuario no encontrado";
                }
                
            }
            catch (Exception e)
            {
                response.Status = 500;
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }
    }
}
