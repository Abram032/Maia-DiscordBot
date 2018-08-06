using Maia.Core.Commands;
using Maia.Core.Settings;
using Maia.Core.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Persistence.Validation.Validators
{
    class RollCommandParametersValidator : BaseCommandValidator
    {
        public RollCommandParametersValidator(IConfiguration config) : base(config)
        {
        }

        public override ValidationResult Validate(BaseCommand command)
        {
            ValidationResult result = new ValidationResult();
            if (command.Parameters.Length > 2)
                return result.Failed("Invalid use of command!");
            if (command.Parameters.Length == 0)
                return result.Failed("Invalid use of command!");
            if (command.Parameters.Length <= 2)
            {
                if (command.Parameters[0].StartsWith('d') == false)
                    return result.Failed("Invalid use of command!");
                string size = command.Parameters[0].Remove(0, 1);
                if (int.TryParse(size, out int _size) == false)
                    return result.Failed("Invalid use of command!");
                if(_size == 0)
                    if(command.Parameters[0].Equals("d00") == false)
                        return result.Failed("Invalid use of command!");
            }
            if (command.Parameters.Length == 2)
            {
                string amount = command.Parameters[1];
                if (int.TryParse(amount, out int _amount) == false)
                    return result.Failed("Invalid use of command!");
                if (_amount > 10 || _amount < 1)
                    return result.Failed("Invalid use of command!");
            }
            return result;
        }
    }
}
