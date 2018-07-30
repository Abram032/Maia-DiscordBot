using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Commands
{
    class FlipcoinCommand : BaseCommand, ICommand
    {
        Random random = new Random();

        public FlipcoinCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, params string[] parameters) : base(author, config, channel, messageWriter, parameters)
        {
        }

        public override async Task ExecuteAsync()
        {
            if(CanExecute())
            {
                string message = Generate() == 0 ? "Heads!" : "Tails!";
                await _messageWriter.Send(message, _author, _channel);
            }
            else
                await _messageWriter.Send("Invalid use of command!", _author, _channel);
        }

        public override bool ValidateAuthor()
        {
            return true;
        }

        private int Generate() => random.Next(0,2);
    }
}
