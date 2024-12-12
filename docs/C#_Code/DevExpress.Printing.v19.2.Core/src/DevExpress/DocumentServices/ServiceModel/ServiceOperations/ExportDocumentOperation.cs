namespace DevExpress.DocumentServices.ServiceModel.ServiceOperations
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ExportDocumentOperation : ReportServiceOperation
    {
        protected ExportId exportId;
        protected readonly DocumentExportArgs exportArgs;

        public event EventHandler<ExportDocumentCompletedEventArgs> Completed;

        public event EventHandler<ExportDocumentProgressEventArgs> Progress;

        public event EventHandler<ExportDocumentStartedEventArgs> Started;

        public ExportDocumentOperation(IReportServiceClient client, DocumentId documentId, DocumentExportArgs exportArgs, TimeSpan statusUpdateInterval) : base(client, statusUpdateInterval, documentId)
        {
            Guard.ArgumentNotNull(exportArgs, "exportArgs");
            this.exportArgs = exportArgs;
        }

        public ExportDocumentOperation(IReportServiceClient client, DocumentId documentId, ExportFormat format, ExportOptions exportOptions, TimeSpan statusUpdateInterval) : this(client, documentId, format, exportOptions, statusUpdateInterval, null)
        {
        }

        public ExportDocumentOperation(IReportServiceClient client, DocumentId documentId, ExportFormat format, ExportOptions exportOptions, TimeSpan statusUpdateInterval, object customArgs) : base(client, statusUpdateInterval, documentId)
        {
            this.exportArgs = Helper.CreateDocumentExportArgs(format, exportOptions, customArgs);
        }

        private void client_GetExportedDocumentCompleted(object sender, ScalarOperationCompletedEventArgs<byte[]> e)
        {
            this.OnCompleted(new ExportDocumentCompletedEventArgs((e.Error == null) ? e.Result : null, this.exportId, e.Error, e.Cancelled, e.UserState));
        }

        private void client_GetTaskStatusCompleted(object sender, ScalarOperationCompletedEventArgs<ExportStatus> e)
        {
            if ((e.Cancelled || (e.Error != null)) || (e.Result.Status == TaskStatus.Fault))
            {
                Exception error = e.Error ?? e.Result.Fault.ToException();
                this.OnCompleted(new ExportDocumentCompletedEventArgs(null, this.exportId, error, e.Cancelled, e.UserState));
            }
            else
            {
                this.RaiseProgress(new ExportDocumentProgressEventArgs(e.Result.ProgressPosition));
                if (e.Result.Status == TaskStatus.InProgress)
                {
                    this.QueryTaskStatus();
                }
                else
                {
                    base.Client.GetExportedDocumentAsync(this.exportId, base.instanceId);
                }
            }
        }

        private void client_StartExportCompleted(object sender, ScalarOperationCompletedEventArgs<ExportId> e)
        {
            if (e.Cancelled || (e.Error != null))
            {
                this.OnCompleted(new ExportDocumentCompletedEventArgs(null, null, e.Error, e.Cancelled, e.UserState));
            }
            else
            {
                this.exportId = e.Result;
                this.RaiseStarted(this.exportId);
                this.QueryTaskStatus();
            }
        }

        protected void OnCompleted(ExportDocumentCompletedEventArgs args)
        {
            this.UnsubscribeClientEvents();
            if (this.Completed != null)
            {
                this.Completed(this, args);
            }
        }

        private void QueryTaskStatus()
        {
            base.Delayer.Execute(() => base.Client.GetExportStatusAsync(this.exportId, base.instanceId));
        }

        private void RaiseProgress(ExportDocumentProgressEventArgs args)
        {
            if (this.Progress != null)
            {
                this.Progress(this, args);
            }
        }

        private void RaiseStarted(ExportId exportId)
        {
            if (this.Started != null)
            {
                this.Started(this, new ExportDocumentStartedEventArgs(exportId));
            }
        }

        public override void Start()
        {
            base.Client.StartExportCompleted += new EventHandler<ScalarOperationCompletedEventArgs<ExportId>>(this.client_StartExportCompleted);
            base.Client.GetExportStatusCompleted += new EventHandler<ScalarOperationCompletedEventArgs<ExportStatus>>(this.client_GetTaskStatusCompleted);
            base.Client.GetExportedDocumentCompleted += new EventHandler<ScalarOperationCompletedEventArgs<byte[]>>(this.client_GetExportedDocumentCompleted);
            base.Client.StartExportAsync(base.documentId, this.exportArgs, base.instanceId);
        }

        public override void Stop()
        {
            throw new NotSupportedException("Export document operation can't be stopped.");
        }

        protected override void UnsubscribeClientEvents()
        {
            base.Client.StartExportCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<ExportId>>(this.client_StartExportCompleted);
            base.Client.GetExportStatusCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<ExportStatus>>(this.client_GetTaskStatusCompleted);
            base.Client.GetExportedDocumentCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<byte[]>>(this.client_GetExportedDocumentCompleted);
        }

        public override bool CanStop =>
            false;
    }
}

