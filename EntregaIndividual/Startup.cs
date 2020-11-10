using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using BusinessLayer.Managers;
using DataAccessLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Utilidades;

namespace EntregaIndividual
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddScoped<IUsuarioManager, UsuarioManager>();
            services.AddScoped<IFacultadManager, FacultadManager>();
            services.AddScoped<ICursoManager, CursoManager>();
            services.AddScoped<IUdelarAdminManager, UdelarAdminManager>();
            services.AddScoped<IEncuestaManager, EncuestaManager>();
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            /*
            // Para despliegue en la nube. Ignorar
            string username = Configuration["RDS_USERNAME"];
            string password = Configuration["RDS_PASSWORD"];
            string dbname = Configuration["RDS_DB_NAME"];
            string hostname = Configuration["RDS_HOSTNAME"];
            string port = Configuration["RDS_PORT"];

            string connection = "Data Source=" + hostname + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";

            services.AddDbContext<MyContext>(opt =>
               opt.UseSqlServer(Configuration.GetConnectionString("connection")));
            */

            services.AddDbContext<MyContext>(opt =>
               opt.UseSqlServer(Configuration.GetConnectionString("TSIDB")));
            //Swagger
            AddSwagger(services);

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            services.AddAuthentication(options =>
            {
                //AuthenticationScheme = "Bearer"
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = context =>
                    {
                        List<Claim> cl = new List<Claim>(((ClaimsIdentity)context.Principal.Identity).Claims);
                        // ver que pasa con varios usuarios simultaneos. probablemente se rompa
                        string strUsuario = cl.Where(c => c.Type == Constantes.JWT_CLAIM_USUARIO).First().Value;

                        if (string.IsNullOrWhiteSpace(strUsuario))
                        {
                            context.Fail("Unauthorized");
                        }

                        return Task.CompletedTask;
                    }
                };

                //https://tools.ietf.org/html/rfc7519#page-9
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    SaveSigninToken = true,
                    ValidateActor = true,
                    ValidateIssuer = true, //Issuer: Emisor
                    ValidateAudience = true, //Audience: Son los destinatarios del token
                    ValidateLifetime = true, //Lifetime: Tiempo de vida del token
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["ApiAuth:Issuer"],
                    ValidAudience = Configuration["ApiAuth:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["ApiAuth:SecretKey"]))
                };
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowOrigin");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Foo API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Foo {groupName}",
                    Version = groupName,
                    Description = "Foo API",
                    Contact = new OpenApiContact
                    {
                        Name = "Foo Company",
                        Email = string.Empty,
                        Url = new Uri("https://foo.com/"),
                    }
                });
            });
        }


      
    }
}
