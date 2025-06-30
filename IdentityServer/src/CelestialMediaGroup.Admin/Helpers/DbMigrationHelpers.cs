using CelestialMediaGroup.Admin.Configuration;
using CelestialMediaGroup.Admin.Configuration.Constants;
using CelestialMediaGroup.Admin.Configuration.Identity;
using CelestialMediaGroup.Admin.Configuration.IdentityServer;
using CelestialMediaGroup.Admin.Configuration.Interfaces;
using IdentityModel;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Skoruba.IdentityServer4.Admin.EntityFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityModel.OidcConstants;
using efModels = IdentityServer4.Models;

namespace CelestialMediaGroup.Admin.Helpers
{
    public static class DbMigrationHelpers
    {
        /// <summary>
        /// Generate migrations before running this method, you can use these steps bellow:
        /// https://github.com/skoruba/IdentityServer4.Admin#ef-core--data-access
        /// </summary>
        /// <param name="host"></param>      
        public static async Task EnsureSeedData<TIdentityServerDbContext, TIdentityDbContext, TPersistedGrantDbContext, TLogDbContext, TUser, TRole>(IWebHost host)
            where TIdentityServerDbContext : DbContext, IAdminConfigurationDbContext
            where TIdentityDbContext : DbContext
            where TPersistedGrantDbContext : DbContext, IAdminPersistedGrantDbContext
            where TLogDbContext : DbContext, IAdminLogDbContext
            where TUser : IdentityUser, new()
            where TRole : IdentityRole, new()
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                await EnsureDatabasesMigrated<TIdentityDbContext, TIdentityServerDbContext, TPersistedGrantDbContext, TLogDbContext>(services);
                await EnsureSeedData<TIdentityServerDbContext, TUser, TRole>(services);
            }
        }

        public static async Task EnsureDatabasesMigrated<TIdentityDbContext, TConfigurationDbContext, TPersistedGrantDbContext, TLogDbContext>(IServiceProvider services)
            where TIdentityDbContext : DbContext
            where TPersistedGrantDbContext : DbContext
            where TConfigurationDbContext : DbContext
            where TLogDbContext : DbContext
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<TPersistedGrantDbContext>())
                {
                    await context.Database.MigrateAsync();
                }

                using (var context = scope.ServiceProvider.GetRequiredService<TIdentityDbContext>())
                {
                    await context.Database.MigrateAsync();
                }

                using (var context = scope.ServiceProvider.GetRequiredService<TConfigurationDbContext>())
                {
                    await context.Database.MigrateAsync();
                }

                using (var context = scope.ServiceProvider.GetRequiredService<TLogDbContext>())
                {
                    await context.Database.MigrateAsync();
                }
            }
        }

        public static async Task EnsureSeedData<TIdentityServerDbContext, TUser, TRole>(IServiceProvider serviceProvider)
        where TIdentityServerDbContext : DbContext, IAdminConfigurationDbContext
        where TUser : IdentityUser, new()
        where TRole : IdentityRole, new()
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<TIdentityServerDbContext>();
                var userManager = services.GetRequiredService<UserManager<TUser>>();
                var roleManager = services.GetRequiredService<RoleManager<TRole>>();
                var rootConfiguration = services.GetRequiredService<IRootConfiguration>();
                var realRootConfiguration = services.GetRequiredService<IConfiguration>();

                var logger = new LoggerConfiguration()
                              .ReadFrom.Configuration(realRootConfiguration)
                              .CreateLogger();



                await EnsureSeedIdentityServerData(context, rootConfiguration.AdminConfiguration);
                await EnsureSeedIdentityData(userManager, roleManager);
                await EnsureDbReadWriteAccessFromIIS(context, logger);
                await EnsureSeedSoundSeshWebClients(context, realRootConfiguration, logger);
                await EnsureSeedSoundSeshAPIs(context, realRootConfiguration, logger);
            }
        }

        /// <summary>
        /// Generate default admin user / role
        /// </summary>
        private static async Task EnsureSeedIdentityData<TUser, TRole>(UserManager<TUser> userManager,
            RoleManager<TRole> roleManager)
            where TUser : IdentityUser, new()
            where TRole : IdentityRole, new()
        {
            // Create admin role
            if (!await roleManager.RoleExistsAsync(AuthorizationConsts.AdministrationRole))
            {
                var role = new TRole { Name = AuthorizationConsts.AdministrationRole };

                await roleManager.CreateAsync(role);
            }

            // Create admin user
            if (await userManager.FindByNameAsync(Users.AdminUserName) != null) return;

            var user = new TUser
            {
                UserName = Users.AdminUserName,
                Email = Users.AdminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, Users.AdminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, AuthorizationConsts.AdministrationRole);
            }
        }

        /// <summary>
        /// Generate default clients, identity and api resources
        /// </summary>
        private static async Task EnsureSeedIdentityServerData<TIdentityServerDbContext>(TIdentityServerDbContext context, IAdminConfiguration adminConfiguration)
            where TIdentityServerDbContext : DbContext, IAdminConfigurationDbContext
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Clients.GetAdminClient(adminConfiguration).ToList())
                {
                    await context.Clients.AddAsync(client.ToEntity());
                }

                await context.SaveChangesAsync();
            }

            if (!context.IdentityResources.Any())
            {
                var identityResources = ClientResources.GetIdentityResources().ToList();

                foreach (var resource in identityResources)
                {
                    await context.IdentityResources.AddAsync(resource.ToEntity());
                }

                await context.SaveChangesAsync();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in ClientResources.GetApiResources().ToList())
                {
                    await context.ApiResources.AddAsync(resource.ToEntity());
                }

                await context.SaveChangesAsync();
            }
        }

        private static async Task EnsureDbReadWriteAccessFromIIS<TIdentityServerDbContext>(TIdentityServerDbContext context, ILogger logger)
            where TIdentityServerDbContext : DbContext, IAdminConfigurationDbContext
        {
            //TODO config drive this per environment, mine runs under IIS APPPOOL\IdentityServer, brain hurts...
            //QC
            await CreateServerLogin(context, "cmgis", logger);
            await CreateServerLogin(context, "cmgisadmin", logger);
            await CreateDbLogin(context, "cmgis", logger);
            await CreateDbLogin(context, "cmgisadmin", logger);
            await CreateDbPermission(context, "cmgis", "reader", logger);
            await CreateDbPermission(context, "cmgis", "writer", logger);
            await CreateDbPermission(context, "cmgisadmin", "reader", logger);
            await CreateDbPermission(context, "cmgisadmin", "writer", logger);


            //Dev and Debug
            await CreateServerLogin(context, "IdentityServer", logger);
            await CreateServerLogin(context, "AdminIdentityServer", logger);
            await CreateDbLogin(context, "IdentityServer", logger);
            await CreateDbLogin(context, "AdminIdentityServer", logger);
            await CreateDbPermission(context, "IdentityServer", "reader", logger);
            await CreateDbPermission(context, "IdentityServer", "writer", logger);
            await CreateDbPermission(context, "AdminIdentityServer", "reader", logger);
            await CreateDbPermission(context, "AdminIdentityServer", "writer", logger);
        }

        private static async Task CreateServerLogin<TIdentityServerDbContext>(TIdentityServerDbContext context, string applicationPoolName, ILogger logger) where TIdentityServerDbContext : DbContext, IAdminConfigurationDbContext
        {
            try
            {
                await context.Database.ExecuteSqlCommandAsync(CreateServerLoginScript(applicationPoolName));
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An exception occured while Creating {applicationPoolName} Login on Server.");
            }
        }

        private static async Task CreateDbLogin<TIdentityServerDbContext>(TIdentityServerDbContext context, string applicationPoolName, ILogger logger) where TIdentityServerDbContext : DbContext, IAdminConfigurationDbContext
        {
            try
            {
                await context.Database.ExecuteSqlCommandAsync(CreateDbLoginScript(applicationPoolName));
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An exception occured while Creating {applicationPoolName} Login on Database: IdentityServer.");
            }
        }

        private static async Task CreateDbPermission<TIdentityServerDbContext>(TIdentityServerDbContext context, string applicationPoolName, string permission, ILogger logger) where TIdentityServerDbContext : DbContext, IAdminConfigurationDbContext
        {
            try
            {
                await context.Database.ExecuteSqlCommandAsync(CreateDbPermission(applicationPoolName, permission));
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"An exception occured while adding {permission} permission to {applicationPoolName}.");
            }
        }

        private static async Task EnsureSeedSoundSeshWebClients<TIdentityServerDbContext>(TIdentityServerDbContext context, IConfiguration realRootConfiguration, ILogger logger) where TIdentityServerDbContext : DbContext, IAdminConfigurationDbContext
        {
            var customConfigurations = GetCustomWebClientConfigurations(realRootConfiguration);

            foreach (var customConfiguration in customConfigurations)
            {
                var client = GetEfClient(customConfiguration);

                if (!context.Clients.Any(c => c.ClientId == client.ClientId))
                {
                    await context.Clients.AddAsync(client.ToEntity());
                }
            }

            await context.SaveChangesAsync();
        }


        private static async Task EnsureSeedSoundSeshAPIs<TIdentityServerDbContext>(TIdentityServerDbContext context, IConfiguration realRootConfiguration, ILogger logger) where TIdentityServerDbContext : DbContext, IAdminConfigurationDbContext
        {
            var customConfigurations = GetCustomAPIConfigurations(realRootConfiguration);

            foreach (var customConfiguration in customConfigurations)
            {
                var api = GetEfAPIResource(customConfiguration);

                if (!context.ApiResources.Any(c => c.Name == api.Name))
                {
                    await context.ApiResources.AddAsync(api.ToEntity());
                }
            }

            await context.SaveChangesAsync();
        }

        private static efModels.Client GetEfClient(CustomWebClientConfiguration customConfiguration)
        {
            return new efModels.Client()
            {

                ClientId = customConfiguration.ClientId,
                ClientName = customConfiguration.ClientName,
                ClientUri = customConfiguration.BaseUrl,
                AllowedGrantTypes = { GrantTypes.Implicit },
                ClientSecrets = new List<efModels.Secret>
                {
                    new efModels.Secret(customConfiguration.ClientSecret.ToSha256())
                },
                RedirectUris = customConfiguration.RedirectUri,
                FrontChannelLogoutUri = $"{customConfiguration.BaseUrl}",
                PostLogoutRedirectUris = { $"{customConfiguration.BaseUrl}" },
                AllowedCorsOrigins = { customConfiguration.BaseUrl },
                AllowedScopes = customConfiguration.Scopes,
                AllowAccessTokensViaBrowser = true //todo this should probably change.
            };
        }

        private static efModels.ApiResource GetEfAPIResource(CustomAPIConfiguration customConfiguration)
        {
            var resource = new efModels.ApiResource()
            {
                Name = customConfiguration.ApiName,
                DisplayName = customConfiguration.ApiDisplayName,
                ApiSecrets = new List<efModels.Secret>
                {
                    new efModels.Secret(customConfiguration.ApiSecret.ToSha256())
                }
            };

            foreach (var scope in customConfiguration.Scopes)
            {
                resource.Scopes.Add(new efModels.Scope { Name = scope, DisplayName = scope });
            }

            return resource;
        }

        private static List<CustomWebClientConfiguration> GetCustomWebClientConfigurations(IConfiguration realRootConfiguration)
        {
            var customConfigurations = realRootConfiguration.GetSection(ConfigurationConsts.CustomWebClientConfigurationsKey).GetChildren();
            var listOfCustomConfigurations = new List<CustomWebClientConfiguration>();
            foreach (var customConfiguration in customConfigurations)
            {
                listOfCustomConfigurations.Add(customConfiguration.Get<CustomWebClientConfiguration>());
            }
            return listOfCustomConfigurations;
        }

        private static List<CustomAPIConfiguration> GetCustomAPIConfigurations(IConfiguration realRootConfiguration)
        {
            var customConfigurations = realRootConfiguration.GetSection(ConfigurationConsts.CustomAPIConfigurationsKey).GetChildren();
            var listOfCustomConfigurations = new List<CustomAPIConfiguration>();
            foreach (var customConfiguration in customConfigurations)
            {
                listOfCustomConfigurations.Add(customConfiguration.Get<CustomAPIConfiguration>());
            }
            return listOfCustomConfigurations;
        }

        private static string CreateServerLoginScript(string applicationPoolName)
        {
            return $"USE master CREATE LOGIN [IIS APPPOOL\\{applicationPoolName}] FROM WINDOWS WITH DEFAULT_DATABASE=[IdentityServer], DEFAULT_LANGUAGE=[us_english]";
        }

        private static string CreateDbLoginScript(string applicationPoolName)
        {
            return $"USE IdentityServer CREATE USER [IIS APPPOOL\\{applicationPoolName}] FROM LOGIN [IIS APPPOOL\\{applicationPoolName}]";
        }

        private static string CreateDbPermission(string applicationPoolName, string permission)
        {
            return $"USE IdentityServer EXECUTE sp_addrolemember 'db_data{permission}', 'IIS APPPOOL\\{applicationPoolName}'";
        }
    }
}
