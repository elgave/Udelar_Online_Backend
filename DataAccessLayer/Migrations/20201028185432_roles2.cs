using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class roles2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuarioId_FacultadId",
                table: "UsuarioRol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioRol",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "FacultadId",
                table: "UsuarioRol");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCedula",
                table: "UsuarioRol",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioFacultadId",
                table: "UsuarioRol",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioRol",
                table: "UsuarioRol",
                columns: new[] { "UsuarioCedula", "UsuarioFacultadId", "RolId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuarioCedula_UsuarioFacultadId",
                table: "UsuarioRol",
                columns: new[] { "UsuarioCedula", "UsuarioFacultadId" },
                principalTable: "Usuarios",
                principalColumns: new[] { "Cedula", "FacultadId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuarioCedula_UsuarioFacultadId",
                table: "UsuarioRol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioRol",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "UsuarioCedula",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "UsuarioFacultadId",
                table: "UsuarioRol");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "UsuarioRol",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FacultadId",
                table: "UsuarioRol",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioRol",
                table: "UsuarioRol",
                columns: new[] { "UsuarioId", "FacultadId", "RolId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuarioId_FacultadId",
                table: "UsuarioRol",
                columns: new[] { "UsuarioId", "FacultadId" },
                principalTable: "Usuarios",
                principalColumns: new[] { "Cedula", "FacultadId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
