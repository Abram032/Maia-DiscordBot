using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Core.Common
{
    interface IMessageWriter
    {
        Task Send(string message, IUser author, IMessageChannel channel);
    }
}
