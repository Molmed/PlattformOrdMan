using System;
using System.Collections.Generic;
using System.Text;

namespace PlattformOrdMan.Data
{
    public class DemandAnswerField
    {
        private readonly bool _hasAnswered;
        private readonly bool _hasValue;
        public readonly string MyValue;

        public DemandAnswerField(bool hasAnswered, bool hasValue, string value)
        {
            _hasAnswered = hasAnswered;
            _hasValue = hasValue;
            MyValue = value;
        }
    }
}
