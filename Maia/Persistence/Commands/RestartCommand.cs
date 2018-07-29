using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Commands
{
    class RestartCommand : BaseCommand, ICommand
    {
        public RestartCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, params string[] parameters) 
            : base(author, config, channel, messageWriter, parameters)
        {
        }

        public async override Task ExecuteAsync()
        {
            if (CanExecute())
            {
                await _messageWriter.Send("Brb! Gonna bring new stuff!", _author, _channel);
                var dir = Environment.CurrentDirectory + @"/";
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    Process.Start(dir + "Run.bat", "-n");
                else
                    Process.Start(dir + "Run.sh", "-n");
                Environment.Exit(0);
            }
            else
                await _messageWriter.Send("Invalid use of command or not authorized!", _author, _channel);
        }
    }
}
