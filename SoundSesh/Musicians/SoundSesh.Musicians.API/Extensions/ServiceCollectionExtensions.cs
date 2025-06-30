using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SoundSesh.Common.Models;
using SoundSesh.Musicians.Core.BusinessLogic;

namespace SoundSesh.Musicians.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            
            services.AddTransient<IMusicianDomain, MusicianDomain>();
            services.AddTransient<IUserDomain, UserDomain>();
            services.AddTransient<IDiagnosticsDomain, DiagnosticsDomain>();
            services.AddTransient<IFeedbackDomain, FeedbackDomain>();
            services.AddTransient<IChatDomain, ChatDomain>();

            services.AddTransient<IMapper, Mapper>();
            services.AddTransient<Message, Message>();
            services.AddTransient<Error, Error>();

            services.AddTransient<IBaseDomain, BaseDomain>();
            
            return services;
        }
    }
}
