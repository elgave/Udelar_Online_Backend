using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using Utilidades.DTOs.Curso;
using Utilidades.DTOs.Usuario;

namespace BusinessLayer
{
    public interface ICursoManager
    {
        ApiResponse<List<GetCursoDTO>> lists();
        Task<ApiResponse<List<GetCursoDTO>>> add(AddCursoDTO curso);
        Task<ApiResponse<List<GetCursoDTO>>> delete(int id);
        Task<ApiResponse<GetCursoDTO>> get(int id);
        Task<ApiResponse<GetCursoDTO>> edit(int id, AddCursoDTO curso);
        ApiResponse<bool> matricularse(DTMatricula matricula);
        Task<ApiResponse<GetCursoDTO>> addDocente(int id, AddUsuarioDTO user);
    }
}
