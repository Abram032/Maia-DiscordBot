using Maia.Core.Commands;
using Maia.Core.Settings;
using Maia.Core.Validation;
using Maia.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Persistence.Validation.Validators
{
    class OwnerValidator : BaseCommandValidator
    {
        public OwnerValidator(IConfiguration config) : base(config)
        {
        }

        public override ValidationResult Validate(BaseCommand command)
        {
            var result = new ValidationResult();
            if(GetOwnerId().Equals(command.Author.Id) == false)
                return result.Failed("Not authorized!");
            return result;
        }

        private ulong GetOwnerId()
        {
            string owner = _config.GetValueOrDefault(ConfigKeys.OwnerID);
            if (ulong.TryParse(owner, out ulong ownerID) == false)
                return default;
            return ownerID;
        }
    }
}
