using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Core.Validation;
using Maia.Persistence.Commands;
using Maia.Persistence.Commands.Audio;
using Maia.Persistence.Commands.Bot;
using Maia.Persistence.Commands.Chat;
using Maia.Persistence.Commands.Info;
using Maia.Persistence.Commands.Misc;
using Maia.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Persistence.Commands
{
    class CommandBuilder : ICommandBuilder
    {
        private readonly IConfiguration _config;
        private readonly IMessageWriter _messageWriter;
        private readonly ICommandsInfo _commandsInfo;
        private readonly IConnectionHandler _connectionHandler;
        private readonly IAudioService _audioService;
        private readonly IValidationHandler _validationHandler;

        public CommandBuilder(IConfiguration config, IMessageWriter messageWriter, ICommandsInfo commandsInfo, 
            IConnectionHandler connectionHandler, IAudioService audioService, IValidationHandler validationHandler)
        {
            _config = config;
            _messageWriter = messageWriter;
            _commandsInfo = commandsInfo;
            _connectionHandler = connectionHandler;
            _audioService = audioService;
            _validationHandler = validationHandler;
        }

        public ICommand BuildCommand(string command, IUser author, IMessageChannel channel, IGuild guild, params string[] parameters)
        {
            ICommand _command = null;
            switch(command)
            {
                case CommandNames.exit:
                    _command = new ExitCommand(author, _config, channel, _messageWriter, _validationHandler, parameters);
                    break;
                case CommandNames.update:
                    _command = new UpdateCommand(author, _config, channel, _messageWriter, _validationHandler, parameters);
                    break;
                case CommandNames.restart:
                    _command = new RestartCommand(author, _config, channel, _messageWriter, _validationHandler, parameters);
                    break;
                case CommandNames.help:
                    _command = new HelpCommand(author, _config, channel, _messageWriter, _commandsInfo, _validationHandler, parameters);
                    break;
                case CommandNames.version:
                    _command = new VersionCommand(author, _config, channel, _messageWriter, _validationHandler, parameters);
                    break;
                case CommandNames.random:
                    _command = new RandomCommand(author, _config, channel, _messageWriter, _validationHandler, parameters);
                    break;
                case CommandNames.github:
                    _command = new GithubCommand(author, _config, channel, _messageWriter, _validationHandler, parameters);
                    break;
                case CommandNames.uptime:
                    _command = new UptimeCommand(author, _config, channel, _messageWriter, _validationHandler, parameters);
                    break;
                case CommandNames.color:
                    _command = new ColorCommand(author, _config, channel, _messageWriter, _validationHandler, parameters);
                    break;
                case CommandNames.flipcoin:
                    _command = new FlipcoinCommand(author, _config, channel, _messageWriter, _validationHandler, parameters);
                    break;
                case CommandNames.roll:
                    _command = new RollCommand(author, _config, channel, _messageWriter, _validationHandler, parameters);
                    break;
                case CommandNames.purge:
                    _command = new PurgeCommand(author, _config, channel, _messageWriter, _connectionHandler, _validationHandler, parameters);
                    break;
                case CommandNames.summon:
                    _command = new SummonCommand(author, _config, channel, _messageWriter, _audioService, guild, _validationHandler, parameters);
                    break;
                case CommandNames.dismiss:
                    _command = new DismissCommand(author, _config, channel, _messageWriter, _audioService, guild, _validationHandler, parameters);
                    break;
                default:
                    break;
            }
            return _command;
        }
    }
}
