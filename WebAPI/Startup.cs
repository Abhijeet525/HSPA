using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using WebAPI.Data;
using WebAPI.Data.Repo;
using WebAPI.Data.Repo.Interface;
using WebAPI.Extensions;
using WebAPI.Helpers;
using WebAPI.Middlewares;

namespace WebAPI
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
            services.AddDbContext<DataContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddControllers().AddNewtonsoftJson();

            //Add CORS to the service to remove the CORS Plicy Error
            services.AddCors(option => {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Added Custom Exception Handling Extension and Middleware
            app.ConfigureExceptionHandler(env);
            
            app.UseRouting();

            //After Adding CORS to the service use it after UseRouting and before UseEndpoints emthods to remove the CORS Plicy Error
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
