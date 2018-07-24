using Maia.Core.Common;
using Maia.Core.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia
{
    class Bot
    {
        IConfiguration _config;
        IConnectionHandler _connectionManager;
        IMessageHandler _messageHandler;

        public Bot(IConnectionHandler connectionManager, IConfiguration config, IMessageHandler messageHandler)
        {
            _connectionManager = connectionManager;
            _config = config;
            _messageHandler = messageHandler;
        }

        public async Task MainAsync()
        {
            await _config.LoadConfig();
            await _connectionManager.Connect();
            await _messageHandler.Handle();
            await Task.Delay(-1);
        }
    }
}
