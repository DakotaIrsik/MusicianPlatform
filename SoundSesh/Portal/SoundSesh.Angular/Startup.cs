using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.DependencyInjection;

namespace SoundSesh
{
  public class Startup
  {

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // In production, the Angular files will be served from this directory
      services.AddSpaStaticFiles(configuration =>
      {
        configuration.RootPath = "ClientApp/dist";
          //test work item association.
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      app.UseSpaStaticFiles();

      app.UseSpa(spa =>
      {

        spa.Options.SourcePath = "ClientApp";

          if (env.IsDevelopment())
          {
              spa.UseAngularCliServer(npmScript: "start");
          }
      });
    }
  }
}
