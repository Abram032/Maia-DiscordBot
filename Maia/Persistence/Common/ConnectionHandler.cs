using Discord;
using Discord.WebSocket;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Common
{
    class ConnectionHandler : IConnectionHandler
    {
        public DiscordSocketClient Client { get; private set; }
        private string Token { get; set; }
        private IConfiguration _config;
        private ILogHandler _logHandler;

        public ConnectionHandler(IConfiguration config, ILogHandler logHandler)
        {
            _config = config;
            _logHandler = logHandler;
        }

        public async Task Connect()
        {
            Token = GetToken();
            Client = InitClient();
            Client.Log += Log;
            if (ValidateToken())
                await InitConnection();
            else
                Console.WriteLine("Invalid Token!");
            await Task.CompletedTask;
        }

        private string GetToken()
        {
            return _config.GetValue(ConfigKeys.Token);
        }

        private DiscordSocketClient InitClient()
        {
            DiscordSocketConfig discordSocketConfig = InitDiscordSocketConfig();
            return new DiscordSocketClient(discordSocketConfig);
        }

        private DiscordSocketConfig InitDiscordSocketConfig()
        {
            DiscordSocketConfig discordSocketConfig = new DiscordSocketConfig();
            string value = _config.GetValue(ConfigKeys.LogSeverity);
            switch (value)
            {
                case "Critical":
                    discordSocketConfig.LogLevel = LogSeverity.Critical;
                    break;
                case "Debug":
                    discordSocketConfig.LogLevel = LogSeverity.Debug;
                    break;
                case "Error":
                    discordSocketConfig.LogLevel = LogSeverity.Error;
                    break;
                case "Info":
                    discordSocketConfig.LogLevel = LogSeverity.Info;
                    break;
                case "Verbose":
                    discordSocketConfig.LogLevel = LogSeverity.Verbose;
                    break;
                case "Warning":
                    discordSocketConfig.LogLevel = LogSeverity.Warning;
                    break;
                default:
                    discordSocketConfig.LogLevel = LogSeverity.Info;
                    break;
            }
            return discordSocketConfig;
        }

        private async Task InitConnection()
        {
            await Client.LoginAsync(TokenType.Bot, Token);
            await Client.StartAsync();
            await Task.CompletedTask;
        }

        private bool ValidateToken()
        {
            try
            {
                Client.LoginAsync(TokenType.Bot, Token).Wait();
            }
            catch
            {
                return false;
            }
            Client.LogoutAsync().Wait();
            return true;
        }

        private async Task Log(LogMessage log)
        {
            await _logHandler.Handle(log);
            await Task.CompletedTask;
        }

        public async Task Disconnect()
        {           
            await Client.LogoutAsync();
            await Task.CompletedTask;
        }
    }
}
