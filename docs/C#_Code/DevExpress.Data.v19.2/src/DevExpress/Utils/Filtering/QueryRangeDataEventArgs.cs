namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Collections.Generic;

    public class QueryRangeDataEventArgs : QueryDataEventArgs<RangeData>
    {
        internal QueryRangeDataEventArgs(string propertyPath, IDictionary<string, object> memberValues) : base(propertyPath, memberValues)
        {
        }

        protected override RangeData CreateData(IDictionary<string, object> memberValues) => 
            new RangeData(memberValues);
    }
}

