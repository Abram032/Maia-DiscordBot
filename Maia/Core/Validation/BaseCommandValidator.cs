using Maia.Core.Commands;
using Maia.Core.Settings;
using Maia.Core.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Core.Validation
{
    public abstract class BaseCommandValidator
    {
        protected ValidationResult ValidationResult { get; private set; }
        protected IConfiguration _config;

        public BaseCommandValidator(IConfiguration config)
        {
            _config = config;
        }

        public abstract ValidationResult Validate(BaseCommand command);
    }
}
