using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class segunda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivos_EntregaTarea_EntregaTareaId",
                table: "Archivos");

            migrationBuilder.DropForeignKey(
                name: "FK_EntregaTarea_ContenedoresTareas_ContenedorTardeaId",
                table: "EntregaTarea");

            migrationBuilder.DropForeignKey(
                name: "FK_EntregaTarea_Usuarios_UsuarioId_FacultadId",
                table: "EntregaTarea");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntregaTarea",
                table: "EntregaTarea");

            migrationBuilder.DropIndex(
                name: "IX_EntregaTarea_ContenedorTardeaId",
                table: "EntregaTarea");

            migrationBuilder.DropColumn(
                name: "ContenedorTardeaId",
                table: "EntregaTarea");

            migrationBuilder.RenameTable(
                name: "EntregaTarea",
                newName: "EntregasTarea");

            migrationBuilder.RenameIndex(
                name: "IX_EntregaTarea_UsuarioId_FacultadId",
                table: "EntregasTarea",
                newName: "IX_EntregasTarea_UsuarioId_FacultadId");

            migrationBuilder.AddColumn<int>(
                name: "ComponenteId",
                table: "EncuestaCursos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmaBedelia",
                table: "Cursos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Calificacion",
                table: "EntregasTarea",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntregasTarea",
                table: "EntregasTarea",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "encuestaFacultad",
                columns: table => new
                {
                    IdEncuesta = table.Column<int>(nullable: false),
                    IdFacultad = table.Column<int>(nullable: false),
                    Fecha = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_encuestaFacultad", x => new { x.IdFacultad, x.IdEncuesta });
                    table.ForeignKey(
                        name: "FK_encuestaFacultad_Encuestas_IdEncuesta",
                        column: x => x.IdEncuesta,
                        principalTable: "Encuestas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_encuestaFacultad_Facultades_IdFacultad",
                        column: x => x.IdFacultad,
                        principalTable: "Facultades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EncuestaCursos_ComponenteId",
                table: "EncuestaCursos",
                column: "ComponenteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntregasTarea_ContenedorTareaId",
                table: "EntregasTarea",
                column: "ContenedorTareaId");

            migrationBuilder.CreateIndex(
                name: "IX_encuestaFacultad_IdEncuesta",
                table: "encuestaFacultad",
                column: "IdEncuesta");

            migrationBuilder.AddForeignKey(
                name: "FK_Archivos_EntregasTarea_EntregaTareaId",
                table: "Archivos",
                column: "EntregaTareaId",
                principalTable: "EntregasTarea",
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
                name: "FK_EntregasTarea_ContenedoresTareas_ContenedorTareaId",
                table: "EntregasTarea",
                column: "ContenedorTareaId",
                principalTable: "ContenedoresTareas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntregasTarea_Usuarios_UsuarioId_FacultadId",
                table: "EntregasTarea",
                columns: new[] { "UsuarioId", "FacultadId" },
                principalTable: "Usuarios",
                principalColumns: new[] { "Cedula", "FacultadId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Archivos_EntregasTarea_EntregaTareaId",
                table: "Archivos");

            migrationBuilder.DropForeignKey(
                name: "FK_EncuestaCursos_Componentes_ComponenteId",
                table: "EncuestaCursos");

            migrationBuilder.DropForeignKey(
                name: "FK_EntregasTarea_ContenedoresTareas_ContenedorTareaId",
                table: "EntregasTarea");

            migrationBuilder.DropForeignKey(
                name: "FK_EntregasTarea_Usuarios_UsuarioId_FacultadId",
                table: "EntregasTarea");

            migrationBuilder.DropTable(
                name: "encuestaFacultad");

            migrationBuilder.DropIndex(
                name: "IX_EncuestaCursos_ComponenteId",
                table: "EncuestaCursos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntregasTarea",
                table: "EntregasTarea");

            migrationBuilder.DropIndex(
                name: "IX_EntregasTarea_ContenedorTareaId",
                table: "EntregasTarea");

            migrationBuilder.DropColumn(
                name: "ComponenteId",
                table: "EncuestaCursos");

            migrationBuilder.DropColumn(
                name: "ConfirmaBedelia",
                table: "Cursos");

            migrationBuilder.RenameTable(
                name: "EntregasTarea",
                newName: "EntregaTarea");

            migrationBuilder.RenameIndex(
                name: "IX_EntregasTarea_UsuarioId_FacultadId",
                table: "EntregaTarea",
                newName: "IX_EntregaTarea_UsuarioId_FacultadId");

            migrationBuilder.AlterColumn<string>(
                name: "Calificacion",
                table: "EntregaTarea",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ContenedorTardeaId",
                table: "EntregaTarea",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntregaTarea",
                table: "EntregaTarea",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_EntregaTarea_ContenedorTardeaId",
                table: "EntregaTarea",
                column: "ContenedorTardeaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Archivos_EntregaTarea_EntregaTareaId",
                table: "Archivos",
                column: "EntregaTareaId",
                principalTable: "EntregaTarea",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntregaTarea_ContenedoresTareas_ContenedorTardeaId",
                table: "EntregaTarea",
                column: "ContenedorTardeaId",
                principalTable: "ContenedoresTareas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EntregaTarea_Usuarios_UsuarioId_FacultadId",
                table: "EntregaTarea",
                columns: new[] { "UsuarioId", "FacultadId" },
                principalTable: "Usuarios",
                principalColumns: new[] { "Cedula", "FacultadId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
