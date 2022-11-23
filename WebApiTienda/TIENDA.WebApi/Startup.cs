using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TIENDA.Data.Services;
using TIENDA.Data.SqlServer;
using TIENDA.Email;

namespace TIENDA.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();

            //Controladores
            services.AddControllers()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDbContext<DBConnection>(option =>
                        option.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            services.AddScoped<DBConnection>();

            //Autenticación
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("Authentication:SecretKey"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    //Emisor
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],

                    //Audiencia
                    ValidateAudience = true,
                    ValidAudience = Configuration["Authentication:Audience"],

                    NameClaimType = JwtClaimTypes.Subject,
                    ValidateLifetime = true,
                };
            });

            services.AddScoped<ICitiesService, CitiesService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICustomersService, CustomersService>();
            services.AddScoped<IBillingService, BillingService>();
            services.AddScoped<IUserService, UsersService>();
            services.AddScoped<IProductService, ProductService>();


            //Configuración para el correo
            var smtpConfig = new SmtpConfig();
            Configuration.Bind("SmtpConfig", smtpConfig);
            services.AddSingleton(smtpConfig);

            services.AddScoped<EmailService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WEBAPI TIENDA",
                    Version = "v1",
                    Description = "Api para la tienda del Bootcamp 2022",
                    Contact = new OpenApiContact() { Name = "SIEMPRE.NET S.A.S.", Email = "siempre.net@gmail.com" }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Autenticación con JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WEBAPI TIENDA  v1"));

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
