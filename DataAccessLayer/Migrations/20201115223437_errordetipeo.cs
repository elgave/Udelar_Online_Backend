using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class errordetipeo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsResponable",
                table: "CursoDocente");

            migrationBuilder.AddColumn<bool>(
                name: "EsResponsable",
                table: "CursoDocente",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsResponsable",
                table: "CursoDocente");

            migrationBuilder.AddColumn<bool>(
                name: "EsResponable",
                table: "CursoDocente",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
