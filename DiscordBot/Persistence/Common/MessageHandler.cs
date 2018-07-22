﻿using Discord;
using Discord.WebSocket;
using DiscordBot.Core.Commands;
using DiscordBot.Core.Common;
using DiscordBot.Core.Settings;
using DiscordBot.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Persistence.Common
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
                await _commandHandler.Handle(message);
            }
            await Task.CompletedTask;
        }
    }
}