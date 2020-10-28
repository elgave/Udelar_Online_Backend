using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Roles_IdRol",
                table: "UsuarioRol");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Usuarios_IdUsuario_IdFacultad",
                table: "UsuarioRol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioRol",
                table: "UsuarioRol");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioRol_IdRol",
                table: "UsuarioRol");

            migrationBuilder.AddColumn<string>(
               name: "UsuarioId",
               table: "UsuarioRol",
               nullable: false,
               defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FacultadId",
                table: "UsuarioRol",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "IdFacultad",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "IdRol",
                table: "UsuarioRol");

            migrationBuilder.AddColumn<int>(
                name: "RolId",
                table: "UsuarioRol",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioCedula1",
                table: "UsuarioRol",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioFacultadId1",
                table: "UsuarioRol",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioRol",
                table: "UsuarioRol",
                columns: new[] { "UsuarioId", "FacultadId", "RolId" });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_RolId",
                table: "UsuarioRol",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_UsuarioCedula1_UsuarioFacultadId1",
                table: "UsuarioRol",
                columns: new[] { "UsuarioCedula1", "UsuarioFacultadId1" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Roles_RolId",
                table: "UsuarioRol",
                column: "RolId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuarioCedula1_UsuarioFacultadId1",
                table: "UsuarioRol",
                columns: new[] { "UsuarioCedula1", "UsuarioFacultadId1" },
                principalTable: "Usuarios",
                principalColumns: new[] { "Cedula", "FacultadId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuarioId_FacultadId",
                table: "UsuarioRol",
                columns: new[] { "UsuarioId", "FacultadId" },
                principalTable: "Usuarios",
                principalColumns: new[] { "Cedula", "FacultadId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Roles_RolId",
                table: "UsuarioRol");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuarioCedula1_UsuarioFacultadId1",
                table: "UsuarioRol");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuarioId_FacultadId",
                table: "UsuarioRol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioRol",
                table: "UsuarioRol");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioRol_RolId",
                table: "UsuarioRol");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioRol_UsuarioCedula1_UsuarioFacultadId1",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "FacultadId",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "RolId",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "UsuarioCedula1",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "UsuarioFacultadId1",
                table: "UsuarioRol");

            migrationBuilder.AddColumn<string>(
                name: "IdUsuario",
                table: "UsuarioRol",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdFacultad",
                table: "UsuarioRol",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdRol",
                table: "UsuarioRol",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioRol",
                table: "UsuarioRol",
                columns: new[] { "IdUsuario", "IdFacultad", "IdRol" });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_IdRol",
                table: "UsuarioRol",
                column: "IdRol");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Roles_IdRol",
                table: "UsuarioRol",
                column: "IdRol",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Usuarios_IdUsuario_IdFacultad",
                table: "UsuarioRol",
                columns: new[] { "IdUsuario", "IdFacultad" },
                principalTable: "Usuarios",
                principalColumns: new[] { "Cedula", "FacultadId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
