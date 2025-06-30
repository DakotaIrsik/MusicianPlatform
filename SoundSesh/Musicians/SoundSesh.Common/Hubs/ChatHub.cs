﻿using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SoundSesh.Common.Hubs
{
    
    public class ChatHub : Hub
    {

        [HubMethodName("SendMessage")]
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("broadcastchartdata", user, message);
        }
    }
}