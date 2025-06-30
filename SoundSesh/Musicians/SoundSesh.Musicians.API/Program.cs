using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using SoundSesh.Musicians.API.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SoundSesh.Musicians.API
{
    public class Program
    {
        private const string SeedArgs = "/seed";

        public static async Task Main(string[] args)
        {
            var shouldSeed = args.Any(x => x == SeedArgs);
            if (shouldSeed)
            {
                args = args.Except(new[] { SeedArgs }).ToArray();
            }

            var host = BuildWebHost(args);
            shouldSeed = true;
            await DbInitializer.Seed(host, shouldSeed);
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
