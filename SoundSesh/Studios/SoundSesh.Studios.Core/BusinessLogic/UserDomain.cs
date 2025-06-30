using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog;
using SoundSesh.Common;
using SoundSesh.Common.Services;
using SoundSesh.Studios.Entities.DTOs;
using SoundSesh.Studios.Entities.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SoundSesh.Studios.Core.BusinessLogic
{
    public interface IUserDomain
    {
        Task CreateOrUpdateUser();
        UserDTO Get(int id, string identityuserId = null);
    }
    public class UserDomain : BaseDomain, IUserDomain
    {
        public UserDomain(StudioContext context, 
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

        public UserDTO Get(int id, string identityUserId = null)
        {
            UserDTO result = null;
            var user = _context.User.SingleOrDefault(u => u.Id == id || u.IdentityUserId == identityUserId);
            if (user != null)
            {
                result = _mapper.Map<UserDTO>(user);
            }

            return result;
        }
    }
}
