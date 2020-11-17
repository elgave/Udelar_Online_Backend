using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class usuarioNota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Nota",
                table: "UsuarioCurso",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "comentario",
                table: "UsuarioCurso",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nota",
                table: "UsuarioCurso");

            migrationBuilder.DropColumn(
                name: "comentario",
                table: "UsuarioCurso");
        }
    }
}
