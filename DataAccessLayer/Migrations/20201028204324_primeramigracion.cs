using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class primeramigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facultades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true)
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
                name: "Encuestas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoId = table.Column<int>(nullable: false),
                    Titulo = table.Column<string>(nullable: true),
                    Fecha = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encuestas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encuestas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
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
                    RolId = table.Column<int>(nullable: false),
                    UsuarioCedula = table.Column<string>(nullable: true),
                    UsuarioFacultadId = table.Column<int>(nullable: true)
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
                        name: "FK_UsuarioRol_Usuarios_UsuarioCedula_UsuarioFacultadId",
                        columns: x => new { x.UsuarioCedula, x.UsuarioFacultadId },
                        principalTable: "Usuarios",
                        principalColumns: new[] { "Cedula", "FacultadId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Usuarios_UsuarioId_FacultadId",
                        columns: x => new { x.UsuarioId, x.FacultadId },
                        principalTable: "Usuarios",
                        principalColumns: new[] { "Cedula", "FacultadId" },
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Respuestas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texto = table.Column<string>(nullable: true),
                    EncuestaId = table.Column<int>(nullable: false),
                    PreguntaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuestas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Respuestas_Encuestas_EncuestaId",
                        column: x => x.EncuestaId,
                        principalTable: "Encuestas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Respuestas_Preguntas_PreguntaId",
                        column: x => x.PreguntaId,
                        principalTable: "Preguntas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_FacultadId",
                table: "Cursos",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_Encuestas_CursoId",
                table: "Encuestas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Preguntas_EncuestaId",
                table: "Preguntas",
                column: "EncuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_EncuestaId",
                table: "Respuestas",
                column: "EncuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_PreguntaId",
                table: "Respuestas",
                column: "PreguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCurso_CursoId",
                table: "UsuarioCurso",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_RolId",
                table: "UsuarioRol",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_UsuarioCedula_UsuarioFacultadId",
                table: "UsuarioRol",
                columns: new[] { "UsuarioCedula", "UsuarioFacultadId" });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_FacultadId",
                table: "Usuarios",
                column: "FacultadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Respuestas");

            migrationBuilder.DropTable(
                name: "UsuarioCurso");

            migrationBuilder.DropTable(
                name: "UsuarioRol");

            migrationBuilder.DropTable(
                name: "Preguntas");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Encuestas");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Facultades");
        }
    }
}
