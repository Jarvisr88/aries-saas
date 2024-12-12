namespace DevExpress.Xpf.Core.DataSources
{
    public class LinqCollectionViewDataSource : CollectionViewDataSourceBase
    {
        protected override DataSourceStrategyBase CreateDataSourceStrategy() => 
            new GenericPropertyDataSourceStrategy(this);
    }
}

