using LiteDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;


namespace DataAccessLayer
{
    public class MyContext : DbContext
    {
        private IConfiguration _configuration;
        private string S3Access { get; set; }
        private string S3Secret { get; set; }
        private string S3Bucket { get; set; }
        private static readonly RegionEndpoint region = RegionEndpoint.USEast1;
        private static IAmazonS3 s3Client;
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
        public DbSet<Archivo> Archivos { get; set; }

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
        public MyContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
            NoSql = new LiteDatabase("Filename=./nosql.db;Connection=shared");
            _configuration = configuration;
            S3Access = _configuration["S3Keys:S3Access"];
            S3Secret = _configuration["S3Keys:S3Secret"];
            S3Bucket = _configuration["S3Keys:S3Bucket"];
            Console.WriteLine(S3Access, S3Bucket, S3Secret);
        }
 
        public void UploadS3(IFormFile file, string folder, string name)
        {
            s3Client = new AmazonS3Client(S3Access, S3Secret, region);
            var newMemoryStream = new MemoryStream();
            file.CopyTo(newMemoryStream);
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = newMemoryStream,
                Key = folder+"/"+name,
                BucketName = S3Bucket,
                CannedACL = S3CannedACL.PublicRead
            };

            var fileTransferUtility = new TransferUtility(s3Client);
            fileTransferUtility.UploadAsync(uploadRequest);
        }
    }
}
