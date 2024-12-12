namespace DevExpress.ReportServer.ServiceModel.Client
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.Data.XtraReports.DataProviders;
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.ReportServer.ServiceModel.DataContracts;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.ServiceModel;
    using System.Threading.Tasks;

    public interface IReportServerClient : IReportServiceClient, IServiceClientBase
    {
        event EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<ColumnInfo>>> GetColumnsCompleted;

        event EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<TableInfo>>> GetDataMembersCompleted;

        event EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<DataSourceInfo>>> GetDataSourcesCompleted;

        void CloneReport(int sourceReportId, ReportDto reportDto, object asyncState, Action<ScalarOperationCompletedEventArgs<ReportDto>> onCompleted);
        Task<int> CreateCategoryAsync(string name, object asyncState);
        void CreateReport(ReportDto message, object asyncState, Action<ScalarOperationCompletedEventArgs<CreateReportResult>> onCompleted);
        Task<CreateReportResult> CreateReportAsync(ReportDto message, object asyncState);
        void CreateReportCategory(string categoryName, object asyncState, Action<ScalarOperationCompletedEventArgs<int>> onCompleted);
        Task<int> CreateScheduledJobAsync(ScheduledJobDto scheduledJob, object asyncState);
        Task DeleteCategoryAsync(int id, object asyncState);
        Task DeleteDataModelAsync(int id, object asyncState);
        void DeleteReport(int reportId, object asyncState, Action<AsyncCompletedEventArgs> onCompleted);
        void DeleteReportCategory(int categoryId, object asyncState, Action<AsyncCompletedEventArgs> onCompleted);
        Task DeleteScheduledJobAsync(int id, object asyncState);
        Task<int?> ExecuteJobAsync(int scheduledJobId, int? scheduledJobResult, object asyncState);
        void GetCategories(object asyncState, Action<ScalarOperationCompletedEventArgs<IEnumerable<CategoryDto>>> onCompleted);
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync(object asyncState);
        Task<IEnumerable<ColumnInfo>> GetColumnsAsync(string dataSourceName, TableInfo dataMemberName);
        void GetColumnsAsync(string dataSourceName, TableInfo dataMemberName, object asyncState);
        Task<IEnumerable<TableInfo>> GetDataMembersAsync(string dataSourceName);
        void GetDataMembersAsync(string dataSourceName, object asyncState);
        Task<DataModelDto> GetDataModelAsync(int id, object asyncState);
        Task<IEnumerable<DataModelDto>> GetDataModelsAsync(object asyncState);
        Task<IEnumerable<DataSourceInfo>> GetDataSourcesAsync();
        void GetDataSourcesAsync(object asyncState);
        string GetDataSourceSchema(int dataSourceId, object asyncState);
        ReportCatalogItemDto GetReportCatalogItemDto(int reportId);
        void GetReportRevisions(int reportId, object asyncState, Action<ScalarOperationCompletedEventArgs<IEnumerable<LayoutRevisionDto>>> onCompleted);
        void GetReports(object asyncState, Action<ScalarOperationCompletedEventArgs<IEnumerable<ReportCatalogItemDto>>> onCompleted);
        Task<IEnumerable<ReportCatalogItemDto>> GetReportsAsync(object asyncState);
        Task<ScheduledJobDto> GetScheduledJobAsync(int id, object asyncState);
        Task<IEnumerable<ScheduledJobLogDto>> GetScheduledJobLogsAsync(int scheduledJobId, DataPagination pagination, object asyncState);
        Task<int> GetScheduledJobLogsCountAsync(int scheduledJobId, object asyncState);
        Task<ScheduledJobResultDto> GetScheduledJobResultAsync(int id, object asyncState);
        Task<IEnumerable<ScheduledJobResultCatalogItemDto>> GetScheduledJobResultsAsync(int scheduledJobLogId, DataPagination pagination, object asyncState);
        Task<int> GetScheduledJobResultsCountAsync(int scheduledJobLogId, object asyncState);
        Task<IEnumerable<ScheduledJobCatalogItemDto>> GetScheduledJobsAsync(object asyncState);
        void LoadReport(int id, object asyncState, Action<ScalarOperationCompletedEventArgs<ReportDto>> onCompleted);
        Task<ReportDto> LoadReportAsync(int id, object asyncState);
        void LoadReportLayoutByRevisionId(int reportId, int revisionId, object asyncState, Action<ScalarOperationCompletedEventArgs<byte[]>> onCompleted);
        [Obsolete("This method has become obsolete.")]
        void LockReport(int id);
        void Ping(Action<AsyncCompletedEventArgs> onCompleted, object asyncState);
        void RollbackReportLayout(int reportId, int revisionId, object asyncState, Action<AsyncCompletedEventArgs> onCompleted);
        void SaveReportById(int reportId, ReportDto reportDto, object asyncState, Action<ScalarOperationCompletedEventArgs<int>> onCompleted);
        [Obsolete("This method has become obsolete.")]
        void UnlockReport(int id);
        Task UpdateCategoryAsync(int id, string name, int? optimisticLock, object asyncState);
        Task UpdateDataModelAsync(DataModelDto dataModel, object asyncState);
        void UpdateReport(int reportId, ReportDto reportDto, object asyncState, Action<ScalarOperationCompletedEventArgs<int>> onCompleted);
        Task<int> UpdateReportAsync(int reportId, ReportDto reportDto, object asyncState);
        void UpdateReportCategory(int categoryId, string categoryName, int? optimisticLock, object asyncState, Action<AsyncCompletedEventArgs> onCompleted);
        Task UpdateScheduledJobAsync(ScheduledJobDto scheduledJob, object asyncState);
        void UploadLayout(Stream layout, object asyncState, Action<ScalarOperationCompletedEventArgs<TransientReportId>> onCompleted);

        IContextChannel ContextChannel { get; }
    }
}

