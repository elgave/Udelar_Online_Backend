using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class addCarreras2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carrera_Facultades_FacultadId",
                table: "Carrera");

            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Carrera_CarreraId",
                table: "Cursos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carrera",
                table: "Carrera");

            migrationBuilder.RenameTable(
                name: "Carrera",
                newName: "Carreras");

            migrationBuilder.RenameIndex(
                name: "IX_Carrera_FacultadId",
                table: "Carreras",
                newName: "IX_Carreras_FacultadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carreras",
                table: "Carreras",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carreras_Facultades_FacultadId",
                table: "Carreras",
                column: "FacultadId",
                principalTable: "Facultades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Carreras_CarreraId",
                table: "Cursos",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carreras_Facultades_FacultadId",
                table: "Carreras");

            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Carreras_CarreraId",
                table: "Cursos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carreras",
                table: "Carreras");

            migrationBuilder.RenameTable(
                name: "Carreras",
                newName: "Carrera");

            migrationBuilder.RenameIndex(
                name: "IX_Carreras_FacultadId",
                table: "Carrera",
                newName: "IX_Carrera_FacultadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carrera",
                table: "Carrera",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carrera_Facultades_FacultadId",
                table: "Carrera",
                column: "FacultadId",
                principalTable: "Facultades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Carrera_CarreraId",
                table: "Cursos",
                column: "CarreraId",
                principalTable: "Carrera",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
