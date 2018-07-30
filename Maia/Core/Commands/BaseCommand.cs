using Discord;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Core.Commands
{
    public abstract class BaseCommand : ICommand
    {
        protected IUser _author;
        protected IConfiguration _config;
        protected IMessageChannel _channel;
        protected IMessageWriter _messageWriter;
        protected string[] _parameters;

        public BaseCommand(IUser author, IConfiguration config, IMessageChannel channel,
            IMessageWriter messageWriter, params string[] parameters)
        {
            _parameters = parameters;
            _author = author;
            _channel = channel;
            _config = config;
            _messageWriter = messageWriter;
        }

        public abstract Task ExecuteAsync();

        public virtual bool CanExecute() => (ValidateParameters() && ValidateAuthor());

        public virtual bool ValidateParameters() => (_parameters.Length == 0);

        public virtual bool ValidateAuthor() => (_author.Id == GetOwnerId());

        public async virtual Task InvalidUseOfCommand()
        {
            await _messageWriter.Send("Invalid use of command", _author, _channel);
        }

        public async virtual Task Reply(string message)
        {
            await _messageWriter.Send(message, _author, _channel);
        }

        public ulong GetOwnerId()
        {
            string owner = _config.GetValue(ConfigKeys.OwnerID);
            ulong ownerID;
            if (ulong.TryParse(owner, out ownerID) == false)
                return default;
            return ownerID;
        }
    }
}
