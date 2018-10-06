using Discord;
using Discord.WebSocket;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Common
{
    class MessageHandler : IMessageHandler
    {
        private IConfiguration _config;
        private IConnectionHandler _connectionHandler;
        private ICommandHandler _commandHandler;
        private DiscordSocketClient Client { get; set; }
        private string CommandPrefix { get; set; }

        public MessageHandler(IConfiguration config, IConnectionHandler connectionHandler, ICommandHandler commandHandler)
        {
            _config = config;
            _connectionHandler = connectionHandler;
            _commandHandler = commandHandler;
        }

        public async Task Handle()
        {
            if(Client == null)
                Client = _connectionHandler.Client;
            if(CommandPrefix == null)
                CommandPrefix = _config.GetValue(ConfigKeys.CommandPrefix);
            Client.MessageReceived += MessageRecieved;
            await Task.CompletedTask;
        }

        private async Task MessageRecieved(IMessage message)
        {
            if(message.Content.StartsWith(CommandPrefix) && message.Content.Length >= 2)
            {
                Task.Run(() => _commandHandler.Handle(message));
            }
            await Task.CompletedTask;
        }
    }
}
