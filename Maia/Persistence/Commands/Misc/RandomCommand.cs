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
    class RandomCommand : BaseCommand, ICommand
    {
        Random random = new Random();

        public RandomCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, IValidationHandler validationHandler, params string[] parameters) 
            : base(author, config, channel, messageWriter, validationHandler, parameters)
        {
        }

        public override bool CanExecute()
        {
            ICommandValidationContext context = new RandomCommandValidationContext(_config);
            var result = _validationHandler.Validate(context, this);
            return result.IsSuccessful;
        }

        public async override Task ExecuteAsync()
        {
            if(CanExecute())
            {
                if(Parameters.Length == 0)
                    await _messageWriter.Send(Generate().ToString(), Author, Channel);
                if(Parameters.Length == 1)
                    await _messageWriter.Send(Generate(0, int.Parse(Parameters[0])).ToString(), Author, Channel);
                else
                    await _messageWriter.Send(Generate(int.Parse(Parameters[0]), int.Parse(Parameters[1])).ToString(), Author, Channel);
            }
            else
                await _messageWriter.Send("Invalid use of command!", Author, Channel);
        }

        private int Generate(int min = 0, int max = 100) => random.Next(min, max);
    }
}
