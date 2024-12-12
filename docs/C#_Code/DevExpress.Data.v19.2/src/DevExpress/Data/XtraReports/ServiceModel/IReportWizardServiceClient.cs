namespace DevExpress.Data.XtraReports.ServiceModel
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.Data.XtraReports.DataProviders;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [Obsolete("Use the DevExpress.ReportServer.ServiceModel.Client.IReportServerClient interface from the Printing.Core assembly instead."), EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReportWizardServiceClient : IServiceClientBase
    {
        event EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<ColumnInfo>>> GetColumnsCompleted;

        event EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<TableInfo>>> GetDataMembersCompleted;

        event EventHandler<ScalarOperationCompletedEventArgs<IEnumerable<DataSourceInfo>>> GetDataSourcesCompleted;

        void GetColumnsAsync(string dataSourceName, TableInfo dataMemberName, object asyncState);
        void GetDataMembersAsync(string dataSourceName, object asyncState);
        void GetDataSourcesAsync(object asyncState);
    }
}

