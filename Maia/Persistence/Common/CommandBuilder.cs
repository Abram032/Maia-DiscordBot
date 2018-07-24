using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Persistence.Commands;
using Maia.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Persistence.Common
{
    class CommandBuilder : ICommandBuilder
    {
        private IConfiguration _config;
        private IMessageWriter _messageWriter;
        private IConnectionHandler _connectionHandler;

        public CommandBuilder(IConfiguration config, IMessageWriter messageWriter, IConnectionHandler connectionHandler)
        {
            _config = config;
            _messageWriter = messageWriter;
            _connectionHandler = connectionHandler;
        }

        public ICommand BuildCommand(string command, List<string> parameters, IUser author, IMessageChannel channel)
        {
            ICommand _command = null;
            switch(command)
            {
                case CommandNames.exit:
                    _command = new CommandExit(parameters, author, channel, _config, _messageWriter, _connectionHandler);
                    break;
                case CommandNames.restart:
                    break;
                case CommandNames.help:
                    break;
                case CommandNames.info:
                    break;
                case CommandNames.random:
                    break;
                default:
                    break;
            }
            return _command;
        }
    }
}
