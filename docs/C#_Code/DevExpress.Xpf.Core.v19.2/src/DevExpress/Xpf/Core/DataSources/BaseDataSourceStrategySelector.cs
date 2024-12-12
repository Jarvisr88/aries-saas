namespace DevExpress.Xpf.Core.DataSources
{
    using System;

    public class BaseDataSourceStrategySelector
    {
        public virtual DataSourceStrategyBase SelectStrategy(IDataSource dataSource, DataSourceStrategyBase currentStrategy) => 
            currentStrategy;
    }
}

