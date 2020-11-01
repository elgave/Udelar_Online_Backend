using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using Utilidades.DTOs.Carrera;

namespace BusinessLayer
{
    public interface ICarreraManager
    {
        ApiResponse<List<GetCarreraDTO>> lists();
        Task<ApiResponse<List<GetCarreraDTO>>> add(AddCarreraDTO carrera);
        Task<ApiResponse<List<GetCarreraDTO>>> delete(int id);
        Task<ApiResponse<GetCarreraDTO>> get(int id);
        Task<ApiResponse<GetCarreraDTO>> edit(int id, AddCarreraDTO carrera);

    }
}
