using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class EstructuraComponentes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivos_Cursos_CursoId",
                table: "Archivos");

            migrationBuilder.DropIndex(
                name: "IX_Archivos_CursoId",
                table: "Archivos");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "Archivos");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Archivos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Archivos");

            migrationBuilder.AddColumn<int>(
                name: "ComponenteId",
                table: "Archivos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EntregaTareaId",
                table: "Archivos",
                nullable: true);

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
                name: "IX_EntregaTarea_ContenedorTardeaId",
                table: "EntregaTarea",
                column: "ContenedorTardeaId");

            migrationBuilder.CreateIndex(
                name: "IX_EntregaTarea_UsuarioId_FacultadId",
                table: "EntregaTarea",
                columns: new[] { "UsuarioId", "FacultadId" });

            migrationBuilder.CreateIndex(
                name: "IX_SeccionesCursos_CursoId",
                table: "SeccionesCursos",
                column: "CursoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Archivos_Componentes_ComponenteId",
                table: "Archivos",
                column: "ComponenteId",
                principalTable: "Componentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Archivos_EntregaTarea_EntregaTareaId",
                table: "Archivos",
                column: "EntregaTareaId",
                principalTable: "EntregaTarea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivos_Componentes_ComponenteId",
                table: "Archivos");

            migrationBuilder.DropForeignKey(
                name: "FK_Archivos_EntregaTarea_EntregaTareaId",
                table: "Archivos");

            migrationBuilder.DropTable(
                name: "Comunicados");

            migrationBuilder.DropTable(
                name: "EntregaTarea");

            migrationBuilder.DropTable(
                name: "ContenedoresTareas");

            migrationBuilder.DropTable(
                name: "Componentes");

            migrationBuilder.DropTable(
                name: "SeccionesCursos");

            migrationBuilder.DropIndex(
                name: "IX_Archivos_ComponenteId",
                table: "Archivos");

            migrationBuilder.DropIndex(
                name: "IX_Archivos_EntregaTareaId",
                table: "Archivos");

            migrationBuilder.DropColumn(
                name: "ComponenteId",
                table: "Archivos");

            migrationBuilder.DropColumn(
                name: "EntregaTareaId",
                table: "Archivos");

            migrationBuilder.AddColumn<int>(
                name: "CursoId",
                table: "Archivos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Archivos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Archivos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Archivos_CursoId",
                table: "Archivos",
                column: "CursoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Archivos_Cursos_CursoId",
                table: "Archivos",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
