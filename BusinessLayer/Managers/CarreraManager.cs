using AutoMapper;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using Utilidades.DTOs.Carrera;

namespace BusinessLayer
{
    public class CarreraManager : ICarreraManager
    {
        private readonly IMapper _mapper;
        private readonly MyContext _context;
        public CarreraManager(IMapper mapper, MyContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public ApiResponse<List<GetCarreraDTO>> lists()
        {
            ApiResponse<List<GetCarreraDTO>> response = new ApiResponse<List<GetCarreraDTO>>();
            try
            {
                response.Data = _context.Carreras.Select(c => _mapper.Map<GetCarreraDTO>(c)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 204;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<List<GetCarreraDTO>>> add(AddCarreraDTO carrera)
        {
            ApiResponse<List<GetCarreraDTO>> response = new ApiResponse<List<GetCarreraDTO>>();
            try
            {
                _context.Carreras.Add(_mapper.Map<Carrera>(carrera));
                await _context.SaveChangesAsync();
                response.Data = _context.Carreras.Select(c => _mapper.Map<GetCarreraDTO>(c)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<List<GetCarreraDTO>>> delete(int id)
        {
            ApiResponse<List<GetCarreraDTO>> response = new ApiResponse<List<GetCarreraDTO>>();
            try
            {
                Carrera carrera = _context.Carreras.First(c => c.Id == id);
                _context.Carreras.Remove(carrera);
                await _context.SaveChangesAsync();
                response.Data = _context.Carreras.Select(c => _mapper.Map<GetCarreraDTO>(c)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<GetCarreraDTO>> get(int id)
        {
            ApiResponse<GetCarreraDTO> response = new ApiResponse<GetCarreraDTO>();
            try
            {
                response.Data = _mapper.Map<GetCarreraDTO>(await _context.Carreras.FirstOrDefaultAsync(c => c.Id == id));
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<GetCarreraDTO>> edit(int id, AddCarreraDTO carrera)
        {
            ApiResponse<GetCarreraDTO> response = new ApiResponse<GetCarreraDTO>();
            try
            {
                Carrera carreraUpdate = _context.Carreras.First(c => c.Id == id);
                carreraUpdate.Nombre = carrera.Nombre;
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCarreraDTO>(carreraUpdate);
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
