using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog;
using SoundSesh.Common;
using SoundSesh.Common.Models;
using SoundSesh.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoundSesh.General.Core.BusinessLogic
{
    public interface IBaseDomain
    {
        void ValidateAndSaveChanges();
        Task ValidateAndSaveChangesAsync();
        bool HasErrors { get; }
        bool HasMessages { get; }
        List<Error> GetErrors();

    }
    public class BaseDomain : IBaseDomain
    {
        protected readonly IElasticSearchService _elastic;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        private readonly IHttpContextAccessor _http;

        public List<Error> Errors { get; set; } = new List<Error>();
        public List<Message> Messages { get; set; } = new List<Message>();

        public BaseDomain(IMapper mapper,
                            IElasticSearchService elastic,
                            ILogger logger,
                            IHttpContextAccessor httpContext,
                            IOptions<AppSettings> settings)
        {
            _elastic = elastic;
            _mapper = mapper;
            _logger = logger;
            _http = httpContext;
        }


        public void ValidateAndSaveChanges()
        {
            throw new NotImplementedException();
        }

        public async Task ValidateAndSaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        protected string UserId => _http?.HttpContext?.User?.Claims?.SingleOrDefault(c => c.Type == "sub")?.Value;

        public bool HasErrors => Errors.Any();

        public bool HasMessages => Messages.Any();

        public List<Error> GetErrors()
        {
            return Errors;
        }
    }
}
