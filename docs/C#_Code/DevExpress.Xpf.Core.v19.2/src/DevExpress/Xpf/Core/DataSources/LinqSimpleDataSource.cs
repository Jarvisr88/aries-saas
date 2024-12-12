namespace DevExpress.Xpf.Core.DataSources
{
    public class LinqSimpleDataSource : EnumerableDataSourceBase
    {
        protected override DataSourceStrategyBase CreateDataSourceStrategy() => 
            new GenericPropertyDataSourceStrategy(this);
    }
}

