using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class facultadpropiedades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Facultades",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Facultades",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Facultades");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Facultades");
        }
    }
}
