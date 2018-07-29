using Discord;
using Maia.Core.Commands;
using Maia.Core.Common;
using Maia.Core.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maia.Persistence.Commands
{
    class RandomCommand : BaseCommand, ICommand
    {
        Random random = new Random();

        public RandomCommand(IUser author, IConfiguration config, IMessageChannel channel, IMessageWriter messageWriter, params string[] parameters) 
            : base(author, config, channel, messageWriter, parameters)
        {
        }

        public async override Task ExecuteAsync()
        {
            if(CanExecute())
            {
                if(_parameters.Length == 0)
                    await _messageWriter.Send(Generate().ToString(), _author, _channel);
                if(_parameters.Length == 1)
                    await _messageWriter.Send(Generate(0, int.Parse(_parameters[0])).ToString(), _author, _channel);
                else
                    await _messageWriter.Send(Generate(int.Parse(_parameters[0]), int.Parse(_parameters[1])).ToString(), _author, _channel);
            }
            else
                await _messageWriter.Send("Invalid use of command!", _author, _channel);
        }

        public override bool ValidateParameters()
        {
            if(_parameters.Length > 2)
                return false;
            foreach(string param in _parameters)
            {
                bool result = int.TryParse(param, out int _result);
                if(result == false)
                    return false;
            }
            if(_parameters.Length == 2)
            {
                int a = int.Parse(_parameters[0]);
                int b = int.Parse(_parameters[1]);
                if(a > b)
                    return false;
            }
            return true;
        }

        public override bool ValidateAuthor()
        {
            return true;
        }

        private int Generate(int min = 0, int max = 100) => random.Next(min, max);
    }
}
