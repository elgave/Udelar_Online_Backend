using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuarioCedula_UsuarioFacultadId",
                table: "UsuarioRol");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioRol_UsuarioCedula_UsuarioFacultadId",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "UsuarioCedula",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "UsuarioFacultadId",
                table: "UsuarioRol");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioCedula",
                table: "UsuarioRol",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioFacultadId",
                table: "UsuarioRol",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_UsuarioCedula_UsuarioFacultadId",
                table: "UsuarioRol",
                columns: new[] { "UsuarioCedula", "UsuarioFacultadId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuarioCedula_UsuarioFacultadId",
                table: "UsuarioRol",
                columns: new[] { "UsuarioCedula", "UsuarioFacultadId" },
                principalTable: "Usuarios",
                principalColumns: new[] { "Cedula", "FacultadId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
