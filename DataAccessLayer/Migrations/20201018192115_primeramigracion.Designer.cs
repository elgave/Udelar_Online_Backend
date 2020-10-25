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
    [Migration("20201018192115_primeramigracion")]
    partial class primeramigracion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccessLayer.Carrera", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdFacultad")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

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

                    b.Property<int?>("CarreraId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarreraId");

                    b.ToTable("Cursos");
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

            modelBuilder.Entity("DataAccessLayer.Curso", b =>
                {
                    b.HasOne("DataAccessLayer.Carrera", null)
                        .WithMany("Cursos")
                        .HasForeignKey("CarreraId");
                });

            modelBuilder.Entity("DataAccessLayer.Usuario", b =>
                {
                    b.HasOne("DataAccessLayer.Facultad", "Facultad")
                        .WithMany("Usuarios")
                        .HasForeignKey("FacultadId")
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