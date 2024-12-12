namespace DevExpress.Xpf.Core.DataSources
{
    using System;

    public class WcfDataSourceStrategyStandart : WcfDataSourceStrategyBase
    {
        public WcfDataSourceStrategyStandart(IWcfDataSource owner) : base(owner)
        {
        }

        private IWcfStandartDataSource Owner =>
            (IWcfStandartDataSource) base.owner;
    }
}

