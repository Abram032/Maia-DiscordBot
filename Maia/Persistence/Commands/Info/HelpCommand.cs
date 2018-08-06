using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Core.Validation;
using Maia.Persistence.Validation.Context;
using Maia.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Commands.Info
{
    class HelpCommand : BaseCommand, ICommand
    {
        private ICommandsInfo _commandsInfo;

        public HelpCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, ICommandsInfo commandsInfo, IValidationHandler validationHandler, params string[] parameters) 
            : base(author, config, channel, messageWriter, validationHandler, parameters)
        {
            _commandsInfo = commandsInfo;
        }

        public override bool CanExecute()
        {
            ICommandValidationContext context = new HelpCommandValidationContext(_config);
            var result = _validationHandler.Validate(context, this);
            return result.IsSuccessful;
        }

        public override async Task ExecuteAsync()
        {
            if(CanExecute())
            {
                if(Parameters.Length == 0)
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
                    await _messageWriter.Send(sb.ToString(), Author, Channel);
                }
                else
                {
                    string message = _commandsInfo.GetCommandHelp(Parameters[0]);
                    if(message == string.Empty)
                        message = "Unknown command!";
                    await _messageWriter.Send(message, Author, Channel);
                }
            }
            else
                await _messageWriter.Send("Invalid use of command!", Author, Channel);
        }
    }
}
