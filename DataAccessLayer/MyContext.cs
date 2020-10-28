using LiteDB;
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
        public DbSet<UsuarioCurso> UsuarioCurso { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioRol> UsuarioRol { get; set; }
        public DbSet<Encuesta> Encuestas { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }
        public LiteDatabase NoSql { get; set; }
        public DbSet<Pregunta> Preguntas { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Usuario>()
                .HasKey(e => new { e.Cedula, e.FacultadId});

            modelBuilder.Entity<UsuarioRol>()
                .HasKey(e => new { e.UsuarioId, e.FacultadId, e.RolId });

            modelBuilder.Entity<UsuarioCurso>()
               .HasKey(uc => new { uc.UsuarioId, uc.FacultadId, uc.CursoId });

            modelBuilder.Entity<Respuesta>()
              .HasOne(e => e.Pregunta).WithMany().HasForeignKey(e => e.PreguntaId).OnDelete(DeleteBehavior.Cascade);
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
            NoSql = new LiteDatabase("Filename=./nosql.db;Connection=shared");
        }
    }
}
