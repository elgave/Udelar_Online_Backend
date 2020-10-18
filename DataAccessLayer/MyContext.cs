using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public DbSet<UsuarioCurso> UsuarioCurso { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UsuarioRol> UsuarioRol { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(e => new { e.Cedula, e.FacultadId});

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Roles).WithOne().HasForeignKey(ur => new {ur.IdUsuario, ur.IdFacultad});

            modelBuilder.Entity<UsuarioRol>()
                .HasKey(e => new { e.IdUsuario, e.IdFacultad, e.IdRol });

            modelBuilder.Entity<UsuarioCurso>()
               .HasKey(uc => new { uc.UsuarioId, uc.CursoId });
        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Usuario && (
                        e.State == EntityState.Added));

            foreach (var entityEntry in entries)
            {
                ((Usuario)entityEntry.Entity).FechaCreacion = DateTime.Now;
            }

            return base.SaveChanges();
        }
        public MyContext(DbContextOptions options) : base(options)
        {
        }
    }
}