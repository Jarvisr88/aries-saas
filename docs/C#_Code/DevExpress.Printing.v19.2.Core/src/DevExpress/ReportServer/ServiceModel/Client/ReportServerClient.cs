namespace DevExpress.ReportServer.ServiceModel.Client
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.Data.XtraReports.DataProviders;
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.ReportServer.ServiceModel.DataContracts;
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.ServiceModel;
    using System.Threading;
    using System.Threading.Tasks;

    public class ReportServerClient : ReportServiceClient, IReportServerClient, IReportServiceClient, IServiceClientBase
    {
        public event EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<ColumnInfo>>> GetColumnsCompleted;

        public event EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<TableInfo>>> GetDataMembersCompleted;

        public event EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<DataSourceInfo>>> GetDataSourcesCompleted;

        public ReportServerClient(IReportServerFacadeAsync channel) : base(channel)
        {
        }

        [Obsolete("Use the ReportServerClient(IReportServerFacadeAsync channel) constructor instead.")]
        public ReportServerClient(IReportServerFacadeAsync channel, string restEndpointAddress) : this(channel)
        {
        }

        public void CloneReport(int sourceReportId, ReportDto reportDto, object asyncState, Action<ScalarOperationCompletedEventArgs<ReportDto>> onCompleted)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                Func<EventHandler<ScalarOperationCompletedEventArgs<ReportDto>>> <>9__1;
                IReportServerFacadeAsync channel = this.Channel;
                Func<EventHandler<ScalarOperationCompletedEventArgs<ReportDto>>> getCompletedEvent = <>9__1;
                if (<>9__1 == null)
                {
                    Func<EventHandler<ScalarOperationCompletedEventArgs<ReportDto>>> local1 = <>9__1;
                    getCompletedEvent = <>9__1 = delegate {
                        EventHandler<ScalarOperationCompletedEventArgs<ReportDto>> <>9__2;
                        EventHandler<ScalarOperationCompletedEventArgs<ReportDto>> handler2 = <>9__2;
                        if (<>9__2 == null)
                        {
                            EventHandler<ScalarOperationCompletedEventArgs<ReportDto>> local1 = <>9__2;
                            handler2 = <>9__2 = (s, args) => onCompleted(args);
                        }
                        return handler2;
                    };
                }
                this.EndScalarOperation<ReportDto>(ar, new Func<IAsyncResult, ReportDto>(channel.EndCloneReport), getCompletedEvent);
            };
            this.Channel.BeginCloneReport(sourceReportId, reportDto, callback, asyncState);
        }

        public Task<int> CreateCategoryAsync(string name, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<string, int>(new Func<string, AsyncCallback, object, IAsyncResult>(channel.BeginCreateReportCategory), new Func<IAsyncResult, int>(async2.EndCreateReportCategory), name, asyncState);
        }

        public void CreateReport(ReportDto message, object asyncState, Action<ScalarOperationCompletedEventArgs<CreateReportResult>> onCompleted)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                Func<EventHandler<ScalarOperationCompletedEventArgs<CreateReportResult>>> <>9__1;
                IReportServerFacadeAsync channel = this.Channel;
                Func<EventHandler<ScalarOperationCompletedEventArgs<CreateReportResult>>> getCompletedEvent = <>9__1;
                if (<>9__1 == null)
                {
                    Func<EventHandler<ScalarOperationCompletedEventArgs<CreateReportResult>>> local1 = <>9__1;
                    getCompletedEvent = <>9__1 = delegate {
                        EventHandler<ScalarOperationCompletedEventArgs<CreateReportResult>> <>9__2;
                        EventHandler<ScalarOperationCompletedEventArgs<CreateReportResult>> handler2 = <>9__2;
                        if (<>9__2 == null)
                        {
                            EventHandler<ScalarOperationCompletedEventArgs<CreateReportResult>> local1 = <>9__2;
                            handler2 = <>9__2 = (s, args) => onCompleted(args);
                        }
                        return handler2;
                    };
                }
                this.EndScalarOperation<CreateReportResult>(ar, new Func<IAsyncResult, CreateReportResult>(channel.EndCreateReport), getCompletedEvent);
            };
            this.Channel.BeginCreateReport(message, callback, asyncState);
        }

        public Task<CreateReportResult> CreateReportAsync(ReportDto message, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<ReportDto, CreateReportResult>(new Func<ReportDto, AsyncCallback, object, IAsyncResult>(channel.BeginCreateReport), new Func<IAsyncResult, CreateReportResult>(async2.EndCreateReport), message, asyncState);
        }

        public void CreateReportCategory(string categoryName, object asyncState, Action<ScalarOperationCompletedEventArgs<int>> onCompleted)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                Func<EventHandler<ScalarOperationCompletedEventArgs<int>>> <>9__1;
                IReportServerFacadeAsync channel = this.Channel;
                Func<EventHandler<ScalarOperationCompletedEventArgs<int>>> getCompletedEvent = <>9__1;
                if (<>9__1 == null)
                {
                    Func<EventHandler<ScalarOperationCompletedEventArgs<int>>> local1 = <>9__1;
                    getCompletedEvent = <>9__1 = delegate {
                        EventHandler<ScalarOperationCompletedEventArgs<int>> <>9__2;
                        EventHandler<ScalarOperationCompletedEventArgs<int>> handler2 = <>9__2;
                        if (<>9__2 == null)
                        {
                            EventHandler<ScalarOperationCompletedEventArgs<int>> local1 = <>9__2;
                            handler2 = <>9__2 = (s, e) => onCompleted(e);
                        }
                        return handler2;
                    };
                }
                this.EndScalarOperation<int>(ar, new Func<IAsyncResult, int>(channel.EndCreateReportCategory), getCompletedEvent);
            };
            this.Channel.BeginCreateReportCategory(categoryName, callback, asyncState);
        }

        public Task<int> CreateScheduledJobAsync(ScheduledJobDto scheduledJob, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<ScheduledJobDto, int>(new Func<ScheduledJobDto, AsyncCallback, object, IAsyncResult>(channel.BeginCreateScheduledJob), new Func<IAsyncResult, int>(async2.EndCreateScheduledJob), scheduledJob, asyncState);
        }

        public Task DeleteCategoryAsync(int id, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<int>(new Func<int, AsyncCallback, object, IAsyncResult>(channel.BeginDeleteCategory), new Action<IAsyncResult>(async2.EndDeleteCategory), id, asyncState);
        }

        public Task DeleteDataModelAsync(int id, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<int>(new Func<int, AsyncCallback, object, IAsyncResult>(channel.BeginDeleteDataModel), new Action<IAsyncResult>(async2.EndDeleteDataModel), id, asyncState);
        }

        public void DeleteReport(int reportId, object asyncState, Action<AsyncCompletedEventArgs> onCompleted)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                Func<EventHandler<AsyncCompletedEventArgs>> <>9__1;
                IReportServerFacadeAsync channel = this.Channel;
                Func<EventHandler<AsyncCompletedEventArgs>> getCompletedEvent = <>9__1;
                if (<>9__1 == null)
                {
                    Func<EventHandler<AsyncCompletedEventArgs>> local1 = <>9__1;
                    getCompletedEvent = <>9__1 = delegate {
                        EventHandler<AsyncCompletedEventArgs> <>9__2;
                        EventHandler<AsyncCompletedEventArgs> handler2 = <>9__2;
                        if (<>9__2 == null)
                        {
                            EventHandler<AsyncCompletedEventArgs> local1 = <>9__2;
                            handler2 = <>9__2 = (s, args) => onCompleted(args);
                        }
                        return handler2;
                    };
                }
                this.EndVoidOperation(ar, new Action<IAsyncResult>(channel.EndDeleteReport), getCompletedEvent);
            };
            this.Channel.BeginDeleteReport(reportId, callback, asyncState);
        }

        public void DeleteReportCategory(int categoryId, object asyncState, Action<AsyncCompletedEventArgs> onCompleted)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                Func<EventHandler<AsyncCompletedEventArgs>> <>9__1;
                IReportServerFacadeAsync channel = this.Channel;
                Func<EventHandler<AsyncCompletedEventArgs>> getCompletedEvent = <>9__1;
                if (<>9__1 == null)
                {
                    Func<EventHandler<AsyncCompletedEventArgs>> local1 = <>9__1;
                    getCompletedEvent = <>9__1 = delegate {
                        EventHandler<AsyncCompletedEventArgs> <>9__2;
                        EventHandler<AsyncCompletedEventArgs> handler2 = <>9__2;
                        if (<>9__2 == null)
                        {
                            EventHandler<AsyncCompletedEventArgs> local1 = <>9__2;
                            handler2 = <>9__2 = (s, args) => onCompleted(args);
                        }
                        return handler2;
                    };
                }
                this.EndVoidOperation(ar, new Action<IAsyncResult>(channel.EndDeleteCategory), getCompletedEvent);
            };
            this.Channel.BeginDeleteCategory(categoryId, callback, asyncState);
        }

        public Task DeleteScheduledJobAsync(int id, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<int>(new Func<int, AsyncCallback, object, IAsyncResult>(channel.BeginDeleteScheduledJob), new Action<IAsyncResult>(async2.EndDeleteScheduledJob), id, asyncState);
        }

        public Task<int?> ExecuteJobAsync(int scheduledJobId, int? scheduledJobResult, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<int, int?, int?>(new Func<int, int?, AsyncCallback, object, IAsyncResult>(channel.BeginExecuteJob), new Func<IAsyncResult, int?>(async2.EndExecuteJob), scheduledJobId, scheduledJobResult, asyncState);
        }

        public void GetCategories(object asyncState, Action<ScalarOperationCompletedEventArgs<IEnumerable<CategoryDto>>> onCompleted)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                Func<EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<CategoryDto>>>> <>9__1;
                IReportServerFacadeAsync channel = this.Channel;
                Func<EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<CategoryDto>>>> getCompletedEvent = <>9__1;
                if (<>9__1 == null)
                {
                    Func<EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<CategoryDto>>>> local1 = <>9__1;
                    getCompletedEvent = <>9__1 = delegate {
                        EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<CategoryDto>>> <>9__2;
                        EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<CategoryDto>>> handler2 = <>9__2;
                        if (<>9__2 == null)
                        {
                            EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<CategoryDto>>> local1 = <>9__2;
                            handler2 = <>9__2 = (s, args) => onCompleted(args);
                        }
                        return handler2;
                    };
                }
                this.EndScalarOperation<IEnumerable<CategoryDto>>(ar, new Func<IAsyncResult, IEnumerable<CategoryDto>>(channel.EndGetCategories), getCompletedEvent);
            };
            this.Channel.BeginGetCategories(callback, asyncState);
        }

        public Task<IEnumerable<CategoryDto>> GetCategoriesAsync(object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<IEnumerable<CategoryDto>>(new Func<AsyncCallback, object, IAsyncResult>(channel.BeginGetCategories), new Func<IAsyncResult, IEnumerable<CategoryDto>>(async2.EndGetCategories), asyncState);
        }

        public Task<IEnumerable<ColumnInfo>> GetColumnsAsync(string dataSourceName, TableInfo dataMemberName)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<string, TableInfo, IEnumerable<ColumnInfo>>(new Func<string, TableInfo, AsyncCallback, object, IAsyncResult>(channel.BeginGetColumns), new Func<IAsyncResult, IEnumerable<ColumnInfo>>(async2.EndGetColumns), dataSourceName, dataMemberName, null);
        }

        public void GetColumnsAsync(string dataSourceName, TableInfo dataMemberName, object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                IReportServerFacadeAsync channel = this.Channel;
                base.EndScalarOperation<IEnumerable<ColumnInfo>>(ar, new Func<IAsyncResult, IEnumerable<ColumnInfo>>(channel.EndGetColumns), () => this.GetColumnsCompleted);
            };
            this.Channel.BeginGetColumns(dataSourceName, dataMemberName, callback, asyncState);
        }

        public Task<IEnumerable<TableInfo>> GetDataMembersAsync(string dataSourceName)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<string, IEnumerable<TableInfo>>(new Func<string, AsyncCallback, object, IAsyncResult>(channel.BeginGetDataMembers), new Func<IAsyncResult, IEnumerable<TableInfo>>(async2.EndGetDataMembers), dataSourceName, null);
        }

        public void GetDataMembersAsync(string dataSourceName, object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                IReportServerFacadeAsync channel = this.Channel;
                base.EndScalarOperation<IEnumerable<TableInfo>>(ar, new Func<IAsyncResult, IEnumerable<TableInfo>>(channel.EndGetDataMembers), () => this.GetDataMembersCompleted);
            };
            this.Channel.BeginGetDataMembers(dataSourceName, callback, asyncState);
        }

        public Task<DataModelDto> GetDataModelAsync(int id, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<int, DataModelDto>(new Func<int, AsyncCallback, object, IAsyncResult>(channel.BeginGetDataModel), new Func<IAsyncResult, DataModelDto>(async2.EndGetDataModel), id, asyncState);
        }

        public Task<IEnumerable<DataModelDto>> GetDataModelsAsync(object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<IEnumerable<DataModelDto>>(new Func<AsyncCallback, object, IAsyncResult>(channel.BeginGetDataModels), new Func<IAsyncResult, IEnumerable<DataModelDto>>(async2.EndGetDataModels), asyncState);
        }

        public Task<IEnumerable<DataSourceInfo>> GetDataSourcesAsync()
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<IEnumerable<DataSourceInfo>>(new Func<AsyncCallback, object, IAsyncResult>(channel.BeginGetDataSources), new Func<IAsyncResult, IEnumerable<DataSourceInfo>>(async2.EndGetDataSources), null);
        }

        public void GetDataSourcesAsync(object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                IReportServerFacadeAsync channel = this.Channel;
                base.EndScalarOperation<IEnumerable<DataSourceInfo>>(ar, new Func<IAsyncResult, IEnumerable<DataSourceInfo>>(channel.EndGetDataSources), () => this.GetDataSourcesCompleted);
            };
            this.Channel.BeginGetDataSources(callback, asyncState);
        }

        public string GetDataSourceSchema(int dataSourceId, object asyncState) => 
            this.Channel.GetDataSourceSchema(dataSourceId, asyncState);

        public ReportCatalogItemDto GetReportCatalogItemDto(int reportId) => 
            this.Channel.GetReportCatalogItemDto(reportId);

        public void GetReportRevisions(int reportId, object asyncState, Action<ScalarOperationCompletedEventArgs<IEnumerable<LayoutRevisionDto>>> onCompleted)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                Func<EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<LayoutRevisionDto>>>> <>9__1;
                IReportServerFacadeAsync channel = this.Channel;
                Func<EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<LayoutRevisionDto>>>> getCompletedEvent = <>9__1;
                if (<>9__1 == null)
                {
                    Func<EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<LayoutRevisionDto>>>> local1 = <>9__1;
                    getCompletedEvent = <>9__1 = delegate {
                        EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<LayoutRevisionDto>>> <>9__2;
                        EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<LayoutRevisionDto>>> handler2 = <>9__2;
                        if (<>9__2 == null)
                        {
                            EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<LayoutRevisionDto>>> local1 = <>9__2;
                            handler2 = <>9__2 = (s, args) => onCompleted(args);
                        }
                        return handler2;
                    };
                }
                this.EndScalarOperation<IEnumerable<LayoutRevisionDto>>(ar, new Func<IAsyncResult, IEnumerable<LayoutRevisionDto>>(channel.EndGetReportRevisions), getCompletedEvent);
            };
            this.Channel.BeginGetReportRevisions(reportId, callback, asyncState);
        }

        public void GetReports(object asyncState, Action<ScalarOperationCompletedEventArgs<IEnumerable<ReportCatalogItemDto>>> onCompleted)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                Func<EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<ReportCatalogItemDto>>>> <>9__1;
                IReportServerFacadeAsync channel = this.Channel;
                Func<EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<ReportCatalogItemDto>>>> getCompletedEvent = <>9__1;
                if (<>9__1 == null)
                {
                    Func<EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<ReportCatalogItemDto>>>> local1 = <>9__1;
                    getCompletedEvent = <>9__1 = delegate {
                        EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<ReportCatalogItemDto>>> <>9__2;
                        EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<ReportCatalogItemDto>>> handler2 = <>9__2;
                        if (<>9__2 == null)
                        {
                            EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<ReportCatalogItemDto>>> local1 = <>9__2;
                            handler2 = <>9__2 = (s, args) => onCompleted(args);
                        }
                        return handler2;
                    };
                }
                this.EndScalarOperation<IEnumerable<ReportCatalogItemDto>>(ar, new Func<IAsyncResult, IEnumerable<ReportCatalogItemDto>>(channel.EndGetReports), getCompletedEvent);
            };
            this.Channel.BeginGetReports(callback, asyncState);
        }

        public Task<IEnumerable<ReportCatalogItemDto>> GetReportsAsync(object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<IEnumerable<ReportCatalogItemDto>>(new Func<AsyncCallback, object, IAsyncResult>(channel.BeginGetReports), new Func<IAsyncResult, IEnumerable<ReportCatalogItemDto>>(async2.EndGetReports), asyncState);
        }

        public Task<ScheduledJobDto> GetScheduledJobAsync(int id, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<int, ScheduledJobDto>(new Func<int, AsyncCallback, object, IAsyncResult>(channel.BeginGetScheduledJob), new Func<IAsyncResult, ScheduledJobDto>(async2.EndGetScheduledJob), id, asyncState);
        }

        public Task<IEnumerable<ScheduledJobLogDto>> GetScheduledJobLogsAsync(int scheduledJobId, DataPagination pagination, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<int, DataPagination, IEnumerable<ScheduledJobLogDto>>(new Func<int, DataPagination, AsyncCallback, object, IAsyncResult>(channel.BeginGetScheduledJobLogs), new Func<IAsyncResult, IEnumerable<ScheduledJobLogDto>>(async2.EndGetScheduledJobLogs), scheduledJobId, pagination, asyncState);
        }

        public Task<int> GetScheduledJobLogsCountAsync(int scheduledJobId, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<int, int>(new Func<int, AsyncCallback, object, IAsyncResult>(channel.BeginGetScheduledJobLogsCount), new Func<IAsyncResult, int>(async2.EndGetScheduledJobLogsCount), scheduledJobId, asyncState);
        }

        public Task<ScheduledJobResultDto> GetScheduledJobResultAsync(int id, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<int, ScheduledJobResultDto>(new Func<int, AsyncCallback, object, IAsyncResult>(channel.BeginGetScheduledJobResult), new Func<IAsyncResult, ScheduledJobResultDto>(async2.EndGetScheduledJobResult), id, asyncState);
        }

        public Task<IEnumerable<ScheduledJobResultCatalogItemDto>> GetScheduledJobResultsAsync(int scheduledJobLogId, DataPagination pagination, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<int, DataPagination, IEnumerable<ScheduledJobResultCatalogItemDto>>(new Func<int, DataPagination, AsyncCallback, object, IAsyncResult>(channel.BeginGetScheduledJobResults), new Func<IAsyncResult, IEnumerable<ScheduledJobResultCatalogItemDto>>(async2.EndGetScheduledJobResults), scheduledJobLogId, pagination, asyncState);
        }

        public Task<int> GetScheduledJobResultsCountAsync(int scheduledJobLogId, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<int, int>(new Func<int, AsyncCallback, object, IAsyncResult>(channel.BeginGetScheduledJobResultsCount), new Func<IAsyncResult, int>(async2.EndGetScheduledJobResultsCount), scheduledJobLogId, asyncState);
        }

        public Task<IEnumerable<ScheduledJobCatalogItemDto>> GetScheduledJobsAsync(object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<IEnumerable<ScheduledJobCatalogItemDto>>(new Func<AsyncCallback, object, IAsyncResult>(channel.BeginGetScheduledJobs), new Func<IAsyncResult, IEnumerable<ScheduledJobCatalogItemDto>>(async2.EndGetScheduledJobs), asyncState);
        }

        public void LoadReport(int reportId, object asyncState, Action<ScalarOperationCompletedEventArgs<ReportDto>> onCompleted)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                Func<EventHandler<ScalarOperationCompletedEventArgs<ReportDto>>> <>9__1;
                IReportServerFacadeAsync channel = this.Channel;
                Func<EventHandler<ScalarOperationCompletedEventArgs<ReportDto>>> getCompletedEvent = <>9__1;
                if (<>9__1 == null)
                {
                    Func<EventHandler<ScalarOperationCompletedEventArgs<ReportDto>>> local1 = <>9__1;
                    getCompletedEvent = <>9__1 = delegate {
                        EventHandler<ScalarOperationCompletedEventArgs<ReportDto>> <>9__2;
                        EventHandler<ScalarOperationCompletedEventArgs<ReportDto>> handler2 = <>9__2;
                        if (<>9__2 == null)
                        {
                            EventHandler<ScalarOperationCompletedEventArgs<ReportDto>> local1 = <>9__2;
                            handler2 = <>9__2 = (s, args) => onCompleted(args);
                        }
                        return handler2;
                    };
                }
                this.EndScalarOperation<ReportDto>(ar, new Func<IAsyncResult, ReportDto>(channel.EndLoadReport), getCompletedEvent);
            };
            this.Channel.BeginLoadReport(reportId, callback, asyncState);
        }

        public Task<ReportDto> LoadReportAsync(int id, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<int, ReportDto>(new Func<int, AsyncCallback, object, IAsyncResult>(channel.BeginLoadReport), new Func<IAsyncResult, ReportDto>(async2.EndLoadReport), id, asyncState);
        }

        public void LoadReportLayoutByRevisionId(int reportId, int revisionId, object asyncState, Action<ScalarOperationCompletedEventArgs<byte[]>> onCompleted)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                Func<EventHandler<ScalarOperationCompletedEventArgs<byte[]>>> <>9__1;
                IReportServerFacadeAsync channel = this.Channel;
                Func<EventHandler<ScalarOperationCompletedEventArgs<byte[]>>> getCompletedEvent = <>9__1;
                if (<>9__1 == null)
                {
                    Func<EventHandler<ScalarOperationCompletedEventArgs<byte[]>>> local1 = <>9__1;
                    getCompletedEvent = <>9__1 = delegate {
                        EventHandler<ScalarOperationCompletedEventArgs<byte[]>> <>9__2;
                        EventHandler<ScalarOperationCompletedEventArgs<byte[]>> handler2 = <>9__2;
                        if (<>9__2 == null)
                        {
                            EventHandler<ScalarOperationCompletedEventArgs<byte[]>> local1 = <>9__2;
                            handler2 = <>9__2 = (s, args) => onCompleted(args);
                        }
                        return handler2;
                    };
                }
                this.EndScalarOperation<byte[]>(ar, new Func<IAsyncResult, byte[]>(channel.EndLoadReportLayoutByRevisionId), getCompletedEvent);
            };
            this.Channel.BeginLoadReportLayoutByRevisionId(reportId, revisionId, callback, asyncState);
        }

        [Obsolete("This method has become obsolete.")]
        public void LockReport(int reportId)
        {
            this.Channel.LockReport(reportId);
        }

        public void Ping(Action<AsyncCompletedEventArgs> onCompleted, object asyncState)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                Func<EventHandler<AsyncCompletedEventArgs>> <>9__1;
                IReportServerFacadeAsync channel = this.Channel;
                Func<EventHandler<AsyncCompletedEventArgs>> getCompletedEvent = <>9__1;
                if (<>9__1 == null)
                {
                    Func<EventHandler<AsyncCompletedEventArgs>> local1 = <>9__1;
                    getCompletedEvent = <>9__1 = delegate {
                        EventHandler<AsyncCompletedEventArgs> <>9__2;
                        EventHandler<AsyncCompletedEventArgs> handler2 = <>9__2;
                        if (<>9__2 == null)
                        {
                            EventHandler<AsyncCompletedEventArgs> local1 = <>9__2;
                            handler2 = <>9__2 = (s, args) => onCompleted(args);
                        }
                        return handler2;
                    };
                }
                this.EndVoidOperation(ar, new Action<IAsyncResult>(channel.EndPing), getCompletedEvent);
            };
            this.Channel.BeginPing(callback, asyncState);
        }

        public void RollbackReportLayout(int reportId, int revisionId, object asyncState, Action<AsyncCompletedEventArgs> onCompleted)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                Func<EventHandler<AsyncCompletedEventArgs>> <>9__1;
                IReportServerFacadeAsync channel = this.Channel;
                Func<EventHandler<AsyncCompletedEventArgs>> getCompletedEvent = <>9__1;
                if (<>9__1 == null)
                {
                    Func<EventHandler<AsyncCompletedEventArgs>> local1 = <>9__1;
                    getCompletedEvent = <>9__1 = delegate {
                        EventHandler<AsyncCompletedEventArgs> <>9__2;
                        EventHandler<AsyncCompletedEventArgs> handler2 = <>9__2;
                        if (<>9__2 == null)
                        {
                            EventHandler<AsyncCompletedEventArgs> local1 = <>9__2;
                            handler2 = <>9__2 = (s, args) => onCompleted(args);
                        }
                        return handler2;
                    };
                }
                this.EndVoidOperation(ar, new Action<IAsyncResult>(channel.EndRollBackReportLayout), getCompletedEvent);
            };
            this.Channel.BeginRollBackReportLayout(reportId, revisionId, callback, asyncState);
        }

        public void SaveReportById(int reportId, ReportDto reportDto, object asyncState, Action<ScalarOperationCompletedEventArgs<int>> onCompleted)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                Func<EventHandler<ScalarOperationCompletedEventArgs<int>>> <>9__1;
                IReportServerFacadeAsync channel = this.Channel;
                Func<EventHandler<ScalarOperationCompletedEventArgs<int>>> getCompletedEvent = <>9__1;
                if (<>9__1 == null)
                {
                    Func<EventHandler<ScalarOperationCompletedEventArgs<int>>> local1 = <>9__1;
                    getCompletedEvent = <>9__1 = delegate {
                        EventHandler<ScalarOperationCompletedEventArgs<int>> <>9__2;
                        EventHandler<ScalarOperationCompletedEventArgs<int>> handler2 = <>9__2;
                        if (<>9__2 == null)
                        {
                            EventHandler<ScalarOperationCompletedEventArgs<int>> local1 = <>9__2;
                            handler2 = <>9__2 = (s, args) => onCompleted(args);
                        }
                        return handler2;
                    };
                }
                this.EndScalarOperation<int>(ar, new Func<IAsyncResult, int>(channel.EndSaveReportById), getCompletedEvent);
            };
            this.Channel.BeginSaveReportById(reportId, reportDto, callback, asyncState);
        }

        [Obsolete("This method has become obsolete.")]
        public void UnlockReport(int reportId)
        {
            this.Channel.UnlockReport(reportId);
        }

        public Task UpdateCategoryAsync(int id, string name, int? optimisticLock, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<int, string, int?>(new Func<int, string, int?, AsyncCallback, object, IAsyncResult>(channel.BeginUpdateReportCategory), new Action<IAsyncResult>(async2.EndUpdateReportCategory), id, name, optimisticLock, asyncState);
        }

        public Task UpdateDataModelAsync(DataModelDto dataModel, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<DataModelDto>(new Func<DataModelDto, AsyncCallback, object, IAsyncResult>(channel.BeginUpdateDataModel), new Action<IAsyncResult>(async2.EndUpdateDataModel), dataModel, asyncState);
        }

        public void UpdateReport(int reportId, ReportDto reportDto, object asyncState, Action<ScalarOperationCompletedEventArgs<int>> onCompleted)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                Func<EventHandler<ScalarOperationCompletedEventArgs<int>>> <>9__1;
                IReportServerFacadeAsync channel = this.Channel;
                Func<EventHandler<ScalarOperationCompletedEventArgs<int>>> getCompletedEvent = <>9__1;
                if (<>9__1 == null)
                {
                    Func<EventHandler<ScalarOperationCompletedEventArgs<int>>> local1 = <>9__1;
                    getCompletedEvent = <>9__1 = delegate {
                        EventHandler<ScalarOperationCompletedEventArgs<int>> <>9__2;
                        EventHandler<ScalarOperationCompletedEventArgs<int>> handler2 = <>9__2;
                        if (<>9__2 == null)
                        {
                            EventHandler<ScalarOperationCompletedEventArgs<int>> local1 = <>9__2;
                            handler2 = <>9__2 = (s, args) => onCompleted(args);
                        }
                        return handler2;
                    };
                }
                this.EndScalarOperation<int>(ar, new Func<IAsyncResult, int>(channel.EndUpdateReport), getCompletedEvent);
            };
            this.Channel.BeginUpdateReport(reportId, reportDto, callback, asyncState);
        }

        public Task<int> UpdateReportAsync(int reportId, ReportDto reportDto, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<int, ReportDto, int>(new Func<int, ReportDto, AsyncCallback, object, IAsyncResult>(channel.BeginUpdateReport), new Func<IAsyncResult, int>(async2.EndUpdateReport), reportId, reportDto, asyncState);
        }

        public void UpdateReportCategory(int categoryId, string name, int? optimisticLock, object asyncState, Action<AsyncCompletedEventArgs> onCompleted)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                Func<EventHandler<AsyncCompletedEventArgs>> <>9__1;
                IReportServerFacadeAsync channel = this.Channel;
                Func<EventHandler<AsyncCompletedEventArgs>> getCompletedEvent = <>9__1;
                if (<>9__1 == null)
                {
                    Func<EventHandler<AsyncCompletedEventArgs>> local1 = <>9__1;
                    getCompletedEvent = <>9__1 = delegate {
                        EventHandler<AsyncCompletedEventArgs> <>9__2;
                        EventHandler<AsyncCompletedEventArgs> handler2 = <>9__2;
                        if (<>9__2 == null)
                        {
                            EventHandler<AsyncCompletedEventArgs> local1 = <>9__2;
                            handler2 = <>9__2 = (s, args) => onCompleted(args);
                        }
                        return handler2;
                    };
                }
                this.EndVoidOperation(ar, new Action<IAsyncResult>(channel.EndUpdateReportCategory), getCompletedEvent);
            };
            this.Channel.BeginUpdateReportCategory(categoryId, name, optimisticLock, callback, asyncState);
        }

        public Task UpdateScheduledJobAsync(ScheduledJobDto scheduledJob, object asyncState)
        {
            IReportServerFacadeAsync channel = this.Channel;
            IReportServerFacadeAsync async2 = this.Channel;
            return Task.Factory.FromAsync<ScheduledJobDto>(new Func<ScheduledJobDto, AsyncCallback, object, IAsyncResult>(channel.BeginUpdateScheduledJob), new Action<IAsyncResult>(async2.EndUpdateScheduledJob), scheduledJob, asyncState);
        }

        public void UploadLayout(Stream layout, object asyncState, Action<ScalarOperationCompletedEventArgs<TransientReportId>> onCompleted)
        {
            AsyncCallback callback = delegate (IAsyncResult ar) {
                Func<EventHandler<ScalarOperationCompletedEventArgs<TransientReportId>>> <>9__1;
                IReportServerFacadeAsync channel = this.Channel;
                Func<EventHandler<ScalarOperationCompletedEventArgs<TransientReportId>>> getCompletedEvent = <>9__1;
                if (<>9__1 == null)
                {
                    Func<EventHandler<ScalarOperationCompletedEventArgs<TransientReportId>>> local1 = <>9__1;
                    getCompletedEvent = <>9__1 = delegate {
                        EventHandler<ScalarOperationCompletedEventArgs<TransientReportId>> <>9__2;
                        EventHandler<ScalarOperationCompletedEventArgs<TransientReportId>> handler2 = <>9__2;
                        if (<>9__2 == null)
                        {
                            EventHandler<ScalarOperationCompletedEventArgs<TransientReportId>> local1 = <>9__2;
                            handler2 = <>9__2 = (s, args) => onCompleted(args);
                        }
                        return handler2;
                    };
                }
                this.EndScalarOperation<TransientReportId>(ar, new Func<IAsyncResult, TransientReportId>(channel.EndUploadLayout), getCompletedEvent);
            };
            this.Channel.BeginUploadLayout(layout, callback, asyncState);
        }

        private IReportServerFacadeAsync Channel =>
            (IReportServerFacadeAsync) base.Channel;

        public IContextChannel ContextChannel =>
            (IContextChannel) this.Channel;
    }
}

