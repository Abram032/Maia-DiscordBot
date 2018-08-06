using Maia.Core.Commands;
using Maia.Core.Settings;
using Maia.Core.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Persistence.Validation.Validators
{
    class PurgeCommandParametersValidator : BaseCommandValidator
    {
        public PurgeCommandParametersValidator(IConfiguration config) : base(config)
        {
        }

        public override ValidationResult Validate(BaseCommand command)
        {
            var result = new ValidationResult();
            if(command.Parameters.Length == 0)
                return result;
            if(command.Parameters.Length > 1)
                return result.Failed("Invalid use of command!");             
            if(int.TryParse(command.Parameters[0], out int value) == false)
                return result.Failed("Invalid use of command!");
            if(value > 100 || value < 0)
                return result.Failed("Invalid use of command!");
            return result;
        }
    }
}
