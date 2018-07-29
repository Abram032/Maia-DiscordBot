using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Persistence.Commands;
using Maia.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Persistence.Commands
{
    class CommandBuilder : ICommandBuilder
    {
        private IConfiguration _config;
        private IMessageWriter _messageWriter;
        private ICommandsInfo _commandsInfo;

        public CommandBuilder(IConfiguration config, IMessageWriter messageWriter, ICommandsInfo commandsInfo)
        {
            _config = config;
            _messageWriter = messageWriter;
            _commandsInfo = commandsInfo;
        }

        public ICommand BuildCommand(string command, IUser author, IMessageChannel channel, params string[] parameters)
        {
            ICommand _command = null;
            switch(command)
            {
                case CommandNames.exit:
                    _command = new ExitCommand(author, _config, channel, _messageWriter, parameters);
                    break;
                case CommandNames.update:
                    _command = new UpdateCommand(author, _config, channel, _messageWriter, parameters);
                    break;
                case CommandNames.restart:
                    _command = new RestartCommand(author, _config, channel, _messageWriter, parameters);
                    break;
                case CommandNames.help:
                    _command = new HelpCommand(author, _config, channel, _messageWriter, _commandsInfo, parameters);
                    break;
                case CommandNames.version:
                    _command = new VersionCommand(author, _config, channel, _messageWriter, parameters);
                    break;
                case CommandNames.random:
                    _command = new RandomCommand(author, _config, channel, _messageWriter, parameters);
                    break;
                case CommandNames.github:
                    _command = new GithubCommand(author, _config, channel, _messageWriter, parameters);
                    break;
                case CommandNames.uptime:
                    _command = new UptimeCommand(author, _config, channel, _messageWriter, parameters);
                    break;
                default:
                    break;
            }
            return _command;
        }
    }
}
