using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;
using SoundSesh.Common;
using SoundSesh.Common.Constants;
using SoundSesh.Common.Extensions;
using SoundSesh.Common.Interfaces;
using SoundSesh.Common.Logging;
using SoundSesh.General.API.Extensions;
using System;
using System.IO;

namespace SoundSesh.General.API
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        private AppSettings Settings => Configuration.Get<AppSettings>();
        public IHostingEnvironment HostingEnvironment { get; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddEnvironmentVariables()
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            File.WriteAllText($"appsettings.{env.EnvironmentName}", "Sanity check for environment. No use.");
            Configuration = builder.Build();
            HostingEnvironment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCrossOriginPolicy(CrossOrigins.Policies.Loose, Settings);
            services.AddHttpContext();
            services.AddAppSettings(Configuration);
            services.AddLogging(Configuration);
            services.AddMvc();
            services.AddAutoMapper();
            services.AddElasticSearch();
            services.AddImageSharp();
            services.AddImaging();
            services.AddRefitClient<IGeoDbAPI>().ConfigureHttpClient(c => c.BaseAddress = new Uri(Settings.ConnectionStrings.GeoDb));
            services.AddBusinessLogic();
            services.AddSwagger(Configuration.Get<AppSettings>());
            services.AddIdentityServerToWebApi(Configuration.Get<AppSettings>());
            services.AddMemoryCaching();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(CrossOrigins.Policies.Loose);
            app.UseVirtualDirectory($"{Settings.StaticFileDrive}/{Settings.ImageFolderName}", Settings.ImageFolderName);
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Settings.Suite}.{Settings.Name}.API - {Settings.Environment} {Settings.Version}");
                c.RoutePrefix = string.Empty;
            });
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
