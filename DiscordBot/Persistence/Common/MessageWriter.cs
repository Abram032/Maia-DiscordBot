using Discord;
using DiscordBot.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Persistence.Common
{
    class MessageWriter : IMessageWriter
    {
        public async Task Send(string message, IUser author, IMessageChannel channel)
        {
            string _message = author.Mention + " " + message;
            await channel.SendMessageAsync(_message);
        }
    }
}
