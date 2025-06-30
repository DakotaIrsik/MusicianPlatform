using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog;
using SoundSesh.Common;
using SoundSesh.Common.Services;
using SoundSesh.Musicians.Entities.DTOs;
using SoundSesh.Musicians.Entities.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SoundSesh.Musicians.Core.BusinessLogic
{
    public interface IUserDomain
    {
        Task CreateOrUpdateUser();
        UserDTO Get();
    }
    public class UserDomain : BaseDomain, IUserDomain
    {
        public UserDomain(MusicianContext context, 
            IMapper mapper, 
            IElasticSearchService elastic, 
            ILogger logger, 
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettings> settings) : base(context, mapper, elastic, logger, httpContextAccessor, settings)
        {
        }

        public async Task CreateOrUpdateUser()
        {
            var user = _context.User.SingleOrDefault(m => m.IdentityUserId == UserId);
            if (user == null)
            {
                _context.User.Add(new User { IdentityUserId = UserId });
            }

            await ValidateAndSaveChangesAsync();
        }

        public UserDTO Get()
        {
            UserDTO result = null;
            var user = _context.User.SingleOrDefault(u => u.IdentityUserId == UserId);
            if (user != null)
            {
                result = _mapper.Map<UserDTO>(user);
            }

            return result;
        }
    }
}
