﻿using Maia.Core.Commands;
using Maia.Core.Settings;
using Maia.Core.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Persistence.Validation.Validators
{
    class NoParametersValidator : BaseCommandValidator
    {
        public NoParametersValidator(IConfiguration config) : base(config)
        {
        }

        public override ValidationResult Validate(BaseCommand command)
        {
            var result = new ValidationResult();
            if(command.Parameters.Length != 0)
                return result.Failed("Invalid use of command!");
            return result;
        }
    }
}
