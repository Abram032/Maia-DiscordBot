using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Core.Common
{
    interface IMessageHandler
    {
        Task Handle();
    }
}
