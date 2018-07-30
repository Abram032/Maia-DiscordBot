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
    class PurgeCommand : BaseCommand, ICommand
    {
        IConnectionHandler _connectionHandler;

        public PurgeCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, IConnectionHandler connectionHandler, params string[] parameters) 
            : base(author, config, channel, messageWriter, parameters)
        {
            _connectionHandler = connectionHandler;
        }

        public override async Task ExecuteAsync()
        {
            if(CanExecute())
            {
                int amount = 100;
                if(_parameters.Length == 1)
                    amount = int.Parse(_parameters[0]);
                var messages = await _channel.GetMessagesAsync(amount).FlattenAsync();
                ulong botId = _connectionHandler.Client.CurrentUser.Id;
                List<IMessage> messagesToPurge = new List<IMessage>();
                foreach(var message in messages)
                {
                   if(ValidateMessage(message, botId))
                        messagesToPurge.Add(message);
                }
                await _messageWriter.Send("PUUUUUUURGE!!!!!", _author, _channel);
                foreach(var message in messagesToPurge)
                    await message.DeleteAsync();
            }
            else
                await InvalidUseOfCommand();
        }

        public override bool ValidateParameters()
        {
            if(_parameters.Length == 0)
                return true;
            if(_parameters.Length > 1)
                return false;
            if(int.TryParse(_parameters[0], out int value) == false)
                return false;
            if(value > 100 || value < 0)
                return false;
            return true;
        }

        private bool ValidateMessage(IMessage message, ulong botId)
        {
            if(message.Author.Id.Equals(botId))
                return true;
            else if(message.Content.StartsWith(_config.GetValueOrDefault(ConfigKeys.CommandPrefix)))
                return true;
            return false;
        }
    }
}
