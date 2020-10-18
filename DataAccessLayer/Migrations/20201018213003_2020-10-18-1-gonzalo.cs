using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class _202010181gonzalo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Carreras_CarreraId",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "IdFacultad",
                table: "Carreras");

            migrationBuilder.AlterColumn<int>(
                name: "CarreraId",
                table: "Cursos",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacultadId",
                table: "Cursos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FacultadId",
                table: "Carreras",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UsuarioCurso",
                columns: table => new
                {
                    CursoId = table.Column<string>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false),
                    CursoId1 = table.Column<int>(nullable: true),
                    UsuarioCedula = table.Column<string>(nullable: false),
                    UsuarioFacultadId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioCurso", x => new { x.UsuarioId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_UsuarioCurso_Cursos_CursoId1",
                        column: x => x.CursoId1,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioCurso_Usuarios_UsuarioCedula_UsuarioFacultadId",
                        columns: x => new { x.UsuarioCedula, x.UsuarioFacultadId },
                        principalTable: "Usuarios",
                        principalColumns: new[] { "Cedula", "FacultadId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_FacultadId",
                table: "Cursos",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_FacultadId",
                table: "Carreras",
                column: "FacultadId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCurso_CursoId1",
                table: "UsuarioCurso",
                column: "CursoId1");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCurso_UsuarioCedula_UsuarioFacultadId",
                table: "UsuarioCurso",
                columns: new[] { "UsuarioCedula", "UsuarioFacultadId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Carreras_Facultades_FacultadId",
                table: "Carreras",
                column: "FacultadId",
                principalTable: "Facultades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Carreras_CarreraId",
                table: "Cursos",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Facultades_FacultadId",
                table: "Cursos",
                column: "FacultadId",
                principalTable: "Facultades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carreras_Facultades_FacultadId",
                table: "Carreras");

            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Carreras_CarreraId",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Facultades_FacultadId",
                table: "Cursos");

            migrationBuilder.DropTable(
                name: "UsuarioCurso");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_FacultadId",
                table: "Cursos");

            migrationBuilder.DropIndex(
                name: "IX_Carreras_FacultadId",
                table: "Carreras");

            migrationBuilder.DropColumn(
                name: "FacultadId",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "FacultadId",
                table: "Carreras");

            migrationBuilder.AlterColumn<int>(
                name: "CarreraId",
                table: "Cursos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "IdFacultad",
                table: "Carreras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Carreras_CarreraId",
                table: "Cursos",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
