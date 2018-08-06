using Maia.Core.Commands;
using Maia.Core.Settings;
using Maia.Core.Validation;
using Maia.Persistence.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Persistence.Validation.Context
{
    class OwnerOnlyNoParametersValidationContext : BaseCommandValidationContext, ICommandValidationContext
    {
        public OwnerOnlyNoParametersValidationContext(IConfiguration config) : base(config)
        {
        }

        public override IEnumerable<ValidationResult> Validate(BaseCommand command)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            OwnerValidator ownerValidator = new OwnerValidator(_config);
            NoParametersValidator parametersValidator = new NoParametersValidator(_config);
            validationResults.Add(ownerValidator.Validate(command));
            validationResults.Add(parametersValidator.Validate(command));
            return validationResults;
        }
    }
}
