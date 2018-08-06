using System;
using System.Collections.Generic;
using System.Text;

namespace Maia.Core.Validation
{
    public class ValidationResult
    {
        public bool IsSuccessful { get; private set; }
        public string ErrorMessage { get; private set; }
        private List<string> _memberNames;

        public IEnumerable<string> MemberNames
        {
            get
            {
                return _memberNames;
            }
        }

        public ValidationResult()
        {
            IsSuccessful = true;
        }

        public ValidationResult Failed(string errorMessage, List<string> memberNames = null)
        {
            IsSuccessful = false;
            ErrorMessage = errorMessage;
            if(memberNames != null)
                _memberNames = memberNames;
            return this;
        }
    }
}
