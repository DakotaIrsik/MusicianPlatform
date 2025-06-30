using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using SoundSesh.Studios.API.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace SoundSesh.Studios.API
{
    public class Program
    {
        private const string SeedArgs = "/seed";

        private const string DeleteStaticFileArgs = "/deletestaticfiles";

        public static async Task Main(string[] args)
        {
            var shouldSeed = args.Any(x => x == SeedArgs);
            var shouldDeleteStaticFiles = args.Any(x => x == DeleteStaticFileArgs);
            if (shouldSeed)
            {
                args = args.Except(new[] { SeedArgs }).ToArray();
            }

            if (shouldDeleteStaticFiles)
            {
                args = args.Except(new[] { DeleteStaticFileArgs }).ToArray();
            }

            var host = BuildWebHost(args);
            //shouldSeed = true;
            //shouldDeleteStaticFiles = true;
            await DbInitializer.Seed(host, shouldSeed, shouldDeleteStaticFiles);
            
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseIIS()
                .UseSerilog((ctx, config) => { config.ReadFrom.Configuration(ctx.Configuration); })
                .UseStartup<Startup>()
                .Build();
    }
}
