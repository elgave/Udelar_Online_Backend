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
                LiteCollection<IdPassword> collection = _context.NoSql.GetCollection<IdPassword>("usuarios");
                response.Data = collection.FindAll().ToList();
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
                Console.WriteLine(usuario.Id);
                Console.WriteLine(usuario.Password);
                LiteCollection<IdPassword> collection = _context.NoSql.GetCollection<IdPassword>("usuarios");
                collection.Insert(new IdPassword(usuario.Id, BCrypt.Net.BCrypt.HashPassword(usuario.Password)));
                collection.EnsureIndex(x => x.Id);
                response.Data = collection.FindAll().ToList();
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
                LiteCollection<IdPassword> collection = _context.NoSql.GetCollection<IdPassword>("usuarios");
                collection.Delete(id);
                response.Data = collection.FindAll().ToList();
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
                LiteCollection<IdPassword> collection = _context.NoSql.GetCollection<IdPassword>("usuarios");
                IdPassword user = collection.FindOne(x => x.Id == usuario.Id);
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
