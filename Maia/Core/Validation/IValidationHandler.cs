using Maia.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Core.Validation
{
    public interface IValidationHandler
    {
        IEnumerable<ValidationResult> ValidationResults { get; }
        ValidationResult Validate(ICommandValidationContext context, BaseCommand command);
    }
}
