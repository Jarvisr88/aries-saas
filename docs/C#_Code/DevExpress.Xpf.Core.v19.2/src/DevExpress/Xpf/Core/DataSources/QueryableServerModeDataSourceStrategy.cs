namespace DevExpress.Xpf.Core.DataSources
{
    using System;
    using System.Linq;

    internal class QueryableServerModeDataSourceStrategy : GenericPropertyDataSourceStrategy
    {
        public QueryableServerModeDataSourceStrategy(IQueryableServerModeDataSource owner) : base(owner)
        {
        }

        public override object CreateData(object value)
        {
            this.Owner.QueryableSource = value as IQueryable;
            return this.Owner.Data;
        }

        private IQueryableServerModeDataSource Owner =>
            (IQueryableServerModeDataSource) base.owner;
    }
}

