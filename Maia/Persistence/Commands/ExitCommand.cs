using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Commands
{
    class ExitCommand : BaseCommand, ICommand
    {
        public ExitCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, params string[] parameters)
            : base(author, config, channel, messageWriter, parameters)
        {
        }

        public override async Task ExecuteAsync()
        {
            if(CanExecute())
            {
                await _messageWriter.Send("Cya!", _author, _channel);
                Environment.Exit(0);
            }
            else
                await _messageWriter.Send("Invalid use of command or not authorized!", _author, _channel);
        }
    }
}
