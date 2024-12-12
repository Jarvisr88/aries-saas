namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Collections.Generic;

    public class QueryBooleanChoiceDataEventArgs : QueryDataEventArgs<BooleanChoiceData>
    {
        internal QueryBooleanChoiceDataEventArgs(string propertyPath, IDictionary<string, object> memberValues) : base(propertyPath, memberValues)
        {
        }

        protected override BooleanChoiceData CreateData(IDictionary<string, object> memberValues) => 
            new BooleanChoiceData(memberValues);
    }
}

