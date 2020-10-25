﻿// <auto-generated />
using System;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccessLayer.Carrera", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FacultadId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FacultadId");

                    b.ToTable("Carreras");
                });

            modelBuilder.Entity("DataAccessLayer.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CantCreditos")
                        .HasColumnType("int");

                    b.Property<int>("CarreraId")
                        .HasColumnType("int");

                    b.Property<int>("FacultadId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarreraId");

                    b.HasIndex("FacultadId");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("DataAccessLayer.Encuesta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<string>("Pregunta")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.ToTable("Encuestas");
                });

            modelBuilder.Entity("DataAccessLayer.Facultad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Facultades");
                });

            modelBuilder.Entity("DataAccessLayer.Respuesta", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("EncuestaId")
                        .HasColumnType("int");

                    b.Property<string>("Texto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id", "EncuestaId");

                    b.HasIndex("EncuestaId");

                    b.ToTable("Respuestas");
                });

            modelBuilder.Entity("DataAccessLayer.Roles", b =>
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
                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<string>("CursoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("CursoId1")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioCedula")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UsuarioFacultadId")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId", "CursoId");

                    b.HasIndex("CursoId1");

                    b.HasIndex("UsuarioCedula", "UsuarioFacultadId");

                    b.ToTable("UsuarioCurso");
                });

            modelBuilder.Entity("DataAccessLayer.UsuarioRol", b =>
                {
                    b.Property<string>("IdUsuario")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IdFacultad")
                        .HasColumnType("int");

                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.HasKey("IdUsuario", "IdFacultad", "IdRol");

                    b.HasIndex("IdRol");

                    b.ToTable("UsuarioRol");
                });

            modelBuilder.Entity("DataAccessLayer.Carrera", b =>
                {
                    b.HasOne("DataAccessLayer.Facultad", "Facultad")
                        .WithMany()
                        .HasForeignKey("FacultadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.Curso", b =>
                {
                    b.HasOne("DataAccessLayer.Carrera", "Carrera")
                        .WithMany("Cursos")
                        .HasForeignKey("CarreraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Facultad", "Facultad")
                        .WithMany("Cursos")
                        .HasForeignKey("FacultadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.Encuesta", b =>
                {
                    b.HasOne("DataAccessLayer.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.Respuesta", b =>
                {
                    b.HasOne("DataAccessLayer.Encuesta", "Encuesta")
                        .WithMany("Respuestas")
                        .HasForeignKey("EncuestaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.Usuario", b =>
                {
                    b.HasOne("DataAccessLayer.Facultad", "Facultad")
                        .WithMany("Usuarios")
                        .HasForeignKey("FacultadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.UsuarioCurso", b =>
                {
                    b.HasOne("DataAccessLayer.Curso", "Curso")
                        .WithMany("UsuariosCursos")
                        .HasForeignKey("CursoId1");

                    b.HasOne("DataAccessLayer.Usuario", "Usuario")
                        .WithMany("UsuariosCursos")
                        .HasForeignKey("UsuarioCedula", "UsuarioFacultadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccessLayer.UsuarioRol", b =>
                {
                    b.HasOne("DataAccessLayer.Roles", "Rol")
                        .WithMany()
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccessLayer.Usuario", null)
                        .WithMany("Roles")
                        .HasForeignKey("IdUsuario", "IdFacultad")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
