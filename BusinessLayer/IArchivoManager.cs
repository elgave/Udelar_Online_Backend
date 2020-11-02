using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Utilidades;
using Utilidades.DTOs.Archivo;

namespace BusinessLayer
{
    public interface IArchivoManager
    {
        ApiResponse<List<GetArchivoDTO>> GetAllXCurso(int cursoId);
        ApiResponse<GetArchivoDTO> Add(IFormFile file, int cursoId, string tipo, string usuarioId);
    }
}
