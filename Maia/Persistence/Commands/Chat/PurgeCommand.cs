using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Core.Validation;
using Maia.Persistence.Validation.Context;
using Maia.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Commands.Chat
{
    class PurgeCommand : BaseCommand, ICommand
    {
        IConnectionHandler _connectionHandler;

        public PurgeCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, IConnectionHandler connectionHandler, IValidationHandler validationHandler, params string[] parameters) 
            : base(author, config, channel, messageWriter, validationHandler, parameters)
        {
            _connectionHandler = connectionHandler;
        }

        public override bool CanExecute()
        {
            ICommandValidationContext context = new PurgeCommandValidationContext(_config);
            var result = _validationHandler.Validate(context, this);
            return result.IsSuccessful;
        }

        public override async Task ExecuteAsync()
        {
            if(CanExecute())
            {
                int amount = 100;
                if(Parameters.Length == 1)
                    amount = int.Parse(Parameters[0]);
                var messages = await Channel.GetMessagesAsync(amount).FlattenAsync();
                ulong botId = _connectionHandler.Client.CurrentUser.Id;
                List<IMessage> messagesToPurge = new List<IMessage>();
                foreach(var message in messages)
                {
                   if(CheckMessage(message, botId))
                        messagesToPurge.Add(message);
                }
                await _messageWriter.Send("PUUUUUUURGE!!!!!", Author, Channel);
                foreach(var message in messagesToPurge)
                    await message.DeleteAsync();
            }
            else
                await InvalidUseOfCommand();
        }

        private bool CheckMessage(IMessage message, ulong botId)
        {
            if(message.Author.Id.Equals(botId))
                return true;
            else if(message.Content.StartsWith(_config.GetValueOrDefault(ConfigKeys.CommandPrefix)))
                return true;
            return false;
        }
    }
}
