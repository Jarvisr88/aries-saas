namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Collections.Generic;

    public class QueryUniqueValuesEnumChoiceDataEventArgs : QueryEnumChoiceDataEventArgs
    {
        internal QueryUniqueValuesEnumChoiceDataEventArgs(string propertyPath, IDictionary<string, object> memberValues) : base(propertyPath, memberValues)
        {
        }

        protected sealed override EnumChoiceData CreateData(IDictionary<string, object> memberValues) => 
            new UniqueValuesEnumChoiceData(memberValues);
    }
}

