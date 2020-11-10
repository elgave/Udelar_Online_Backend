﻿// <auto-generated />
using System;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20201110233044_segunda")]
    partial class segunda
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccessLayer.Archivo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ComponenteId")
                        .HasColumnType("int");

                    b.Property<int?>("EntregaTareaId")
                        .HasColumnType("int");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ubicacion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ComponenteId")
                        .IsUnique()
                        .HasFilter("[ComponenteId] IS NOT NULL");

                    b.HasIndex("EntregaTareaId")
                        .IsUnique()
                        .HasFilter("[EntregaTareaId] IS NOT NULL");

                    b.ToTable("Archivos");
                });

            modelBuilder.Entity("DataAccessLayer.Componente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Indice")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SeccionCursoId")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SeccionCursoId");

                    b.ToTable("Componentes");
                });

            modelBuilder.Entity("DataAccessLayer.Comunicado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ComponenteId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ComponenteId")
                        .IsUnique();

                    b.ToTable("Comunicados");
                });

            modelBuilder.Entity("DataAccessLayer.ContenedorTarea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ComponenteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCierre")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ComponenteId")
                        .IsUnique();

                    b.ToTable("ContenedoresTareas");
                });

            modelBuilder.Entity("DataAccessLayer.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CantCreditos")
                        .HasColumnType("int");

                    b.Property<bool>("ConfirmaBedelia")
                        .HasColumnType("bit");

                    b.Property<int>("FacultadId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FacultadId");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("DataAccessLayer.CursoDocente", b =>
                {
                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("FacultadId")
                        .HasColumnType("int");

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId", "FacultadId", "CursoId");

                    b.HasIndex("CursoId");

                    b.ToTable("CursoDocente");
                });

            modelBuilder.Entity("DataAccessLayer.Encuesta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Fecha")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Encuestas");
                });

            modelBuilder.Entity("DataAccessLayer.EncuestaCurso", b =>
                {
                    b.Property<int>("IdCurso")
                        .HasColumnType("int");

                    b.Property<int>("IdEncuesta")
                        .HasColumnType("int");

                    b.Property<int>("ComponenteId")
                        .HasColumnType("int");

                    b.Property<string>("Fecha")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCurso", "IdEncuesta");

                    b.HasIndex("ComponenteId")
                        .IsUnique();

                    b.HasIndex("IdEncuesta");

                    b.ToTable("EncuestaCursos");
                });

            modelBuilder.Entity("DataAccessLayer.EncuestaFacultad", b =>
                {
                    b.Property<int>("IdFacultad")
                        .HasColumnType("int");

                    b.Property<int>("IdEncuesta")
                        .HasColumnType("int");

                    b.Property<string>("Fecha")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdFacultad", "IdEncuesta");

                    b.HasIndex("IdEncuesta");

                    b.ToTable("encuestaFacultad");
                });

            modelBuilder.Entity("DataAccessLayer.EncuestaUsuario", b =>
                {
                    b.Property<int>("IdEncuesta")
                        .HasColumnType("int");

                    b.Property<string>("Cedula")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("FacultadId")
                        .HasColumnType("int");

                    b.Property<string>("Fecha")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdEncuesta", "Cedula");

                    b.HasIndex("Cedula", "FacultadId");

                    b.ToTable("EncuestaUsuarios");
                });

            modelBuilder.Entity("DataAccessLayer.EntregaTarea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Calificacion")
                        .HasColumnType("int");

                    b.Property<int>("ContenedorTareaId")
                        .HasColumnType("int");

                    b.Property<int>("FacultadId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ContenedorTareaId");

                    b.HasIndex("UsuarioId", "FacultadId");

                    b.ToTable("EntregasTarea");
                });

            modelBuilder.Entity("DataAccessLayer.Facultad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Facultades");
                });

            modelBuilder.Entity("DataAccessLayer.Pregunta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EncuestaId")
                        .HasColumnType("int");

                    b.Property<string>("Texto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EncuestaId");

                    b.ToTable("Preguntas");
                });

            modelBuilder.Entity("DataAccessLayer.Respuesta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PreguntaId")
                        .HasColumnType("int");

                    b.Property<string>("Texto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PreguntaId");

                    b.ToTable("Respuestas");
                });

            modelBuilder.Entity("DataAccessLayer.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("DataAccessLayer.SeccionCurso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<int>("Indice")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.ToTable("SeccionesCursos");
                });

            modelBuilder.Entity("DataAccessLayer.Usuario", b =>
                {
                    b.Property<string>("Cedula")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("FacultadId")
                        .HasColumnType("int");

                    b.Property<string>("Apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contrasena")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Cedula", "FacultadId");

                    b.HasIndex("FacultadId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("DataAccessLayer.UsuarioCurso", b =>
                {
                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("FacultadId")
                        .HasColumnType("int");

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId", "FacultadId", "CursoId");

                    b.HasIndex("CursoId");

                    b.ToTable("UsuarioCurso");
                });

            modelBuilder.Entity("DataAccessLayer.UsuarioRol", b =>
                {
                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("FacultadId")
                        .HasColumnType("int");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId", "FacultadId", "RolId");

                    b.HasIndex("RolId");

                    b.ToTable("UsuarioRol");
                });

            modelBuilder.Entity("DataAccessLayer.Archivo", b =>
                {
                    b.HasOne("DataAccessLayer.Componente", "Componente")
                        .WithOne("Archivo")
                        .HasForeignKey("DataAccessLayer.Archivo", "ComponenteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DataAccessLayer.EntregaTarea", "EntregaTarea")
                        .WithOne("ArchivoEntrega")
                        .HasForeignKey("DataAccessLayer.Archivo", "EntregaTareaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DataAccessLayer.Componente", b =>
                {
                    b.HasOne("DataAccessLayer.SeccionCurso", "SeccionCurso")
                        .WithMany("Componentes")
                        .HasForeignKey("SeccionCursoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.Comunicado", b =>
                {
                    b.HasOne("DataAccessLayer.Componente", "Componente")
                        .WithOne("Comunicado")
                        .HasForeignKey("DataAccessLayer.Comunicado", "ComponenteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.ContenedorTarea", b =>
                {
                    b.HasOne("DataAccessLayer.Componente", "Componente")
                        .WithOne("ContenedorTarea")
                        .HasForeignKey("DataAccessLayer.ContenedorTarea", "ComponenteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.Curso", b =>
                {
                    b.HasOne("DataAccessLayer.Facultad", "Facultad")
                        .WithMany("Cursos")
                        .HasForeignKey("FacultadId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.CursoDocente", b =>
                {
                    b.HasOne("DataAccessLayer.Curso", "Curso")
                        .WithMany("CursosDocentes")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Usuario", "Usuario")
                        .WithMany("CursosDocentes")
                        .HasForeignKey("UsuarioId", "FacultadId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.EncuestaCurso", b =>
                {
                    b.HasOne("DataAccessLayer.Componente", "Componente")
                        .WithOne("Encuesta")
                        .HasForeignKey("DataAccessLayer.EncuestaCurso", "ComponenteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("IdCurso")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Encuesta", "Encuesta")
                        .WithMany()
                        .HasForeignKey("IdEncuesta")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.EncuestaFacultad", b =>
                {
                    b.HasOne("DataAccessLayer.Encuesta", "Encuesta")
                        .WithMany()
                        .HasForeignKey("IdEncuesta")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Facultad", "Facultad")
                        .WithMany()
                        .HasForeignKey("IdFacultad")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.EncuestaUsuario", b =>
                {
                    b.HasOne("DataAccessLayer.Encuesta", "Encuesta")
                        .WithMany()
                        .HasForeignKey("IdEncuesta")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("Cedula", "FacultadId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.EntregaTarea", b =>
                {
                    b.HasOne("DataAccessLayer.ContenedorTarea", "ContenedorTarea")
                        .WithMany("TareasEntregadas")
                        .HasForeignKey("ContenedorTareaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId", "FacultadId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DataAccessLayer.Pregunta", b =>
                {
                    b.HasOne("DataAccessLayer.Encuesta", "Encuesta")
                        .WithMany("Preguntas")
                        .HasForeignKey("EncuestaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.Respuesta", b =>
                {
                    b.HasOne("DataAccessLayer.Pregunta", "Pregunta")
                        .WithMany("Respuestas")
                        .HasForeignKey("PreguntaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.SeccionCurso", b =>
                {
                    b.HasOne("DataAccessLayer.Curso", "Curso")
                        .WithMany("SeccionesCurso")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.Usuario", b =>
                {
                    b.HasOne("DataAccessLayer.Facultad", "Facultad")
                        .WithMany("Usuarios")
                        .HasForeignKey("FacultadId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.UsuarioCurso", b =>
                {
                    b.HasOne("DataAccessLayer.Curso", "Curso")
                        .WithMany("UsuariosCursos")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Usuario", "Usuario")
                        .WithMany("UsuariosCursos")
                        .HasForeignKey("UsuarioId", "FacultadId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.UsuarioRol", b =>
                {
                    b.HasOne("DataAccessLayer.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Usuario", "Usuario")
                        .WithMany("UsuariosRoles")
                        .HasForeignKey("UsuarioId", "FacultadId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
