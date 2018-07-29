using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Commands
{
    class HelpCommand : BaseCommand, ICommand
    {
        private ICommandsInfo _commandsInfo;

        public HelpCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, ICommandsInfo commandsInfo, params string[] parameters) 
            : base(author, config, channel, messageWriter, parameters)
        {
            _commandsInfo = commandsInfo;
        }

        public override async Task ExecuteAsync()
        {
            if(CanExecute())
            {
                if(_parameters.Length == 0)
                {
                    List<string> commands = _commandsInfo.GetCommands();
                    commands.Sort();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Available commands: \n");
                    foreach(var command in commands)
                    {
                        sb.Append(command);
                        sb.Append(" | ");
                    }
                    await _messageWriter.Send(sb.ToString(), _author, _channel);
                }
                else
                {
                    string message = _commandsInfo.GetCommandHelp(_parameters[0]);
                    if(message == string.Empty)
                        message = "Unknown command!";
                    await _messageWriter.Send(message, _author, _channel);
                }
            }
            else
                await _messageWriter.Send("Invalid use of command!", _author, _channel);
        }

        public override bool ValidateAuthor()
        {
            return true;
        }

        public override bool ValidateParameters()
        {
            if(_parameters.Length < 2)
                return true;
            else
                return false;
        }
    }
}
