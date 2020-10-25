using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using Utilidades.DTOs.Usuario;

namespace BusinessLayer
{
    public interface IUsuarioManager
    {
        ApiResponse<List<GetUsuarioDTO>> lists();
        Task<ApiResponse<List<GetUsuarioDTO>>> add(AddUsuarioDTO usuario);
        Task<ApiResponse<List<GetUsuarioDTO>>> delete(string cedula, int idFacultad);
        Task<ApiResponse<GetUsuarioDTO>> get(string cedula, int idFacultad);
        Task<ApiResponse<GetUsuarioDTO>> edit(AddUsuarioDTO usuario);
        Task<ApiResponse<GetUsuarioDTO>> login(LoginUser usuario);
    }
}
