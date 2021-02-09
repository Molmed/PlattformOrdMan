using System;
using System.Collections.Generic;
using System.Text;
using Molmed.PlattformOrdMan;
using PlattformOrdMan.Data.Exception;

namespace PlattformOrdMan.Data
{
    public class Enquiry : PlattformOrdManBase
    {
        /// <summary>
        /// Enquiry contain a value together with a flag telling if 
        /// an empty field was intentionally or not.
        /// </summary>
        private readonly bool _hasAnswered;
        private readonly bool _hasValue;
        private readonly string _value;

        public Enquiry(bool hasAnswered, bool hasValue, string value)
        {
            _hasAnswered = hasAnswered;
            _hasValue = hasValue;
            _value = value;
            if (HasIllegalCombination())
                throw new ValidationException("There is an illegal combination in Enquiry");
        }

        private bool HasIllegalCombination()
        {
            return (!_hasAnswered && _hasValue) ||
                   (!_hasAnswered && !string.IsNullOrEmpty(_value)) ||
                   (!_hasValue && !string.IsNullOrEmpty(_value));

        }

        protected override bool OverrideEquals(object other)
        {
            var otherr = (Enquiry) other;
            return _hasAnswered == otherr.HasAnswered && _hasValue == otherr.HasValue && _value == otherr.Value;
        }

        public bool HasAnswered => _hasAnswered;

        public bool HasValue => _hasValue;

        public string Value => _value;
    }
}
