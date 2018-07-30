using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Audio;

namespace Maia.Persistence.Commands
{
    class SummonCommand : BaseCommand, ICommand
    {
        public SummonCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, params string[] parameters) 
            : base(author, config, channel, messageWriter, parameters)
        {
        }

        public override async Task ExecuteAsync()
        {
            if(CanExecute())
            {
                
            }
            else
                await InvalidUseOfCommand();
        }
    }
}
