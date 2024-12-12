namespace DevExpress.Xpo.DB
{
    using System;

    public class DataCacheNodeLocal : DataCacheNode
    {
        public DataCacheNodeLocal(ICacheToCacheCommunicationCore parentCache) : base(parentCache)
        {
        }

        protected override bool IsGoodForCache(SelectStatement stmt) => 
            base.IsGoodForCache(stmt) && IsProbablyGroupByStatement(stmt);

        public static bool IsProbablyGroupByStatement(SelectStatement statement) => 
            (statement.GroupProperties.Count <= 0) ? ((statement.Operands.Count > 0) && (statement.Operands[0] is QuerySubQueryContainer)) : true;
    }
}

