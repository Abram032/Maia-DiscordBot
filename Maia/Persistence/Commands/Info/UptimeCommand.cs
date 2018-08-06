using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Core.Validation;
using Maia.Persistence.Validation.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Commands.Info
{
    class UptimeCommand : BaseCommand, ICommand
    {
        public UptimeCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, IValidationHandler validationHandler, params string[] parameters)
            : base(author, config, channel, messageWriter, validationHandler, parameters)
        {
        }

        public override bool CanExecute()
        {
            ICommandValidationContext context = new NoParametersValidationContext(_config);
            var result = _validationHandler.Validate(context, this);
            return result.IsSuccessful;
        }

        public override async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                TimeSpan timeSpan = Program.timer.Elapsed;
                string message = BuildMessage(timeSpan);
                await _messageWriter.Send(message, Author, Channel);
            }
            else
                await _messageWriter.Send("Invalid use of command!", Author, Channel);
        }

        private string BuildMessage(TimeSpan time)
        {
            StringBuilder sb = new StringBuilder();
            string temp = string.Empty;
            sb.Append("Bot has been running for ");
            if (time.Days > 0)
            {
                sb.Append(time.Days);
                temp = (time.Days == 1) ? " day " : " days ";
                sb.Append(temp);
            }
            if (time.Hours > 0)
            {
                //sb.Append(", ");
                sb.Append(time.Hours);
                temp = (time.Hours == 1) ? " hour " : " hours ";
                sb.Append(temp);
            }
            if(time.Minutes > 0)
            {
                //sb.Append(", ");
                sb.Append(time.Minutes);
                temp = (time.Minutes == 1) ? " minute " : " minutes ";
                sb.Append(temp);
            }
            if(time.Seconds > 0)
            {
                //sb.Append(", ");
                sb.Append(time.Seconds);
                temp = (time.Seconds == 1) ? " second " : " seconds ";
                sb.Append(temp);
            }
            return sb.ToString();
        }
    }
}
