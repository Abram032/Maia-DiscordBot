using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using Maia.Core.Validation;
using Maia.Persistence.Validation.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Commands.Misc
{
    class RollCommand : BaseCommand, ICommand
    {
        Random random = new Random();

        public RollCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, IValidationHandler validationHandler, params string[] parameters)
            : base(author, config, channel, messageWriter, validationHandler, parameters)
        {
        }

        public override bool CanExecute()
        {
            ICommandValidationContext context = new RollCommandValidationContext(_config);
            var result = _validationHandler.Validate(context, this);
            return result.IsSuccessful;
        }

        public override async Task ExecuteAsync()
         {
            if (CanExecute())
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("You rolled: ");
                int size = int.Parse(Parameters[0].Remove(0, 1));
                int amount = 1;
                if(Parameters.Length == 2)
                    amount = int.Parse(Parameters[1]);
                if (Parameters[0].Equals("d00"))
                {
                    for (int i = 1; i <= amount; i++)
                    {
                        sb.Append(Generate(10));
                        sb.Append("0% ");
                    }
                }
                else
                {
                    for (int i = 1; i <= amount; i++)
                    {
                        sb.Append(Generate(size));
                        sb.Append(" ");
                    }
                }
                await SendMessageAsync(sb.ToString());
            }
            else
                await InvalidUseOfCommand();
        }

        public int Generate(int size) => random.Next(1, size + 1);
    }
}
