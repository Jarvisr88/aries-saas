namespace DevExpress.Xpf.Core.DataSources
{
    public class EntityCollectionViewSource : CollectionViewDataSourceBase
    {
        protected override DataSourceStrategyBase CreateDataSourceStrategy() => 
            new GenericPropertyDataSourceStrategy(this);

        protected override BaseDataSourceStrategySelector CreateDataSourceStrategySelector() => 
            new EntityFrameworkStrategySelector();
    }
}

