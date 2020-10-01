using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class unamigracion2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carreras_Facultades_FacultadId",
                table: "Carreras");

            migrationBuilder.DropIndex(
                name: "IX_Carreras_FacultadId",
                table: "Carreras");

            migrationBuilder.DropColumn(
                name: "FacultadId",
                table: "Carreras");

            migrationBuilder.AddColumn<int>(
                name: "IdFacultad",
                table: "Carreras",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdFacultad",
                table: "Carreras");

            migrationBuilder.AddColumn<int>(
                name: "FacultadId",
                table: "Carreras",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_FacultadId",
                table: "Carreras",
                column: "FacultadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carreras_Facultades_FacultadId",
                table: "Carreras",
                column: "FacultadId",
                principalTable: "Facultades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
