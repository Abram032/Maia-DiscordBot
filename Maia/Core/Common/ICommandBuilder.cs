using Discord;
using DiscordBot.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Core.Common
{
    interface ICommandBuilder
    {
        ICommand BuildCommand(string command, List<string> parameters, IUser author, IMessageChannel channel);
    }
}
