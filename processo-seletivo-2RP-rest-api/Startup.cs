using _2rp_processo.webApi.Contexts;
using _2rp_processo.webApi.Interface;
using _2rp_processo.webApi.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace _2rp_processo.webApi
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

            services.AddControllers();


            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                                builder =>
                                {
                                    builder.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "2RP-processo.webApi", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddDbContext<Processo2rpContext>(options =>
   options.UseSqlServer(Configuration.GetConnectionString("AwsRdsConnetionString"),
   options => options.EnableRetryOnFailure()
   ));


            services.AddTransient<DbContext, Processo2rpContext>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<ITipoUsuarioRepository, TipoUsuarioRepository>();

            services
               .AddAuthentication(option =>
               {
                   option.DefaultAuthenticateScheme = "JwtBearer";
                   option.DefaultChallengeScheme = "JwtBearer";
               }
               )

               .AddJwtBearer("JwtBearer", options =>
               options.TokenValidationParameters = new TokenValidationParameters()
               {

                    // será validado emissor do token
                    ValidateIssuer = true,

                    //será validade endereço do token
                    ValidateAudience = true,

                    //será vailidado tempo do token
                    ValidateLifetime = true,

                    //definição da chave de segurança
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("LKAJSDÇLKFHISFDJA-AHEFHLAEHFL-HFEUH298HG3RQ2")),

                    //define o tempo de expiração
                    ClockSkew = TimeSpan.FromHours(1),

                   ValidIssuer = "2rp-processo.webApi",

                   ValidAudience = "2rp-processo.webApi"
               }
               );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "2RP-processo.webApi");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseStaticFiles();

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
                RequestPath = "/StaticFiles",
                EnableDefaultFiles = true
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
