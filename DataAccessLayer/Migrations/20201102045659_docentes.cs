using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class docentes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Archivos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Archivos",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_CursoDocente_CursoId",
                table: "CursoDocente",
                column: "CursoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursoDocente");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Archivos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Archivos");
        }
    }
}
