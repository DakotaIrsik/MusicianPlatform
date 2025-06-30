using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SoundSesh.common.Services;
using SoundSesh.Common.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
namespace SoundSesh.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration);
            return services;
        }

        public static bool IsLocal(this IHostingEnvironment env)
        {
            return env.IsEnvironment("Local");
        }

        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsLocal() || env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            return app;
        }

        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            return services;
        }

      

        public static IServiceCollection AddMvc(this IServiceCollection services, AppSettings settings)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new ResponseCacheAttribute
                {
                    Duration = 0
                });

            });

            return services;
        }

       

        public static IServiceCollection AddIdentityServerImplementation(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(x =>
                    {
                        x.Authority = "http://localhost:44317";
                        x.ApiSecret = "93ffd43c-dfab-42b9-af2d-8583284d5fe8";
                        x.ApiName = "WOLF WebAPI Client";
                        x.SupportedTokens = SupportedTokens.Both;
                        x.RequireHttpsMetadata = false;
                    });

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddDeveloperSigningCredential();


            return services;
        }

        public static IServiceCollection AddHttpContext(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient(
                provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

            return services;
        }

        private static IServiceCollection AddNoCachingService(this IServiceCollection services)
        {
            services.AddScoped<ICacheService, NoCacheService>();
            return services;
        }

        private static IServiceCollection AddRedisCachingService(this IServiceCollection services)
        {
            services.AddScoped<ICacheService, RedisCacheService>();
            return services;
        }

        public static IServiceCollection AddIdentityServerAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.Get<AppSettings>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";

            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = "Cookies";
                    options.Authority = configuration.GetConnectionString("IdentityServerConnection");
                    options.RequireHttpsMetadata = false;
                    options.ClientId = settings.ClientId;
                    options.ResponseType = "code id_token token";
                    options.SaveTokens = true;
                    options.Scope.Clear();
                    options.Scope.Add(AuthenticationConsts.ScopeOpenId);
                    options.Scope.Add(AuthenticationConsts.ScopeProfile);
                    options.Scope.Add(AuthenticationConsts.ScopeRoles);
                    options.Scope.Add("api1");
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role",
                    };
                    options.ClaimActions.MapJsonKey("role", "role");
                });

            return services;
        }
    }

}
