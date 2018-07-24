using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Common
{
    class CommandHandler : ICommandHandler
    {
        IConfiguration _config;
        ICommandBuilder _commandBuilder;
        IMessageWriter _messageWriter;

        public CommandHandler(IConfiguration config, ICommandBuilder commandBuilder, IMessageWriter messageWriter)
        {
            _config = config;
            _commandBuilder = commandBuilder;
            _messageWriter = messageWriter;
        }

        public async Task Handle(IMessage message)
        {
            ICommand command = CreateCommand(message);
            if(command != null)
                await command.Execute();
            else
                await _messageWriter.Send("Unknown command.", message.Author, message.Channel);
            await Task.CompletedTask;
        }

        private ICommand CreateCommand(IMessage message)
        {
            string command = message.Content;
            IUser author = message.Author;
            IMessageChannel channel = message.Channel;
            command = RemovePrefix(command);
            command = ToLowercase(command);
            List<string> parameters = GetParameters(command);
            command = ExtractCommand(command);
            return _commandBuilder.BuildCommand(command, parameters, author, channel); 
        }

        private List<string> GetParameters(string command)
        {
            List<string> parameters = command.Split(' ').ToList();
            parameters.RemoveAt(0);
            return parameters;
        }

        private string ExtractCommand(string command)
        {
            return command.Split(' ').GetValue(0).ToString();
        }

        private string ToLowercase(string command)
        {
            return command.ToLower();
        }

        private string RemovePrefix(string command)
        {
            return command.Replace(_config.GetValue(ConfigKeys.CommandPrefix), string.Empty).Trim();
        }
    }
}
