using AutoMapper;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Utilidades;
using Utilidades.DTOs.Archivo;

namespace BusinessLayer.Managers
{
    public class ArchivoManager : IArchivoManager
    {
        private readonly IMapper _mapper;
        private readonly MyContext _context;

        public ArchivoManager(IMapper mapper, MyContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public ApiResponse<GetArchivoDTO> Add(IFormFile file, int cursoId, string tipo, string usuarioId)
        {
            ApiResponse<GetArchivoDTO> response = new ApiResponse<GetArchivoDTO>();
            try
            {
                var filepath = "C:\\Users\\elgav\\Documents\\Udelar_Online_Backend\\Utilidades\\Archivos\\" + file.FileName;

                using (var stream = System.IO.File.Create(filepath))
                {
                    file.CopyToAsync(stream);
                }

                AddArchivoDTO archivo = new AddArchivoDTO();
                archivo.Extension = Path.GetExtension(file.FileName).Substring(1);
                archivo.Nombre = Path.GetFileNameWithoutExtension(file.FileName);
                archivo.CursoId = cursoId;
                archivo.Ubicacion = filepath;
                archivo.Tipo = tipo;
                archivo.UsuarioId = usuarioId;

                _context.Archivos.Add(_mapper.Map<Archivo>(archivo));
                _context.SaveChangesAsync();
                response.Data = _context.Archivos.Select(a => _mapper.Map<GetArchivoDTO>(a)).SingleOrDefault(a => a.Nombre == archivo.Nombre);
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Status = 500;
                response.Message = e.Message;
            }
            return response;
        }

        public ApiResponse<List<GetArchivoDTO>> GetAllXCurso(int cursoId)
        {
            throw new NotImplementedException();
        }
    }
}
