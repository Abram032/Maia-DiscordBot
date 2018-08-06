using Maia.Core.Commands;
using Maia.Core.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Maia.Core.Validation;

namespace Maia.Persistence.Validation
{
    class ValidationHandler : IValidationHandler
    {
        private List<ValidationResult> _validationResults;

        public IEnumerable<ValidationResult> ValidationResults
        {
            get
            {
                return _validationResults;
            }
        }

        public ValidationHandler()
        {
            _validationResults = new List<ValidationResult>();
        }

        private bool CheckResults()
        {
            foreach (var result in ValidationResults)
                if (result.IsSuccessful == false)
                    return false;
            return true;
        }

        private ValidationResult AggregateErrors()
        {
            var validationResult = new ValidationResult();
            StringBuilder sb = new StringBuilder();
            foreach(var result in _validationResults)
            {
                sb.Append(result.ErrorMessage);
                sb.Append(" ");
            }
            validationResult.Failed(sb.ToString());
            return validationResult;
        }

        public ValidationResult Validate(ICommandValidationContext context, BaseCommand command)
        {
            ValidationResult result = new ValidationResult();
            _validationResults = context.Validate(command).ToList();
            if(CheckResults() == false)
                result = AggregateErrors();
            return result;
        }
    }
}
