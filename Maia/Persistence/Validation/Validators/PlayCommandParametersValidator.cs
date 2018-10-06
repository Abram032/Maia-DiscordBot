using Maia.Core.Commands;
using Maia.Core.Settings;
using Maia.Core.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Persistence.Validation.Validators
{
    class PlayCommandParametersValidator : BaseCommandValidator
    {
        public PlayCommandParametersValidator(IConfiguration config) : base(config)
        {
        }

        public override ValidationResult Validate(BaseCommand command)
        {
            ValidationResult result = new ValidationResult();
            if (command.Parameters.Length == 1)
                return result;
            else
                return result.Failed("Invalid use of command!");
        }
    }
}
