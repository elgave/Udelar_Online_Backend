using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class rolesdeusuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Usuarios");

            migrationBuilder.AddColumn<int>(
                name: "FacultadId",
                table: "Usuarios",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                columns: new[] { "Cedula", "IdFacultad" });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRol = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRol",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(nullable: false),
                    IdFacultad = table.Column<int>(nullable: false),
                    IdRol = table.Column<int>(nullable: false),
                    UsuarioCedula = table.Column<string>(nullable: true),
                    UsuarioIdFacultad = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRol", x => new { x.IdRol, x.IdFacultad, x.IdUsuario });
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Usuarios_UsuarioCedula_UsuarioIdFacultad",
                        columns: x => new { x.UsuarioCedula, x.UsuarioIdFacultad },
                        principalTable: "Usuarios",
                        principalColumns: new[] { "Cedula", "IdFacultad" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_FacultadId",
                table: "Usuarios",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_UsuarioCedula_UsuarioIdFacultad",
                table: "UsuarioRol",
                columns: new[] { "UsuarioCedula", "UsuarioIdFacultad" });

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Facultades_FacultadId",
                table: "Usuarios",
                column: "FacultadId",
                principalTable: "Facultades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Facultades_FacultadId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UsuarioRol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_FacultadId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "FacultadId",
                table: "Usuarios");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Usuarios",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                columns: new[] { "Cedula", "IdFacultad", "Tipo" });
        }
    }
}
