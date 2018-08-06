using Discord;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Core.Validation;
using Maia.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Core.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public string[] Parameters { get; private set; }
        public IUser Author { get; private set; }
        public IMessageChannel Channel { get; private set; }
        protected IConfiguration _config;
        protected IMessageWriter _messageWriter;
        protected IValidationHandler _validationHandler;

        public BaseCommand(IUser author, IConfiguration config, IMessageChannel channel,
            IMessageWriter messageWriter, IValidationHandler validationHandler, params string[] parameters)
        {
            Parameters = parameters;
            Author = author;
            Channel = channel;
            _config = config;
            _messageWriter = messageWriter;
            _validationHandler = validationHandler;
        }

        public abstract Task ExecuteAsync();

        public abstract bool CanExecute();

        //TODO: Remove InvalidUseOfCommand method and throw exception for it.
        public async virtual Task InvalidUseOfCommand()
        {
            await _messageWriter.Send("Invalid use of command", Author, Channel);
        }

        public async virtual Task SendMessageAsync(string message)
        {
            await _messageWriter.Send(message, Author, Channel);
        }

        public ulong GetOwnerId()
        {
            string owner = _config.GetValue(ConfigKeys.OwnerID);
            if (ulong.TryParse(owner, out ulong ownerID) == false)
                return default;
            return ownerID;
        }
    }
}
