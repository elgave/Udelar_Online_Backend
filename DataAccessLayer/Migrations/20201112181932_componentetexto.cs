using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class componentetexto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivos_Componentes_ComponenteId",
                table: "Archivos");

            migrationBuilder.DropForeignKey(
                name: "FK_Componentes_SeccionesCursos_SeccionCursoId",
                table: "Componentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Comunicados_Componentes_ComponenteId",
                table: "Comunicados");

            migrationBuilder.DropForeignKey(
                name: "FK_ContenedoresTareas_Componentes_ComponenteId",
                table: "ContenedoresTareas");

            migrationBuilder.DropForeignKey(
                name: "FK_EncuestaCursos_Componentes_ComponenteId",
                table: "EncuestaCursos");

            migrationBuilder.AddColumn<string>(
                name: "Texto",
                table: "Componentes",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Archivos_Componentes_ComponenteId",
                table: "Archivos",
                column: "ComponenteId",
                principalTable: "Componentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Componentes_SeccionesCursos_SeccionCursoId",
                table: "Componentes",
                column: "SeccionCursoId",
                principalTable: "SeccionesCursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comunicados_Componentes_ComponenteId",
                table: "Comunicados",
                column: "ComponenteId",
                principalTable: "Componentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContenedoresTareas_Componentes_ComponenteId",
                table: "ContenedoresTareas",
                column: "ComponenteId",
                principalTable: "Componentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EncuestaCursos_Componentes_ComponenteId",
                table: "EncuestaCursos",
                column: "ComponenteId",
                principalTable: "Componentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivos_Componentes_ComponenteId",
                table: "Archivos");

            migrationBuilder.DropForeignKey(
                name: "FK_Componentes_SeccionesCursos_SeccionCursoId",
                table: "Componentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Comunicados_Componentes_ComponenteId",
                table: "Comunicados");

            migrationBuilder.DropForeignKey(
                name: "FK_ContenedoresTareas_Componentes_ComponenteId",
                table: "ContenedoresTareas");

            migrationBuilder.DropForeignKey(
                name: "FK_EncuestaCursos_Componentes_ComponenteId",
                table: "EncuestaCursos");

            migrationBuilder.DropColumn(
                name: "Texto",
                table: "Componentes");

            migrationBuilder.AddForeignKey(
                name: "FK_Archivos_Componentes_ComponenteId",
                table: "Archivos",
                column: "ComponenteId",
                principalTable: "Componentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Componentes_SeccionesCursos_SeccionCursoId",
                table: "Componentes",
                column: "SeccionCursoId",
                principalTable: "SeccionesCursos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comunicados_Componentes_ComponenteId",
                table: "Comunicados",
                column: "ComponenteId",
                principalTable: "Componentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContenedoresTareas_Componentes_ComponenteId",
                table: "ContenedoresTareas",
                column: "ComponenteId",
                principalTable: "Componentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EncuestaCursos_Componentes_ComponenteId",
                table: "EncuestaCursos",
                column: "ComponenteId",
                principalTable: "Componentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
