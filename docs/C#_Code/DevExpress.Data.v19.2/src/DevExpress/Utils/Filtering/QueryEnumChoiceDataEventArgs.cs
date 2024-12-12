namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Collections.Generic;

    public class QueryEnumChoiceDataEventArgs : QueryDataEventArgs<EnumChoiceData>
    {
        internal QueryEnumChoiceDataEventArgs(string propertyPath, IDictionary<string, object> memberValues) : base(propertyPath, memberValues)
        {
        }

        protected override EnumChoiceData CreateData(IDictionary<string, object> memberValues) => 
            new EnumChoiceData(memberValues);
    }
}

