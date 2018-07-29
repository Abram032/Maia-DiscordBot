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
    class GithubCommand : BaseCommand, ICommand
    {
        public GithubCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, params string[] parameters) : base(author, config, channel, messageWriter, parameters)
        {
        }

        public override async Task ExecuteAsync()
        {
            if(CanExecute())
                await _messageWriter.Send(Info.projectRepositoryURL, _author, _channel);
            else
                await _messageWriter.Send("Invalid use of command!", _author, _channel);
        }

        public override bool ValidateAuthor()
        {
            return true;
        }
    }
}
