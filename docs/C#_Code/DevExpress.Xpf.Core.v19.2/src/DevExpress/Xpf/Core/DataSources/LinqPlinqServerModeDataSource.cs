namespace DevExpress.Xpf.Core.DataSources
{
    public class LinqPlinqServerModeDataSource : PLinqServerModeDataSourceBase
    {
        protected override DataSourceStrategyBase CreateDataSourceStrategy() => 
            new GenericPropertyDataSourceStrategy(this);
    }
}

