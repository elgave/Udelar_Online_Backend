using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public class MyContext : DbContext
    {
        public MyContext(): base() { }
        public DbSet<Facultad> Facultades { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Carrera> Carreras { get; set; }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<UsuarioRol> UsuarioRol { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=LAPTOP-GCMMBV6B\SQLEXPRESS;Database=EntregaIndividual;Integrated Security=True;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(e => new { e.Cedula, e.IdFacultad});

            modelBuilder.Entity<UsuarioRol>()
                .HasKey(e => new { e.IdUsuario, e.IdFacultad, e.IdRol});
        }
    }
}