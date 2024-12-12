namespace DevExpress.Xpf.Core.DataSources
{
    internal class EntityFrameworkStrategySelector : BaseDataSourceStrategySelector
    {
        public override DataSourceStrategyBase SelectStrategy(IDataSource dataSource, DataSourceStrategyBase currentStrategy) => 
            ((dataSource == null) || (dataSource.ContextType == null)) ? currentStrategy : ((dataSource.ContextType.BaseType.FullName != "System.Data.Entity.DbContext") ? new GenericPropertyDataSourceStrategy(dataSource) : new EF5_DataSourceStrategy(dataSource));
    }
}

