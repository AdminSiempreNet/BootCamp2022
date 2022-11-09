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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIENDA.Data.Services;
using TIENDA.Data.SqlServer;

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
            services.AddControllers();

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

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
