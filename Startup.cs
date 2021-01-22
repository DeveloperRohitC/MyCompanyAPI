using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyCompanyAPI.Context;
using Microsoft.EntityFrameworkCore;
using MyCompanyAPI.Repositories.IRepository;
using MyCompanyAPI.Repositories.Repository;
using AutoMapper;
using System;

namespace MyCompanyAPI
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

            services.AddDbContext<CompanyDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CompanyDBConStr")));
            services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();

            #region Cors

            services.AddCors(options =>
                options.AddDefaultPolicy(
                    builder => builder.WithOrigins("http://localhost:4200")
                                .AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader())
                );
            //services.AddCors();

            #endregion

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyCompanyAPI", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyCompanyAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            #region CORS

            //app.UseCors(
            //    cors => cors.WithOrigins("").AllowAnyMethod()
            //    );

            app.UseCors();

            //app.UseCors(
            //    cors => cors.AllowAnyOrigin()
            //                .AllowAnyMethod()
            //                .AllowAnyHeader()
            //    );

            #endregion

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
