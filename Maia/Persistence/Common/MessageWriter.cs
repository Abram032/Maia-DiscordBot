using Discord;
using Maia.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Common
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
