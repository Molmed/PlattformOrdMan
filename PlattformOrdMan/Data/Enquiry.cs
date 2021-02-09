using System;
using System.Collections.Generic;
using System.Text;

namespace PlattformOrdMan.Data
{
    public class Enquiry
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
        }

        public bool HasAnswered => _hasAnswered;

        public bool HasValue => _hasValue;

        public string Value => _value;
    }
}
