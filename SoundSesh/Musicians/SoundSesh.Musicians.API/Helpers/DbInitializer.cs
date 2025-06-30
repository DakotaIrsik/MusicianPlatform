using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SoundSesh.Common;
using SoundSesh.Common.Services;
using SoundSesh.Musicians.Entities.ElasticSearch;
using SoundSesh.Musicians.Entities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoundSesh.Musicians.API.Helpers
{
    public static class DbInitializer
    {
        public static async Task Seed(IWebHost host, bool shouldSeed)
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var iOptions = scope.ServiceProvider.GetRequiredService<IOptions<AppSettings>>();

                    if (shouldSeed)
                    {
                        using (var elasticSearchService = scope.ServiceProvider.GetRequiredService<IElasticSearchService>())
                        {
                            Console.WriteLine($"Deleting musician ElasticSearch Index");
                            elasticSearchService.DeleteIndex("musician");

                            Console.WriteLine($"Creating musician ElasticSearch Index");
                            elasticSearchService.CreateIndex(ElasticMusician.IndexDescriptor);
                        }

                        using (var context = scope.ServiceProvider.GetRequiredService<MusicianContext>())
                        {
                            Console.WriteLine("Deleting MSSQL Database");
                            await context.Database.EnsureDeletedAsync();

                            Console.WriteLine("Creating MSSQL Database");
                            await context.Database.EnsureCreatedAsync();
                        }

                        Console.WriteLine("Data successfully seeded.");
                    }
                }
            }
        }

        private static async Task SeedUsersAsync(MusicianContext context)
        {
            List<User> users = new List<User>();
            for (int i = 0; i < 500; i++)
            {
                users.Add(new User()
                {
                    IdentityUserId = Guid.NewGuid().ToString(),
                });
            }

            await context.User.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }

        private static bool DeleteStaticFiles(AppSettings settings, bool recursive)
        {
            
            var imageVirtualDirectory = $"{settings.StaticFileDrive}/{settings.Suite}/{settings.Name}/{settings.ImageFolderName}";
            if (Directory.Exists(imageVirtualDirectory))
            {
                Directory.Delete(imageVirtualDirectory, recursive);
            }
            return Directory.Exists(imageVirtualDirectory);
        }
    }
}
