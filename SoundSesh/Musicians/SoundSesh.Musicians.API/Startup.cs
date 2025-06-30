using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SoundSesh.Common;
using SoundSesh.Common.Constants;
using SoundSesh.Common.Extensions;
using SoundSesh.Common.Hubs;
using SoundSesh.Common.Logging;
using SoundSesh.Musicians.API.Extensions;
using SoundSesh.Musicians.API.Interfaces;
using SoundSesh.Musicians.Entities.Models;
using System.IO;

namespace SoundSesh.Musicians.API
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
            services.AddSignalR();
            services.AddHttpContext();
            services.AddAppSettings(Configuration);
            services.AddCrossOriginPolicy(CrossOrigins.Policies.Loose, Settings);
            services.AddLogging(Configuration);
            services.AddMvc();
            services.AddAutoMapper();
            services.AddElasticSearch();
            services.AddRefit<ISoundSeshGeneralAPI>(Settings.ConnectionStrings.GeneralApi, Settings.Timers.Apis.General);
            services.AddRefit<ISeedSoundSeshGeneralAPI>(Settings.ConnectionStrings.GeneralApi);
            services.AddDbContext<MusicianContext>(options => options.UseSqlServer(Settings.ConnectionStrings.MSSQL));
            services.AddBusinessLogic();
            services.AddSwagger(Configuration.Get<AppSettings>());
            services.AddIdentityServerToWebApi(Configuration.Get<AppSettings>());
            services.AddMemoryCaching();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(CrossOrigins.Policies.Loose);
            var fullImagepath = $"{Settings.StaticFileDrive}/{Settings.Suite}/{Settings.Name}/{Settings.ImageFolderName}";
            app.UseVirtualDirectory($"{Settings.StaticFileDrive}/{Settings.Suite}/{Settings.Name}/{Settings.ImageFolderName}", Settings.ImageFolderName);
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Settings.Suite}.{Settings.Name}.API - {Settings.Environment} {Settings.Version}");
                c.RoutePrefix = string.Empty;
            });
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseAuthentication();
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chat");
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
