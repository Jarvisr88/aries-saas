namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Collections.Generic;

    public class QueryUniqueValuesBooleanChoiceDataEventArgs : QueryBooleanChoiceDataEventArgs
    {
        internal QueryUniqueValuesBooleanChoiceDataEventArgs(string propertyPath, IDictionary<string, object> memberValues) : base(propertyPath, memberValues)
        {
        }

        protected sealed override BooleanChoiceData CreateData(IDictionary<string, object> memberValues) => 
            new UniqueValuesBooleanChoiceData(memberValues);
    }
}

