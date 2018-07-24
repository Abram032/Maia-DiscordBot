using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Core.Common
{
    interface ICommandHandler
    {
        Task Handle(IMessage message);
    }
}
