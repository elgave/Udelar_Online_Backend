using AutoMapper;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using Utilidades.DTOs.Curso;
using Utilidades.DTOs.Usuario;

namespace BusinessLayer
{
    public class CursoManager : ICursoManager
    {
        private readonly IMapper _mapper;
        private readonly MyContext _context;
        public CursoManager(IMapper mapper, MyContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public ApiResponse<List<GetCursoDTO>> lists()
        {
            ApiResponse<List<GetCursoDTO>> response = new ApiResponse<List<GetCursoDTO>>();

            try
            {
                response.Data = _context.Cursos
                    .Include(c => c.CursosDocentes).ThenInclude(cd => cd.Usuario)
                    .Include(c => c.UsuariosCursos).ThenInclude(uc => uc.Usuario)
                    .Select(c => _mapper.Map<GetCursoDTO>(c)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 204;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<List<GetCursoDTO>>> add(AddCursoDTO curso)
        {
            ApiResponse<List<GetCursoDTO>> response = new ApiResponse<List<GetCursoDTO>>();
            try
            {
                _context.Cursos.Add(_mapper.Map<Curso>(curso));
                await _context.SaveChangesAsync();
                response.Data = _context.Cursos.Select(c => _mapper.Map<GetCursoDTO>(c)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }
            return response;
        }
        public async Task<ApiResponse<List<GetCursoDTO>>> delete(int id)
        {
            ApiResponse<List<GetCursoDTO>> response = new ApiResponse<List<GetCursoDTO>>();
            try
            {
                Curso curso = _context.Cursos.First(c => c.Id == id);
                _context.Cursos.Remove(curso);
                await _context.SaveChangesAsync();
                response.Data = _context.Cursos.Select(c => _mapper.Map<GetCursoDTO>(c)).ToList();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ApiResponse<GetCursoDTO>> get(int id)
        {
            ApiResponse<GetCursoDTO> response = new ApiResponse<GetCursoDTO>();
            try
            {
                response.Data = _mapper.Map<GetCursoDTO>(
                    await _context.Cursos
                    .Include(c => c.CursosDocentes).ThenInclude(cd => cd.Usuario)
                    .Include(c => c.UsuariosCursos).ThenInclude(uc => uc.Usuario)
                    .FirstOrDefaultAsync(c => c.Id == id)
                );
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }
            return response;
        }
        public async Task<ApiResponse<GetCursoDTO>> edit(int id, AddCursoDTO curso)
        {
            ApiResponse<GetCursoDTO> response = new ApiResponse<GetCursoDTO>();
            try
            {
                Curso cursoUpdate = _context.Cursos.SingleOrDefault(c => c.Id == id);
                cursoUpdate.Nombre = curso.Nombre;
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCursoDTO>(cursoUpdate);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 404;
                response.Message = e.Message;
            }

            return response;
        }

        public ApiResponse<bool> matricularse(DTMatricula matricula)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();
            IBedeliasApi _bedeliasApi = new BedeliasApi();
            response.Data = _bedeliasApi.MatricularseACurso(matricula);
            return response;
        }

        public async Task<ApiResponse<GetCursoDTO>> addDocente(int id, AddUsuarioDTO user)
        {
            ApiResponse<GetCursoDTO> response = new ApiResponse<GetCursoDTO>();
            try
            {
                CursoDocente cd = new CursoDocente();
                cd.CursoId = id;
                cd.FacultadId = user.FacultadId;
                cd.UsuarioId = user.Cedula;
                _context.CursoDocente.Add(cd);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCursoDTO>(_context.Cursos.SingleOrDefault(c => c.Id == id));
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }

            return response;
        }
    }
}
