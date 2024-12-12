namespace DevExpress.Xpf.Core.DataSources
{
    public class EntityPLinqServerModeDataSource : PLinqServerModeDataSourceBase
    {
        protected override DataSourceStrategyBase CreateDataSourceStrategy() => 
            new GenericPropertyDataSourceStrategy(this);

        protected override BaseDataSourceStrategySelector CreateDataSourceStrategySelector() => 
            new EntityFrameworkStrategySelector();
    }
}

