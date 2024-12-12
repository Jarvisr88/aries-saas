namespace DevExpress.Xpo.DB.Helpers
{
    using System;
    using System.Collections.Generic;

    public sealed class QueryCollection : List<Query>
    {
        public QueryCollection()
        {
        }

        public QueryCollection(params Query[] queries)
        {
            base.AddRange(queries);
        }
    }
}

