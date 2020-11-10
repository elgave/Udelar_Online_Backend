using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class addbooleanEnCurso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ConfirmaBedelia",
                table: "Cursos",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaUsuarios_Cedula_FacultadId",
                table: "EncuestaUsuarios",
                columns: new[] { "Cedula", "FacultadId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncuestaUsuarios");

            migrationBuilder.DropColumn(
                name: "ConfirmaBedelia",
                table: "Cursos");
        }
    }
}
