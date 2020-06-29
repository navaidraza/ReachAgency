using ACME.DAL;
using ACME.DAL.Interfaces;
using ACME.DAL.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;

namespace ACME.API
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
            services.AddDbContext<DatabaseContext>(x =>
             x.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));
            services.AddCors();
            services.AddScoped<IRepository, Repository>();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder => {

                builder.Run(async context => {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var error = context.Features.Get<IExceptionHandlerFeature>();
               
                if (error != null)
                {
                   //can add centralized logging here
                    await context.Response.WriteAsync(Appconstants.ErrorText + error.Error.Message);
                }
                    });
                });
            }

            app.UseRouting();

            Action<CorsPolicyBuilder> configurePolicy =
            y => y.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

            app.UseCors(configurePolicy);
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
