using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class roles5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuarioCedula1_UsuarioFacultadId1",
                table: "UsuarioRol");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioRol_UsuarioCedula1_UsuarioFacultadId1",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "UsuarioCedula1",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "UsuarioFacultadId1",
                table: "UsuarioRol");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioCedula1",
                table: "UsuarioRol",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioFacultadId1",
                table: "UsuarioRol",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_UsuarioCedula1_UsuarioFacultadId1",
                table: "UsuarioRol",
                columns: new[] { "UsuarioCedula1", "UsuarioFacultadId1" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuarioCedula1_UsuarioFacultadId1",
                table: "UsuarioRol",
                columns: new[] { "UsuarioCedula1", "UsuarioFacultadId1" },
                principalTable: "Usuarios",
                principalColumns: new[] { "Cedula", "FacultadId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
