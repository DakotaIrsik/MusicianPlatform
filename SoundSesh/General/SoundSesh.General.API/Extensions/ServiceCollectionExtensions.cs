using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SoundSesh.Common.Models;
using SoundSesh.General.Core.BusinessLogic;

namespace SoundSesh.General.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddTransient<IMapper, Mapper>();
            services.AddTransient<Message, Message>();
            services.AddTransient<Error, Error>();

            services.AddTransient<IBaseDomain, BaseDomain>();
            services.AddTransient<IImageDomain, ImageDomain>();
            return services;
        }
    }
}
