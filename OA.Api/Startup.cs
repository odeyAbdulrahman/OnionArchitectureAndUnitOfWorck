using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OA.api.ConfigureServices;
using OA.Api.ConfigureServices;
using OA.Data;
using System;
namespace HomeCareService.Api
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
            services.InstallServiceInAssembly(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, AppDbContext db, ILoggerFactory loggerFactory)
        {
            loggerFactory.CreateLogger("Logs/OnionArchitecture-{Date}.txt");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Home.Care.Services v1"));
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowOrigin");
            app.UseRouting().UseCors(options => options.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed((Host) => true).AllowCredentials());
            app.UseAuthentication();
            app.UseAuthorization();
            Endpoints.UseEndPoints(app);
        }
    }
}
