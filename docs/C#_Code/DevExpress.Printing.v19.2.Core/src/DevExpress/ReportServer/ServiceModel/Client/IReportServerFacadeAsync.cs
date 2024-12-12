namespace DevExpress.ReportServer.ServiceModel.Client
{
    using DevExpress.Data.XtraReports.DataProviders;
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.ReportServer.ServiceModel.DataContracts;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.ServiceModel;

    [ServiceContract(Name="IReportServerFacade"), ServiceKnownType("GetKnownTypes", typeof(ServiceKnownTypeProvider))]
    public interface IReportServerFacadeAsync : IAsyncReportService, IAsyncExportService
    {
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginCloneReport(int sourceReportId, ReportDto reportDto, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginCreateReport(ReportDto message, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginCreateReportCategory(string categoryName, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginCreateScheduledJob(ScheduledJobDto scheduledJob, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginDeleteCategory(int categoryId, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginDeleteDataModel(int id, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginDeleteReport(int reportId, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginDeleteScheduledJob(int id, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginExecuteJob(int scheduledJobId, int? scheduledJobResult, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetCategories(AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetColumns(string dataSourceName, TableInfo dataMemberName, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetDataMembers(string dataSourceName, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetDataModel(int id, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetDataModels(AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetDataSources(AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetReportRevisions(int reportId, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetReports(AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetScheduledJob(int id, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetScheduledJobLogs(int scheduledJobId, DataPagination pagination, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetScheduledJobLogsCount(int scheduledJobId, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetScheduledJobResult(int id, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetScheduledJobResults(int scheduledJobLogId, DataPagination pagination, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetScheduledJobResultsCount(int scheduledJobLogId, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetScheduledJobs(AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true), FaultContract(typeof(ObjectNotFoundFault))]
        IAsyncResult BeginLoadReport(int id, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginLoadReportLayoutByRevisionId(int reportId, int revisionId, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginPing(AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginRollBackReportLayout(int reportId, int revisionId, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginSaveReportById(int reportId, ReportDto reportDto, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginUpdateDataModel(DataModelDto dataModel, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginUpdateReport(int reportId, ReportDto reportDto, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginUpdateReportCategory(int categoryId, string name, int? optimisticLock, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginUpdateScheduledJob(ScheduledJobDto scheduledJob, AsyncCallback callback, object asyncState);
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginUploadLayout(Stream layout, AsyncCallback callback, object asyncState);
        ReportDto EndCloneReport(IAsyncResult ar);
        CreateReportResult EndCreateReport(IAsyncResult ar);
        int EndCreateReportCategory(IAsyncResult ar);
        int EndCreateScheduledJob(IAsyncResult ar);
        void EndDeleteCategory(IAsyncResult ar);
        void EndDeleteDataModel(IAsyncResult ar);
        void EndDeleteReport(IAsyncResult ar);
        void EndDeleteScheduledJob(IAsyncResult ar);
        int? EndExecuteJob(IAsyncResult ar);
        IEnumerable<CategoryDto> EndGetCategories(IAsyncResult ar);
        IEnumerable<ColumnInfo> EndGetColumns(IAsyncResult ar);
        IEnumerable<TableInfo> EndGetDataMembers(IAsyncResult ar);
        DataModelDto EndGetDataModel(IAsyncResult ar);
        IEnumerable<DataModelDto> EndGetDataModels(IAsyncResult ar);
        IEnumerable<StoredDataSourceInfo> EndGetDataSources(IAsyncResult ar);
        IEnumerable<LayoutRevisionDto> EndGetReportRevisions(IAsyncResult ar);
        IEnumerable<ReportCatalogItemDto> EndGetReports(IAsyncResult ar);
        ScheduledJobDto EndGetScheduledJob(IAsyncResult ar);
        IEnumerable<ScheduledJobLogDto> EndGetScheduledJobLogs(IAsyncResult ar);
        int EndGetScheduledJobLogsCount(IAsyncResult ar);
        ScheduledJobResultDto EndGetScheduledJobResult(IAsyncResult ar);
        IEnumerable<ScheduledJobResultCatalogItemDto> EndGetScheduledJobResults(IAsyncResult ar);
        int EndGetScheduledJobResultsCount(IAsyncResult ar);
        IEnumerable<ScheduledJobCatalogItemDto> EndGetScheduledJobs(IAsyncResult ar);
        ReportDto EndLoadReport(IAsyncResult ar);
        byte[] EndLoadReportLayoutByRevisionId(IAsyncResult ar);
        void EndPing(IAsyncResult ar);
        void EndRollBackReportLayout(IAsyncResult ar);
        int EndSaveReportById(IAsyncResult ar);
        void EndUpdateDataModel(IAsyncResult ar);
        int EndUpdateReport(IAsyncResult ar);
        void EndUpdateReportCategory(IAsyncResult ar);
        void EndUpdateScheduledJob(IAsyncResult ar);
        TransientReportId EndUploadLayout(IAsyncResult ar);
        [OperationContract]
        string GetDataSourceSchema(int dataSourceId, object asyncState);
        [OperationContract]
        ReportCatalogItemDto GetReportCatalogItemDto(int reportId);
        [Obsolete("This method has become obsolete."), OperationContract]
        void LockReport(int reportId);
        [Obsolete("This method has become obsolete."), OperationContract]
        void UnlockReport(int reportId);
    }
}

