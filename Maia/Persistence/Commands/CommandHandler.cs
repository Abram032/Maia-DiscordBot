using Discord;
using Discord.WebSocket;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Commands
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
                await command.ExecuteAsync();
            else
                await _messageWriter.Send("Unknown command.", message.Author, message.Channel);
            await Task.CompletedTask;
        }

        private ICommand CreateCommand(IMessage message)
        {
            string _message = message.Content;
            IUser author = message.Author;
            IMessageChannel channel = message.Channel;
            var _channel = message.Channel as SocketGuildChannel;
            IGuild guild = _channel.Guild;
            _message = RemovePrefix(_message);
            _message = ToLowercase(_message);
            string command = ExtractCommand(_message);
            string[] parameters = GetParameters(_message);
            return _commandBuilder.BuildCommand(command, author, channel, guild, parameters); 
        }

        private string[] GetParameters(string message)
        {
            return message.Split(' ').ToList().Skip(1).ToArray();
        }

        private string ExtractCommand(string message)
        {
            return message.Split(' ').GetValue(0).ToString();
        }

        private string ToLowercase(string message)
        {
            return message.ToLower();
        }

        private string RemovePrefix(string message)
        {
            return message.Replace(_config.GetValue(ConfigKeys.CommandPrefix), string.Empty).Trim();
        }
    }
}
