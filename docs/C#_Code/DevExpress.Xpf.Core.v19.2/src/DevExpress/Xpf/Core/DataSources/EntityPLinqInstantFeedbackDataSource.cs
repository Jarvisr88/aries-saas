namespace DevExpress.Xpf.Core.DataSources
{
    public class EntityPLinqInstantFeedbackDataSource : PLinqInstantFeedbackDataSourceBase
    {
        protected override BaseDataSourceStrategySelector CreateDataSourceStrategySelector() => 
            new EntityFrameworkStrategySelector();
    }
}

