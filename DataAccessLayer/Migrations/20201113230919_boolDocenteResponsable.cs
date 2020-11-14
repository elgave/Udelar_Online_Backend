using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class boolDocenteResponsable : Migration
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

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuarioId_FacultadId",
                table: "UsuarioRol");

            migrationBuilder.AddColumn<string>(
                name: "CreadaPor",
                table: "Encuestas",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EsResponable",
                table: "CursoDocente",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuarioId_FacultadId",
                table: "UsuarioRol",
                columns: new[] { "UsuarioId", "FacultadId" },
                principalTable: "Usuarios",
                principalColumns: new[] { "Cedula", "FacultadId" },
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

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuarioId_FacultadId",
                table: "UsuarioRol");

            migrationBuilder.DropColumn(
                name: "CreadaPor",
                table: "Encuestas");

            migrationBuilder.DropColumn(
                name: "EsResponable",
                table: "CursoDocente");

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

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRol_Usuarios_UsuarioId_FacultadId",
                table: "UsuarioRol",
                columns: new[] { "UsuarioId", "FacultadId" },
                principalTable: "Usuarios",
                principalColumns: new[] { "Cedula", "FacultadId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
