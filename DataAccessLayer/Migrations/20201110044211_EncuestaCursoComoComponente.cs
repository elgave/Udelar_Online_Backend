using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class EncuestaCursoComoComponente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntregaTarea_ContenedoresTareas_ContenedorTardeaId",
                table: "EntregaTarea");

            migrationBuilder.DropIndex(
                name: "IX_EntregaTarea_ContenedorTardeaId",
                table: "EntregaTarea");

            migrationBuilder.DropColumn(
                name: "ContenedorTardeaId",
                table: "EntregaTarea");

            migrationBuilder.AddColumn<int>(
                name: "ComponenteId",
                table: "EncuestaCursos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EntregaTarea_ContenedorTareaId",
                table: "EntregaTarea",
                column: "ContenedorTareaId");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaCursos_ComponenteId",
                table: "EncuestaCursos",
                column: "ComponenteId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EncuestaCursos_Componentes_ComponenteId",
                table: "EncuestaCursos",
                column: "ComponenteId",
                principalTable: "Componentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntregaTarea_ContenedoresTareas_ContenedorTareaId",
                table: "EntregaTarea",
                column: "ContenedorTareaId",
                principalTable: "ContenedoresTareas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EncuestaCursos_Componentes_ComponenteId",
                table: "EncuestaCursos");

            migrationBuilder.DropForeignKey(
                name: "FK_EntregaTarea_ContenedoresTareas_ContenedorTareaId",
                table: "EntregaTarea");

            migrationBuilder.DropIndex(
                name: "IX_EntregaTarea_ContenedorTareaId",
                table: "EntregaTarea");

            migrationBuilder.DropIndex(
                name: "IX_EncuestaCursos_ComponenteId",
                table: "EncuestaCursos");

            migrationBuilder.DropColumn(
                name: "ComponenteId",
                table: "EncuestaCursos");

            migrationBuilder.AddColumn<int>(
                name: "ContenedorTardeaId",
                table: "EntregaTarea",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntregaTarea_ContenedorTardeaId",
                table: "EntregaTarea",
                column: "ContenedorTardeaId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntregaTarea_ContenedoresTareas_ContenedorTardeaId",
                table: "EntregaTarea",
                column: "ContenedorTardeaId",
                principalTable: "ContenedoresTareas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
