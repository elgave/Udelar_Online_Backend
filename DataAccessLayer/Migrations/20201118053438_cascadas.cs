using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class cascadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Facultades_FacultadId",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_SeccionesCursos_Cursos_CursoId",
                table: "SeccionesCursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Facultades_FacultadId",
                table: "Usuarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Facultades_FacultadId",
                table: "Cursos",
                column: "FacultadId",
                principalTable: "Facultades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeccionesCursos_Cursos_CursoId",
                table: "SeccionesCursos",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Facultades_FacultadId",
                table: "Usuarios",
                column: "FacultadId",
                principalTable: "Facultades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Facultades_FacultadId",
                table: "Cursos");

            migrationBuilder.DropForeignKey(
                name: "FK_SeccionesCursos_Cursos_CursoId",
                table: "SeccionesCursos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Facultades_FacultadId",
                table: "Usuarios");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Facultades_FacultadId",
                table: "Cursos",
                column: "FacultadId",
                principalTable: "Facultades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SeccionesCursos_Cursos_CursoId",
                table: "SeccionesCursos",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Facultades_FacultadId",
                table: "Usuarios",
                column: "FacultadId",
                principalTable: "Facultades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
