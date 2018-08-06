using Maia.Core.Commands;
using Maia.Core.Settings;
using Maia.Core.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Persistence.Validation.Validators
{
    class RandomCommandParametersValidator : BaseCommandValidator
    {
        public RandomCommandParametersValidator(IConfiguration config) : base(config)
        {
        }

        public override ValidationResult Validate(BaseCommand command)
        {
            ValidationResult validationResult = new ValidationResult();
            if(command.Parameters.Length > 2)
                return validationResult.Failed("Invalid use of command!");
            foreach(string param in command.Parameters)
            {
                bool result = int.TryParse(param, out int _result);
                if(result == false)
                    return validationResult.Failed("Invalid use of command!");
            }
            if(command.Parameters.Length == 2)
            {
                int a = int.Parse(command.Parameters[0]);
                int b = int.Parse(command.Parameters[1]);
                if(a > b)
                    return validationResult.Failed("Invalid use of command!");
            }
            return validationResult;
        }
    }
}
