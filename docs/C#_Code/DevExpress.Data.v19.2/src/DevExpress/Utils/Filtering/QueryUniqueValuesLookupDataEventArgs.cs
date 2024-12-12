namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Collections.Generic;

    public class QueryUniqueValuesLookupDataEventArgs : QueryLookupDataEventArgs
    {
        internal QueryUniqueValuesLookupDataEventArgs(string propertyPath, IDictionary<string, object> memberValues) : base(propertyPath, memberValues)
        {
        }

        protected sealed override LookupData CreateData(IDictionary<string, object> memberValues) => 
            new UniqueValuesLookupData(memberValues);
    }
}

