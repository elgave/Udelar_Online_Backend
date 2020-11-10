using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Encuestas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(nullable: true),
                    Fecha = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encuestas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Facultades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facultades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Preguntas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texto = table.Column<string>(nullable: true),
                    EncuestaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preguntas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Preguntas_Encuestas_EncuestaId",
                        column: x => x.EncuestaId,
                        principalTable: "Encuestas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacultadId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    CantCreditos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cursos_Facultades_FacultadId",
                        column: x => x.FacultadId,
                        principalTable: "Facultades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Cedula = table.Column<string>(nullable: false),
                    FacultadId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Apellido = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Contrasena = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => new { x.Cedula, x.FacultadId });
                    table.ForeignKey(
                        name: "FK_Usuarios_Facultades_FacultadId",
                        column: x => x.FacultadId,
                        principalTable: "Facultades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Respuestas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texto = table.Column<string>(nullable: true),
                    PreguntaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuestas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Respuestas_Preguntas_PreguntaId",
                        column: x => x.PreguntaId,
                        principalTable: "Preguntas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EncuestaCursos",
                columns: table => new
                {
                    IdEncuesta = table.Column<int>(nullable: false),
                    IdCurso = table.Column<int>(nullable: false),
                    Fecha = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncuestaCursos", x => new { x.IdCurso, x.IdEncuesta });
                    table.ForeignKey(
                        name: "FK_EncuestaCursos_Cursos_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EncuestaCursos_Encuestas_IdEncuesta",
                        column: x => x.IdEncuesta,
                        principalTable: "Encuestas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SeccionesCursos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(nullable: true),
                    CursoId = table.Column<int>(nullable: false),
                    Indice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeccionesCursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeccionesCursos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CursoDocente",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(nullable: false),
                    FacultadId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoDocente", x => new { x.UsuarioId, x.FacultadId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_CursoDocente_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CursoDocente_Usuarios_UsuarioId_FacultadId",
                        columns: x => new { x.UsuarioId, x.FacultadId },
                        principalTable: "Usuarios",
                        principalColumns: new[] { "Cedula", "FacultadId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EncuestaUsuarios",
                columns: table => new
                {
                    IdEncuesta = table.Column<int>(nullable: false),
                    Cedula = table.Column<string>(nullable: false),
                    FacultadId = table.Column<int>(nullable: false),
                    Fecha = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncuestaUsuarios", x => new { x.IdEncuesta, x.Cedula });
                    table.ForeignKey(
                        name: "FK_EncuestaUsuarios_Encuestas_IdEncuesta",
                        column: x => x.IdEncuesta,
                        principalTable: "Encuestas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EncuestaUsuarios_Usuarios_Cedula_FacultadId",
                        columns: x => new { x.Cedula, x.FacultadId },
                        principalTable: "Usuarios",
                        principalColumns: new[] { "Cedula", "FacultadId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioCurso",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(nullable: false),
                    FacultadId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioCurso", x => new { x.UsuarioId, x.FacultadId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_UsuarioCurso_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioCurso_Usuarios_UsuarioId_FacultadId",
                        columns: x => new { x.UsuarioId, x.FacultadId },
                        principalTable: "Usuarios",
                        principalColumns: new[] { "Cedula", "FacultadId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRol",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(nullable: false),
                    FacultadId = table.Column<int>(nullable: false),
                    RolId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRol", x => new { x.UsuarioId, x.FacultadId, x.RolId });
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Usuarios_UsuarioId_FacultadId",
                        columns: x => new { x.UsuarioId, x.FacultadId },
                        principalTable: "Usuarios",
                        principalColumns: new[] { "Cedula", "FacultadId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Componentes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeccionCursoId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    Indice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Componentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Componentes_SeccionesCursos_SeccionCursoId",
                        column: x => x.SeccionCursoId,
                        principalTable: "SeccionesCursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comunicados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponenteId = table.Column<int>(nullable: false),
                    Titulo = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comunicados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comunicados_Componentes_ComponenteId",
                        column: x => x.ComponenteId,
                        principalTable: "Componentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContenedoresTareas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponenteId = table.Column<int>(nullable: false),
                    FechaCierre = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContenedoresTareas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContenedoresTareas_Componentes_ComponenteId",
                        column: x => x.ComponenteId,
                        principalTable: "Componentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntregaTarea",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(nullable: true),
                    FacultadId = table.Column<int>(nullable: false),
                    ContenedorTareaId = table.Column<int>(nullable: false),
                    Calificacion = table.Column<string>(nullable: true),
                    FechaEntrega = table.Column<DateTime>(nullable: false),
                    ContenedorTardeaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntregaTarea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntregaTarea_ContenedoresTareas_ContenedorTardeaId",
                        column: x => x.ContenedorTardeaId,
                        principalTable: "ContenedoresTareas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntregaTarea_Usuarios_UsuarioId_FacultadId",
                        columns: x => new { x.UsuarioId, x.FacultadId },
                        principalTable: "Usuarios",
                        principalColumns: new[] { "Cedula", "FacultadId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Archivos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    Ubicacion = table.Column<string>(nullable: true),
                    ComponenteId = table.Column<int>(nullable: true),
                    EntregaTareaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Archivos_Componentes_ComponenteId",
                        column: x => x.ComponenteId,
                        principalTable: "Componentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Archivos_EntregaTarea_EntregaTareaId",
                        column: x => x.EntregaTareaId,
                        principalTable: "EntregaTarea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Archivos_ComponenteId",
                table: "Archivos",
                column: "ComponenteId",
                unique: true,
                filter: "[ComponenteId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Archivos_EntregaTareaId",
                table: "Archivos",
                column: "EntregaTareaId",
                unique: true,
                filter: "[EntregaTareaId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Componentes_SeccionCursoId",
                table: "Componentes",
                column: "SeccionCursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comunicados_ComponenteId",
                table: "Comunicados",
                column: "ComponenteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContenedoresTareas_ComponenteId",
                table: "ContenedoresTareas",
                column: "ComponenteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CursoDocente_CursoId",
                table: "CursoDocente",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_FacultadId",
                table: "Cursos",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaCursos_IdEncuesta",
                table: "EncuestaCursos",
                column: "IdEncuesta");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaUsuarios_Cedula_FacultadId",
                table: "EncuestaUsuarios",
                columns: new[] { "Cedula", "FacultadId" });

            migrationBuilder.CreateIndex(
                name: "IX_EntregaTarea_ContenedorTardeaId",
                table: "EntregaTarea",
                column: "ContenedorTardeaId");

            migrationBuilder.CreateIndex(
                name: "IX_EntregaTarea_UsuarioId_FacultadId",
                table: "EntregaTarea",
                columns: new[] { "UsuarioId", "FacultadId" });

            migrationBuilder.CreateIndex(
                name: "IX_Preguntas_EncuestaId",
                table: "Preguntas",
                column: "EncuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_PreguntaId",
                table: "Respuestas",
                column: "PreguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_SeccionesCursos_CursoId",
                table: "SeccionesCursos",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCurso_CursoId",
                table: "UsuarioCurso",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_RolId",
                table: "UsuarioRol",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_FacultadId",
                table: "Usuarios",
                column: "FacultadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Archivos");

            migrationBuilder.DropTable(
                name: "Comunicados");

            migrationBuilder.DropTable(
                name: "CursoDocente");

            migrationBuilder.DropTable(
                name: "EncuestaCursos");

            migrationBuilder.DropTable(
                name: "EncuestaUsuarios");

            migrationBuilder.DropTable(
                name: "Respuestas");

            migrationBuilder.DropTable(
                name: "UsuarioCurso");

            migrationBuilder.DropTable(
                name: "UsuarioRol");

            migrationBuilder.DropTable(
                name: "EntregaTarea");

            migrationBuilder.DropTable(
                name: "Preguntas");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "ContenedoresTareas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Encuestas");

            migrationBuilder.DropTable(
                name: "Componentes");

            migrationBuilder.DropTable(
                name: "SeccionesCursos");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Facultades");
        }
    }
}
