using Maia.Core.Commands;
using Maia.Core.Settings;
using Maia.Core.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Core.Validation
{
    public interface ICommandValidationContext
    {
        IEnumerable<ValidationResult> Validate(BaseCommand command);
    }
}
