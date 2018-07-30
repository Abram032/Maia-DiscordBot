using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Commands
{
    class RollCommand : BaseCommand, ICommand
    {
        Random random = new Random();

        public RollCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, params string[] parameters)
            : base(author, config, channel, messageWriter, parameters)
        {
        }

        public override async Task ExecuteAsync()
         {
            if (CanExecute())
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("You rolled: ");
                int size = int.Parse(_parameters[0].Remove(0, 1));
                int amount = 1;
                if(_parameters.Length == 2)
                    amount = int.Parse(_parameters[1]);
                if (_parameters[0].Equals("d00"))
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
                await Reply(sb.ToString());
            }
            else
                await InvalidUseOfCommand();
        }

        public override bool ValidateAuthor()
        {
            return true;
        }

        public override bool ValidateParameters()
        {
            if (_parameters.Length > 2)
                return false;
            if (_parameters.Length == 0)
                return false;
            if (_parameters.Length <= 2)
            {
                if (_parameters[0].StartsWith('d') == false)
                    return false;
                string size = _parameters[0].Remove(0, 1);
                if (int.TryParse(size, out int _size) == false)
                    return false;
                if(_size == 0)
                    if(_parameters[0].Equals("d00") == false)
                        return false;
            }
            if (_parameters.Length == 2)
            {
                string amount = _parameters[1];
                if (int.TryParse(amount, out int _amount) == false)
                    return false;
                if (_amount > 10 || _amount < 1)
                    return false;
            }
            return true;
        }

        public int Generate(int size) => random.Next(1, size + 1);
    }
}
