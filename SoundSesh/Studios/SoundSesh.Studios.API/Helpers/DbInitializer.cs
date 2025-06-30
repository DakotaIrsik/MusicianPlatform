using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SoundSesh.Common;
using SoundSesh.Common.Constants;
using SoundSesh.Common.Helpers;
using SoundSesh.Common.LookUps;
using SoundSesh.Common.Services;
using SoundSesh.Studios.API.Interfaces;
using SoundSesh.Studios.Entities.ElasticSearch;
using SoundSesh.Studios.Entities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SoundSesh.Studios.API.Helpers
{
    public static class DbInitializer
    {
        public static async Task Seed(IWebHost host, bool shouldSeed, bool shouldDeleteStaticFiles)
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var iOptions = scope.ServiceProvider.GetRequiredService<IOptions<AppSettings>>();

                    if (shouldDeleteStaticFiles)
                    {
                        Console.WriteLine("Deleting Static files");
                        DeleteStaticFiles(iOptions.Value, true);
                    }

                    if (shouldSeed)
                    {
                        using (var elasticSearchService = scope.ServiceProvider.GetRequiredService<IElasticSearchService>())
                        {
                            Console.WriteLine($"Deleting studio ElasticSearch Index");
                            elasticSearchService.DeleteIndex("studio");
                            Console.WriteLine($"Creating studio ElasticSearch Index");
                            elasticSearchService.CreateIndex(ElasticStudio.IndexDescriptor);

                            Console.WriteLine($"Deleting amenity ElasticSearch Index");
                            elasticSearchService.DeleteIndex("amenity");
                            Console.WriteLine($"Creating amenity ElasticSearch Index");
                            elasticSearchService.CreateIndex(ElasticStudio.IndexDescriptor);

                            Console.WriteLine($"Deleting feedback ElasticSearch Index");
                            elasticSearchService.DeleteIndex("feedback");
                            Console.WriteLine($"Creating feedback ElasticSearch Index");
                            elasticSearchService.CreateIndex(ElasticFeedback.IndexDescriptor);

                            Console.WriteLine($"Deleting chat ElasticSearch Index");
                            elasticSearchService.DeleteIndex("chat");
                            Console.WriteLine($"Creating chat ElasticSearch Index");
                            elasticSearchService.CreateIndex(ElasticChat.IndexDescriptor);
                        }

                        using (var context = scope.ServiceProvider.GetRequiredService<StudioContext>())
                        {
                            Console.WriteLine("Deleting MSSQL Database");
                            await context.Database.EnsureDeletedAsync();

                            Console.WriteLine("Creating MSSQL Database");
                            await context.Database.EnsureCreatedAsync();

                            Console.WriteLine("Seeding Sample Users");
                            await SeedUsersAsync(context);

                            Console.WriteLine("Seeding Sample Studios");
                            var studios = await SeedStudiosAsync(context);

                            Console.WriteLine("Seeding Stock Images");
                            await SeedStockImages(scope, iOptions);

                            Console.WriteLine("Seeding Sample Images");
                            await SeedSampleImages(scope, iOptions);

                            Console.WriteLine("Seeding Sample Studios with Sample Images");
                            await SeedSampleStudioImages(context, studios, scope, iOptions);

                            Console.WriteLine("Seeding Amenities");
                            await SeedAmenitiesAsync(context);

                            Console.WriteLine("Seeding Studio Rooms");
                            await SeedRooms(context, studios);

                            Console.WriteLine("Seeding Hours of Operation");
                            await SeedHoursOfOperation(context, studios);
                        }

                        Console.WriteLine("Data successfully seeded.");
                    }
                }
            }
        }

        private static async Task<List<Studio>> SeedStudiosAsync(StudioContext context)
        {
            var studios = SampleStudiosFromFile();
            var userIds = context.User.Select(u => u.IdentityUserId).ToList();
            var skillLevels = SkillLevels.ToList;
            var genres = Genres.ToList;
            var amenities = SampleAmenitiesFromFile();

            foreach (var studio in studios)
            {
                Random rnd = new Random();
                studio.UserId = userIds[rnd.Next(userIds.Count())];
                studio.SkillLevel = skillLevels[rnd.Next(skillLevels.Count())].Name;

                studio.Genres = string.Join(",", genres.OrderBy(x => rnd.Next(genres.Count()))
                                                                 .Take(rnd.Next(1, genres.Count))
                                                                 .Select(s => s.Name));

                studio.Amenities = string.Join(",", amenities.OrderBy(x => rnd.Next(amenities.Count()))
                                                                 .Take(rnd.Next(1, amenities.Count))
                                                                 .Select(s => s.Name));
            }

            await context.Studio.AddRangeAsync(studios);
            await context.SaveChangesAsync();
            return studios.ToList();
        }

        private static async Task SeedSampleStudioImages(StudioContext context, List<Studio> studios, IServiceScope scope, IOptions<AppSettings> options)
        {
            var configProvider = scope.ServiceProvider.GetRequiredService<IConfigurationProvider>();
            var mapper = new Mapper(configProvider);
            var imageService = scope.ServiceProvider.GetRequiredService<IImageService>();
            var imagePath = imageService.GetImagePath(options.Value.Name, ImageTypes.SubTypes.Profile);
            Common.Models.ApplicationFile theImage = new Common.Models.ApplicationFile(ImageTypes.Types.Image, ImageTypes.SubTypes.Profile);
            var sampleImages = Directory.GetFiles($"{imagePath}/original").Where(f => f.Contains("sample")).ToList();

            foreach (var studio in studios)
            {

                Random rnd = new Random();
                int r = rnd.Next(sampleImages.Count());
                theImage.Url = $"{options.Value.Url}/{options.Value.ImageFolderName}/{ImageTypes.SubTypes.Profile}/original/{Path.GetFileName(sampleImages[r])}";
                var mappedImage = mapper.DefaultContext.Mapper.Map<Entities.Models.ApplicationFile>(theImage);
                mappedImage.IsDefault = true;
                mappedImage.StudioId = studio.Id;
                studio.ApplicationFiles.Add(mappedImage);
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedRooms(StudioContext context, List<Studio> studios)
        {
            var amenities = SampleAmenitiesFromFile();
            foreach (var studio in studios)
            {
                Random rnd = new Random();
                int r = rnd.Next(amenities.Count);
                studio.Rooms.Add(new Room
                {
                    StudioId = studio.Id,
                });
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedHoursOfOperation(StudioContext context, List<Studio> studios)
        {
            foreach (var studio in studios)
            {
                Random rnd = new Random();
                studio.HoursOfOperation = RandomHoursOfOperation(rnd.Next(1, 5), studio.Id);
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedAmenitiesAsync(StudioContext context)
        {
            var amenities = SampleAmenitiesFromFile();

            foreach (var amenity in amenities)
            {
                await context.Amenity.AddAsync(amenity);
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedUsersAsync(StudioContext context)
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

        private static async Task SeedUsers(StudioContext context)
        {
            List<User> users = new List<User>();
            for (int i = 0; i < 1; i++)
            {
                users.Add(new User()
                {
                    IdentityUserId = Guid.NewGuid().ToString(),
                });
            }

            await context.User.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }

        private static async Task SeedStockImages(IServiceScope scope, IOptions<AppSettings> options)
        {
            var api = scope.ServiceProvider.GetRequiredService<ISeedSoundSeshGeneralAPI>();
            var stockImages = Directory.GetFiles("Data/stock-images");

            foreach (var stockImage in stockImages)
            {
                var uploadedImage = await api.UploadImage(options.Value.Name,
                        ImageTypes.Types.Image,
                        ImageTypes.SubTypes.Profile,
                        new Refit.StreamPart(File.OpenRead(stockImage), Path.GetFileName(stockImage)));
            }
        }

        private static async Task SeedSampleImages(IServiceScope scope, IOptions<AppSettings> options)
        {
            var api = scope.ServiceProvider.GetRequiredService<ISeedSoundSeshGeneralAPI>();
            var sampleImages = Directory.GetFiles("Data/images").ToList();
            var imageService = scope.ServiceProvider.GetRequiredService<IImageService>();
            var imagePath = imageService.GetImagePath(options.Value.Name, ImageTypes.SubTypes.Profile);
            sampleImages.RemoveAll(img => File.Exists($"{imagePath}/original/{Path.GetFileName(img)}"));
            if (sampleImages.Count() > 0)
            {
                Console.WriteLine($"Found {sampleImages.Count()} images to seed");
            }

            for (int i = sampleImages.Count(); i > 0; i--)
            {
                await api.UploadImage(options.Value.Name,
                    ImageTypes.Types.Image,
                    ImageTypes.SubTypes.Profile,
                    new Refit.StreamPart(File.OpenRead(sampleImages[i-1]), sampleImages[i-1]));

                if (i % 20 == 0)
                {
                    Console.WriteLine($"{i} images remaining...");
                }
            }
        }

        private static List<Studio> SampleStudiosFromFile()
        {
            string json = File.ReadAllText(@"Data\studioData.json");
            var sampleStudios = JsonConvert.DeserializeObject<List<Studio>>(json);
            return sampleStudios;
        }

        private static List<Amenity> SampleAmenitiesFromFile()
        {
            string json = File.ReadAllText(@"Data\amenityData.json");
            var sampleAmenities = JsonConvert.DeserializeObject<List<Amenity>>(json);
            return sampleAmenities;
        }

        private static List<string> SampleStudioImagesFromFile()
        {
            var logFile = File.ReadAllLines(@"Data\studioImages.txt");
            return new List<string>(logFile);
        }

        private static List<HoursOfOperation> RandomHoursOfOperation(int howMany, int studioId, int? roomId = null)
        {
            var result = new List<HoursOfOperation>();
            while (result.Count < howMany)
            {
                var randomDay = new Random().Next(General.DaysOfWeek.Count());
                if (!result.Any(r => r.DayOfWeek == General.DaysOfWeek[randomDay]))
                {
                    result.Add(new HoursOfOperation
                    {
                        StudioId = studioId,
                        RoomId = roomId,
                        DayOfWeek = General.DaysOfWeek[randomDay],
                        IsOpen = true,
                        Open = new Random().Next(5, 11).ToString() + ":00 AM",
                        Close = new Random().Next(2, 6).ToString() + ":00 PM"
                    });
                }
            }

            return result;
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
