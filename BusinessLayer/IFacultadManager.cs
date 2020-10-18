using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using Utilidades.DTOs.Facultad;

namespace BusinessLayer
{
    public interface IFacultadManager
    {
        ApiResponse<List<GetFacultadDTO>> lists();
        Task<ApiResponse<List<GetFacultadDTO>>> add(AddFacultadDTO facultad);
        Task<ApiResponse<List<GetFacultadDTO>>> delete(int id);
        Task<ApiResponse<GetFacultadDTO>> get(int id);
        Task<ApiResponse<GetFacultadDTO>> edit(int id, AddFacultadDTO facultad);
        ApiResponse<List<DTUsuariosXFacultad>> UsuariosXFacultad();
    }
}
