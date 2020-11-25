using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class Calendario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calendarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(nullable: true),
                    IdCurso = table.Column<int>(nullable: false),
                    ComponenteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendarios_Componentes_ComponenteId",
                        column: x => x.ComponenteId,
                        principalTable: "Componentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calendarios_Cursos_IdCurso",
                        column: x => x.IdCurso,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FechaCalendarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<string>(nullable: true),
                    Texto = table.Column<string>(nullable: true),
                    CalendarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FechaCalendarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FechaCalendarios_Calendarios_CalendarioId",
                        column: x => x.CalendarioId,
                        principalTable: "Calendarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calendarios_ComponenteId",
                table: "Calendarios",
                column: "ComponenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Calendarios_IdCurso",
                table: "Calendarios",
                column: "IdCurso");

            migrationBuilder.CreateIndex(
                name: "IX_FechaCalendarios_CalendarioId",
                table: "FechaCalendarios",
                column: "CalendarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FechaCalendarios");

            migrationBuilder.DropTable(
                name: "Calendarios");
        }
    }
}
