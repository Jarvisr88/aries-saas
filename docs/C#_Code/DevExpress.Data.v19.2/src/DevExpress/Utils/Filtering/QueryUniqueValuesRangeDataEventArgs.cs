namespace DevExpress.Utils.Filtering
{
    using System;
    using System.Collections.Generic;

    public class QueryUniqueValuesRangeDataEventArgs : QueryRangeDataEventArgs
    {
        internal QueryUniqueValuesRangeDataEventArgs(string propertyPath, IDictionary<string, object> memberValues) : base(propertyPath, memberValues)
        {
        }

        protected sealed override RangeData CreateData(IDictionary<string, object> memberValues) => 
            new UniqueValuesRangeData(memberValues);
    }
}

