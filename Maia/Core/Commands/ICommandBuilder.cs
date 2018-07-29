using Discord;
using Maia.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Core.Commands
{
    public interface ICommandBuilder
    {
        ICommand BuildCommand(string command, IUser author, IMessageChannel channel, params string[] parameters);
    }
}
