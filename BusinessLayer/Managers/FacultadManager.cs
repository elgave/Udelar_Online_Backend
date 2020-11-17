using AutoMapper;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                response.Data = _context.Facultades
                    .Include(f => f.Cursos)
                    .Include(f => f.Usuarios).ThenInclude(u => u.UsuariosRoles).ThenInclude(ur => ur.Rol)
                    .Select(f => _mapper.Map<GetFacultadDTO>(f)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 204;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<List<GetFacultadDTO>>> add(AddFacultadDTO facultad, IFormFile icono)
        {
            ApiResponse<List<GetFacultadDTO>> response = new ApiResponse<List<GetFacultadDTO>>();
            try
            {
                _context.Facultades.Add(_mapper.Map<Facultad>(facultad));
                _context.UploadS3(icono, "facultadIcon", facultad.Url+".png");
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
                var facultad2 = await _context.Facultades
                    .Include(f => f.Cursos).ThenInclude(c => c.UsuariosCursos).ThenInclude(uc => uc.Usuario)
                    .Include(f => f.Cursos).ThenInclude(c => c.CursosDocentes).ThenInclude(cd => cd.Usuario)
                    .FirstAsync(f => f.Id == id);

                var facultadusers = await _context.Facultades
                    .Include(f => f.Usuarios).ThenInclude(u => u.UsuariosRoles).ThenInclude(ur => ur.Rol)
                    .FirstAsync(f => f.Id == id);

                facultad2.Usuarios = facultadusers.Usuarios;

                response.Data = _mapper.Map<GetFacultadDTO>(
                    facultad2
                );
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
                facultadUpdate.Color = facultad.Color;
                facultadUpdate.Url = facultad.Url;
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
