using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace SoundSesh.Angular
{
    public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseIIS()
            .UseSerilog((ctx, config) => { config.ReadFrom.Configuration(ctx.Configuration); })
            .UseStartup<Startup>();
  }
}
