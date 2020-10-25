﻿using AutoMapper;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using Utilidades.DTOs.Facultad;

namespace BusinessLayer
{
    public class FacultadManager : IFacultadManager
    {
        private readonly IMapper _mapper;
        private readonly MyContext _context;
        public FacultadManager(IMapper mapper, MyContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public ApiResponse<List<GetFacultadDTO>> lists()
        {
            ApiResponse<List<GetFacultadDTO>> response = new ApiResponse<List<GetFacultadDTO>>();
            try
            {
                response.Data = _context.Facultades.Include(f => f.Cursos).Include(f => f.Usuarios).Select(f => _mapper.Map<GetFacultadDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 204;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<List<GetFacultadDTO>>> add(AddFacultadDTO facultad)
        {
            ApiResponse<List<GetFacultadDTO>> response = new ApiResponse<List<GetFacultadDTO>>();
            try
            {
                _context.Facultades.Add(_mapper.Map<Facultad>(facultad));
                await _context.SaveChangesAsync();
                response.Data = _context.Facultades.Select(f => _mapper.Map<GetFacultadDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<List<GetFacultadDTO>>> delete(int id)
        {
            ApiResponse<List<GetFacultadDTO>> response = new ApiResponse<List<GetFacultadDTO>>();
            try
            {
                Facultad facultad = _context.Facultades.First(f => f.Id == id);
                _context.Facultades.Remove(facultad);
                await _context.SaveChangesAsync();
                response.Data = _context.Facultades.Select(f => _mapper.Map<GetFacultadDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<GetFacultadDTO>> get(int id)
        {
            ApiResponse<GetFacultadDTO> response = new ApiResponse<GetFacultadDTO>();
            try
            {
                response.Data = _mapper.Map<GetFacultadDTO>(await _context.Facultades.Include(f => f.Cursos).Include(f => f.Usuarios).FirstOrDefaultAsync(f => f.Id == id));
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            return response;
        }
        public async Task<ApiResponse<GetFacultadDTO>> edit(int id, AddFacultadDTO facultad)
        {
            ApiResponse<GetFacultadDTO> response = new ApiResponse<GetFacultadDTO>();
            try
            {
                Facultad facultadUpdate = _context.Facultades.First(f => f.Id == id);
                facultadUpdate.Nombre = facultad.Nombre;
                //! agregar mas datos al expandir Facultad.cs
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetFacultadDTO>(facultadUpdate);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }

            return response;
        }

        public ApiResponse<List<DTUsuariosXFacultad>> UsuariosXFacultad()
        {
            ApiResponse<List<DTUsuariosXFacultad>> response = new ApiResponse<List<DTUsuariosXFacultad>>();

            List<Usuario> usuarios = _context.Usuarios.ToList();
            List<Facultad> facultades = _context.Facultades.ToList();
            List<DTUsuariosXFacultad> resultado = new List<DTUsuariosXFacultad>();

            foreach (Facultad f in facultades)
            {
                DTUsuariosXFacultad uxf = new DTUsuariosXFacultad(f.Id, f.Nombre, 0);
                resultado.Add(uxf);
            }
            foreach (Usuario u in usuarios)
            {
                foreach (DTUsuariosXFacultad r in resultado)
                {
                    if (u.FacultadId == r.FacultadId)
                    {
                        r.CantUsuarios += 1;
                    }
                }
            }

            response.Data = resultado;
            return response;
        }
    }
}