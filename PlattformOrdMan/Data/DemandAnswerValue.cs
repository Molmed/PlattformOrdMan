using System;
using System.Collections.Generic;
using System.Text;

namespace PlattformOrdMan.Data
{
    public class DemandAnswerValue
    {
        private readonly bool _hasAnswered;
        private readonly bool _hasValue;
        private readonly string _value;

        public DemandAnswerValue(bool hasAnswered, bool hasValue, string value)
        {
            _hasAnswered = hasAnswered;
            _hasValue = hasValue;
            _value = value;
        }
    }
}
