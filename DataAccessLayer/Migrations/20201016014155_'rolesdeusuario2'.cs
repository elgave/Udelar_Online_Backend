using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class rolesdeusuario2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuarioCedula_UsuarioIdFacultad",
                table: "UsuarioRol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioRol",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "IdRol",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "IdFacultad",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "UsuarioRol");

            migrationBuilder.RenameColumn(
                name: "UsuarioIdFacultad",
                table: "UsuarioRol",
                newName: "usuarioIdFacultad");

            migrationBuilder.RenameColumn(
                name: "UsuarioCedula",
                table: "UsuarioRol",
                newName: "usuarioCedula");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioRol_UsuarioCedula_UsuarioIdFacultad",
                table: "UsuarioRol",
                newName: "IX_UsuarioRol_usuarioCedula_usuarioIdFacultad");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UsuarioRol",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "RolIdRol",
                table: "UsuarioRol",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioRol",
                table: "UsuarioRol",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_RolIdRol",
                table: "UsuarioRol",
                column: "RolIdRol");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Roles_RolIdRol",
                table: "UsuarioRol",
                column: "RolIdRol",
                principalTable: "Roles",
                principalColumn: "IdRol",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Usuarios_usuarioCedula_usuarioIdFacultad",
                table: "UsuarioRol",
                columns: new[] { "usuarioCedula", "usuarioIdFacultad" },
                principalTable: "Usuarios",
                principalColumns: new[] { "Cedula", "IdFacultad" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Roles_RolIdRol",
                table: "UsuarioRol");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Usuarios_usuarioCedula_usuarioIdFacultad",
                table: "UsuarioRol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioRol",
                table: "UsuarioRol");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioRol_RolIdRol",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "RolIdRol",
                table: "UsuarioRol");

            migrationBuilder.RenameColumn(
                name: "usuarioIdFacultad",
                table: "UsuarioRol",
                newName: "UsuarioIdFacultad");

            migrationBuilder.RenameColumn(
                name: "usuarioCedula",
                table: "UsuarioRol",
                newName: "UsuarioCedula");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioRol_usuarioCedula_usuarioIdFacultad",
                table: "UsuarioRol",
                newName: "IX_UsuarioRol_UsuarioCedula_UsuarioIdFacultad");

            migrationBuilder.AddColumn<int>(
                name: "IdRol",
                table: "UsuarioRol",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdFacultad",
                table: "UsuarioRol",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "UsuarioRol",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioRol",
                table: "UsuarioRol",
                columns: new[] { "IdRol", "IdFacultad", "IdUsuario" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuarioCedula_UsuarioIdFacultad",
                table: "UsuarioRol",
                columns: new[] { "UsuarioCedula", "UsuarioIdFacultad" },
                principalTable: "Usuarios",
                principalColumns: new[] { "Cedula", "IdFacultad" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
