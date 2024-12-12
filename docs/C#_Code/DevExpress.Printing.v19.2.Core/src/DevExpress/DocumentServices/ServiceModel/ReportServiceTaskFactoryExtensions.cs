namespace DevExpress.DocumentServices.ServiceModel
{
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public static class ReportServiceTaskFactoryExtensions
    {
        public static Task<byte[]> ExportReportAsync(this TaskFactory taskFactory, IReportServiceClient client, InstanceIdentity reportIdentity, ExportOptionsBase exportOptions, ReportParameter[] parameters, object asyncState)
        {
            Guard.ArgumentNotNull(client, "client");
            Guard.ArgumentNotNull(reportIdentity, "reportIdentity");
            Guard.ArgumentNotNull(exportOptions, "exportOptions");
            TaskCompletionSource<byte[]> source = new TaskCompletionSource<byte[]>(asyncState);
            ExportReportTask task = new ExportReportTask(client);
            task.Completed += new AsyncCompletedEventHandler(ReportServiceTaskFactoryExtensions.ReportServiceTaskCompleted<byte[]>);
            task.ExecuteAsync(reportIdentity, exportOptions, parameters, source);
            return source.Task;
        }

        public static Task<byte[]> ExportReportAsync(this TaskFactory taskFactory, IReportServiceClient client, string reportName, ExportOptionsBase exportOptions, ReportParameter[] parameters, object asyncState)
        {
            Guard.ArgumentIsNotNullOrEmpty(reportName, "reportName");
            return taskFactory.ExportReportAsync(client, new ReportNameIdentity(reportName), exportOptions, parameters, asyncState);
        }

        private static void ReportServiceTaskCompleted<TResult>(object sender, AsyncCompletedEventArgs e)
        {
            ReportServiceTaskBase<TResult> base2 = (ReportServiceTaskBase<TResult>) sender;
            base2.Completed -= new AsyncCompletedEventHandler(ReportServiceTaskFactoryExtensions.ReportServiceTaskCompleted<TResult>);
            TaskCompletionSource<TResult> userState = (TaskCompletionSource<TResult>) e.UserState;
            if (e.Error != null)
            {
                userState.SetException(e.Error);
            }
            else if (e.Cancelled)
            {
                userState.SetCanceled();
            }
            else
            {
                userState.SetResult(base2.Result);
            }
        }
    }
}

