using AutoMapper;
using GenericBizRunner;
using GenericBizRunner.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoundSesh.Common;
using SoundSesh.Venues.Core.BusinessLogic;
using SoundSesh.Venues.Entities.DTOs.Create;
using SoundSesh.Venues.Entities.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;

namespace SoundSesh
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void CustomSaveChangesExceptionHandler(System.Exception exception, DbContext context, IStatusGeneric statusGeneric)
        {

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<StudioContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            var config = new GenericBizRunnerConfig
            {
                SaveChangesExceptionHandler = EFDbExceptionHandler.SaveChangesError
            };

            services.RegisterBizRunnerWithDtoScans<StudioContext>(config, Assembly.GetAssembly(typeof(StudioDTO)));

            services.AddTransient<ICreateStudio, CreateStudio>();
            services.AddAutoMapper();


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState);

                    var result = new BadRequestObjectResult(problemDetails.Errors);

                    result.ContentTypes.Add("application/json");
                    result.ContentTypes.Add("application/xml");

                    return result;
                };
            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "SoundSesh Venues API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
