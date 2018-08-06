using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Core.Validation;
using Maia.Persistence.Validation.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Commands.Misc
{
    class FlipcoinCommand : BaseCommand, ICommand
    {
        Random random = new Random();

        public FlipcoinCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, IValidationHandler validationHandler, params string[] parameters) 
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
            if(CanExecute())
            {
                string message = Generate() == 0 ? "Heads!" : "Tails!";
                await _messageWriter.Send(message, Author, Channel);
            }
            else
                await _messageWriter.Send("Invalid use of command!", Author, Channel);
        }

        private int Generate() => random.Next(0,2);
    }
}
