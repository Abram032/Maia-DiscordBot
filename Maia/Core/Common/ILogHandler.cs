using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Core.Common
{
    public interface ILogHandler
    {
        Task Handle(LogMessage log);
    }
}
