namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Collections.Generic;

    public class QueryLookupDataEventArgs : QueryDataEventArgs<LookupData>
    {
        internal QueryLookupDataEventArgs(string propertyPath, IDictionary<string, object> memberValues) : base(propertyPath, memberValues)
        {
        }

        protected override LookupData CreateData(IDictionary<string, object> memberValues) => 
            new LookupData(memberValues);
    }
}

