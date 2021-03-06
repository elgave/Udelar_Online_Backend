﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utilidades;
using Utilidades.DTOs.Calendario;
using Utilidades.DTOs.Componente;
using Utilidades.DTOs.Curso;
using Utilidades.DTOs.EntregaTarea;
using Utilidades.DTOs.SeccionCurso;
using Utilidades.DTOs.Template;
using Utilidades.DTOs.Template.SeccionTemplate;
using Utilidades.DTOs.Usuario;
using Utilidades.DTOs.UsuarioCurso;

namespace BusinessLayer
{
    public interface ICursoManager
    {
        ApiResponse<List<GetCursoDTO>> lists();
        Task<ApiResponse<List<GetCursoDTO>>> add(AddCursoDTO curso);
        Task<ApiResponse<List<GetCursoDTO>>> delete(int id);
        Task<ApiResponse<GetCursoDTO>> get(int id);
        Task<ApiResponse<GetCursoDTO>> edit(int id, AddCursoDTO curso);
        Task<ApiResponse<bool>> matricularse(DTMatricula matricula);

        Task<ApiResponse<bool>> darBajaMatricula(DTMatricula matricula);

        Task<ApiResponse<GetCursoDTO>> addDocente(int id, AddUsuarioDTO user);

        Task<ApiResponse<AddSeccionCursoDTO>> addSeccion(AddSeccionCursoDTO seccion);

        Task<ApiResponse<GetSeccionCursoDTO>> editSeccion(int idSeccion, AddSeccionCursoDTO seccion);

        Task<ApiResponse<List<GetSeccionCursoDTO>>> deleteSeccion(int idSeccion);

        Task<ApiResponse<AddComponenteDTO>> addComponente(AddComponenteDTO componente, IFormFile archivo);

        Task<ApiResponse<GetComponenteDTO>> editComponente(int idComponente, AddComponenteDTO componente);

        Task<ApiResponse<List<GetComponenteDTO>>> deleteComponente(int idComponente);

        ApiResponse<List<GetTemplateDTO>> getAllTemplate();
        Task<ApiResponse<GetTemplateDTO>> getTemplate(int id);
        Task<ApiResponse<AddTemplateDTO>> addTemplate(AddTemplateDTO template);

        Task<ApiResponse<GetTemplateDTO>> editTemplate(int idTemplate, AddTemplateDTO template);

        Task<ApiResponse<List<GetTemplateDTO>>> deleteTemplate(int idTemplate);

        Task<ApiResponse<AddSeccionTemplateDTO>> addSeccionTemplate(AddSeccionTemplateDTO secciontemplate);

        Task<ApiResponse<GetSeccionTemplateDTO>> editSeccionTemplate(int idSeccionTemplate, AddSeccionTemplateDTO seccionTemplate);

        Task<ApiResponse<List<GetSeccionTemplateDTO>>> deleteSeccionTemplate(int idSeccionTemplate);
        Task<ApiResponse<AddEntregaTareaDTO>> addEntregaTarea(AddEntregaTareaDTO entregaTarea, IFormFile archivoEntrega);
        Task<ApiResponse<GetEntregaTareaDTO>> getEntregaTarea(string cedula, int facultadId, int contendorId);
        Task<ApiResponse<GetUsuarioNotaDTO>> addUsuarioNota(AddUsuarioNotaDTO usuarioNota);

        ApiResponse<List<GetUsuarioNotaDTO>> getUsuariosNota(int idCurso);

        Task<ApiResponse<AddEntregaTareaDTO>> calificarTarea(int entregaTareaId, int nota);
        ApiResponse<List<GetEntregaTareaDTO>> getTareasUsuario(int contenedorTareaId);

        Task<ApiResponse<AddFechaCalendarioDTO>> addFecha(AddFechaCalendarioDTO fechaCalendario);
        Task<ApiResponse<GetCalendarioDTO>> getCalendario(int id);






    }
}
