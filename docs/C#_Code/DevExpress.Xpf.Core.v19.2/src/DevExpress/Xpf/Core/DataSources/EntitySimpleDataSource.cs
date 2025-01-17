﻿namespace DevExpress.Xpf.Core.DataSources
{
    public class EntitySimpleDataSource : SimpleDataSource
    {
        protected override DataSourceStrategyBase CreateDataSourceStrategy() => 
            new GenericPropertyDataSourceStrategy(this);

        protected override BaseDataSourceStrategySelector CreateDataSourceStrategySelector() => 
            new EntityFrameworkStrategySelector();
    }
}

