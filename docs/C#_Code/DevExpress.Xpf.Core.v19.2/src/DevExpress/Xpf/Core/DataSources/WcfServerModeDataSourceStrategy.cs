namespace DevExpress.Xpf.Core.DataSources
{
    using System;
    using System.Linq;

    public class WcfServerModeDataSourceStrategy : WcfDataSourceStrategyBase
    {
        public WcfServerModeDataSourceStrategy(IWcfServerModeDataSource owner) : base(owner)
        {
        }

        public override object CreateData(object value)
        {
            this.Owner.DataServiceContext = this.Owner.ContextInstance;
            this.Owner.Query = value as IQueryable;
            return this.Owner.Data;
        }

        private IWcfServerModeDataSource Owner =>
            (IWcfServerModeDataSource) base.owner;
    }
}

