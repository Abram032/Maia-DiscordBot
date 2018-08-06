using Maia.Core.Commands;
using Maia.Core.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Core.Validation
{
    public abstract class BaseCommandValidationContext : ICommandValidationContext
    {
        protected IConfiguration _config;

        public BaseCommandValidationContext(IConfiguration config)
        {
            _config = config;
        }

        public abstract IEnumerable<ValidationResult> Validate(BaseCommand command);
    }
}
