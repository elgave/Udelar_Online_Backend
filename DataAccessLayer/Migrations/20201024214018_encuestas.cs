using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class encuestas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Encuestas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoId = table.Column<int>(nullable: false),
                    Pregunta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encuestas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encuestas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Respuestas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    EncuestaId = table.Column<int>(nullable: false),
                    Texto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuestas", x => new { x.Id, x.EncuestaId });
                    table.ForeignKey(
                        name: "FK_Respuestas_Encuestas_EncuestaId",
                        column: x => x.EncuestaId,
                        principalTable: "Encuestas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Encuestas_CursoId",
                table: "Encuestas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_EncuestaId",
                table: "Respuestas",
                column: "EncuestaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Respuestas");

            migrationBuilder.DropTable(
                name: "Encuestas");
        }
    }
}
