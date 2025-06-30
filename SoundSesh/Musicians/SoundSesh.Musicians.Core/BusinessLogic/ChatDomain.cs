using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using SoundSesh.Common;
using SoundSesh.Common.Constants;
using SoundSesh.Common.Hubs;
using SoundSesh.Common.Services;
using SoundSesh.Musicians.Entities.DTOs;
using SoundSesh.Musicians.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SoundSesh.Musicians.Core.BusinessLogic
{

    public interface IChatDomain
    {
        void Get(string userId);
    }
    public class ChatDomain : BaseDomain, IChatDomain
    {
        private IHubContext<ChatHub> _chatHub;
        public ChatDomain(MusicianContext context,
            IMapper mapper,
            IElasticSearchService elastic,
            ILogger logger,
            IHttpContextAccessor httpContextAccessor,
            IOptions<AppSettings> settings,
            IHubContext<ChatHub> chatHub) : base(context, mapper, elastic, logger, httpContextAccessor, settings)
        {
            _chatHub = chatHub;
        }

        public void Get(string userId)
        {
            var timerManager = new TimerService(() => _chatHub.Clients.All.SendAsync("transferchartdata", WithUser(userId)));
        }

        public ChatDTO WithUser(string userId)
        {
            Thread.Sleep(3000);
            var chat = GenerateSampleChat(userId);
            return _mapper.Map<ChatDTO>(chat);
        }

        public Chat GenerateSampleChat(string userId)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MusicianContext>();
            optionsBuilder.UseSqlServer(_settings.Value.ConnectionStrings.MSSQL);

            var x2 = UserId;
            using (var context = new MusicianContext(optionsBuilder.Options, _http, _elastic, _settings, _mapper))
            {
                var demoChat = new Chat()
                {
                    ToUserId = context.User.OrderBy(x => new Random().Next(1, 1000)).FirstOrDefault().IdentityUserId,
                    Message = Strings.LoremIpsum(3, 20, 1, 3, 1, false),
                    UserId = userId
                };

                context.Add(demoChat);
                context.SaveChanges();
                return demoChat;
            }
        }
    }
}
