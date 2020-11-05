using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class migraciondepeter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encuestas_Cursos_CursoId",
                table: "Encuestas");

            migrationBuilder.DropForeignKey(
                name: "FK_Preguntas_Encuestas_EncuestaId",
                table: "Preguntas");

            migrationBuilder.DropForeignKey(
                name: "FK_Respuestas_Encuestas_EncuestaId",
                table: "Respuestas");

            migrationBuilder.DropIndex(
                name: "IX_Respuestas_EncuestaId",
                table: "Respuestas");

            migrationBuilder.DropIndex(
                name: "IX_Encuestas_CursoId",
                table: "Encuestas");

            migrationBuilder.DropColumn(
                name: "EncuestaId",
                table: "Respuestas");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "Encuestas");

            migrationBuilder.CreateTable(
                name: "EncuestaCursos",
                columns: table => new
                {
                    IdEncuesta = table.Column<int>(nullable: false),
                    IdCurso = table.Column<int>(nullable: false),
                    Fecha = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncuestaCursos", x => new { x.IdCurso, x.IdEncuesta });
                    table.ForeignKey(
                        name: "FK_EncuestaCursos_Cursos_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EncuestaCursos_Encuestas_IdEncuesta",
                        column: x => x.IdEncuesta,
                        principalTable: "Encuestas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaCursos_IdEncuesta",
                table: "EncuestaCursos",
                column: "IdEncuesta");

            migrationBuilder.AddForeignKey(
                name: "FK_Preguntas_Encuestas_EncuestaId",
                table: "Preguntas",
                column: "EncuestaId",
                principalTable: "Encuestas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Preguntas_Encuestas_EncuestaId",
                table: "Preguntas");

            migrationBuilder.DropTable(
                name: "EncuestaCursos");

            migrationBuilder.AddColumn<int>(
                name: "EncuestaId",
                table: "Respuestas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CursoId",
                table: "Encuestas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_EncuestaId",
                table: "Respuestas",
                column: "EncuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_Encuestas_CursoId",
                table: "Encuestas",
                column: "CursoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Encuestas_Cursos_CursoId",
                table: "Encuestas",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Preguntas_Encuestas_EncuestaId",
                table: "Preguntas",
                column: "EncuestaId",
                principalTable: "Encuestas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Respuestas_Encuestas_EncuestaId",
                table: "Respuestas",
                column: "EncuestaId",
                principalTable: "Encuestas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
