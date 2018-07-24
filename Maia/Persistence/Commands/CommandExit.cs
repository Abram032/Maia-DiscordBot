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
    class CommandExit : ICommand
    {
        private IConfiguration _config;
        private List<string> _parameters;
        private IUser _author;
        private IMessageChannel _channel;
        private IMessageWriter _messageWriter;
        private IConnectionHandler _connectionHandler;

        public CommandExit(List<string> parameters, IUser author, IMessageChannel channel, IConfiguration config, IMessageWriter messageWriter, IConnectionHandler connectionHandler)
        {
            _parameters = parameters;
            _author = author;
            _channel = channel;
            _config = config;
            _messageWriter = messageWriter;
            _connectionHandler = connectionHandler;
        }

        public string Help => throw new NotImplementedException();

        public async Task Execute()
        {
            if (ValidateParameters())
            {
                if (ValidateAuthor())
                {
                    await _messageWriter.Send("Cya!", _author, _channel);
                    await _connectionHandler.Disconnect();
                    Environment.Exit(0);
                }
                else
                    await _messageWriter.Send("Not authorized!", _author, _channel);   
            }
            else
                await _messageWriter.Send("Invalid use of command!", _author, _channel);
            await Task.CompletedTask;
        }

        private bool ValidateParameters()
        {
            if (_parameters.Count > 0)
                return false;
            return true;
        }

        private bool ValidateAuthor()
        {
            if (_author.Id == GetOwner())
                return true;
            return false;
        }

        private ulong GetOwner()
        {
            string owner = _config.GetValue(ConfigKeys.OwnerID);
            ulong ownerID;
            if (ulong.TryParse(owner, out ownerID) == false)
                return default;
            return ownerID;
        }
    }
}
