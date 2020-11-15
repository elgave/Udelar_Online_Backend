using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using Utilidades.DTOs.Componente;
using Utilidades.DTOs.Curso;
using Utilidades.DTOs.EntregaTarea;
using Utilidades.DTOs.SeccionCurso;
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
        Task<ApiResponse<GetCursoDTO>> renombrar(int id, AddCursoDTO curso);
        Task<ApiResponse<bool>> matricularse(DTMatricula matricula);

        Task<ApiResponse<bool>> darBajaMatricula(DTMatricula matricula);

        Task<ApiResponse<GetCursoDTO>> addDocente(int id, AddUsuarioDTO user);

        Task<ApiResponse<AddSeccionCursoDTO>> addSeccion(AddSeccionCursoDTO seccion);

        Task<ApiResponse<GetSeccionCursoDTO>> editSeccion(int idSeccion, AddSeccionCursoDTO seccion);

        Task<ApiResponse<List<GetSeccionCursoDTO>>> deleteSeccion(int idSeccion);

        Task<ApiResponse<AddComponenteDTO>> addComponente(AddComponenteDTO componente, IFormFile archivo);

        Task<ApiResponse<GetComponenteDTO>> editComponente(int idComponente, AddComponenteDTO componente);

        Task<ApiResponse<List<GetComponenteDTO>>> deleteComponente(int idComponente);
        Task<ApiResponse<AddEntregaTareaDTO>> addEntregaTarea(AddEntregaTareaDTO entregaTarea, IFormFile archivoEntrega);

        



    }
}
