namespace DevExpress.DocumentServices.ServiceModel.ServiceOperations
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.DocumentServices.ServiceModel.Native;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class PrintDocumentOperation : ReportServiceOperation
    {
        private PrintId printId;
        private PrintingStatus printingStatus;
        private bool stopped;
        private readonly string documentName;
        private readonly PageCompatibility compatibility;
        protected readonly bool fIsDirectPrinting;

        public event EventHandler<PrintDocumentCompletedEventArgs> Completed;

        public event EventHandler<PrintDocumentProgressEventArgs> Progress;

        public event EventHandler<PrintDocumentStartedEventArgs> Started;

        public PrintDocumentOperation(IReportServiceClient client, DocumentId documentId, PageCompatibility compatibility, string documentName, TimeSpan statusUpdateInterval, bool isDirectPrinting) : base(client, statusUpdateInterval, documentId)
        {
            Guard.ArgumentNotNull(documentId, "documentId");
            Guard.ArgumentNotNull(documentName, "documentName");
            this.documentName = documentName;
            this.fIsDirectPrinting = isDirectPrinting;
            this.compatibility = compatibility;
        }

        private void client_GetPrintDocumentCompleted(object sender, ScalarOperationCompletedEventArgs<byte[]> e)
        {
            if (!e.Cancelled && (e.Error == null))
            {
                this.OnCompleted(Helper.DeserializePages(e.Result), e.Error, e.Cancelled, e.UserState);
            }
            else
            {
                this.OnCompleted(e.Error, e.Cancelled, e.UserState);
            }
        }

        private void client_GetPrintStatusCompleted(object sender, ScalarOperationCompletedEventArgs<PrintStatus> e)
        {
            if (e.Cancelled || ((e.Error != null) || (e.Result.Status == TaskStatus.Fault)))
            {
                Exception fault = e.Error ?? ((e.Result.Status == TaskStatus.Fault) ? e.Result.Fault.ToException() : null);
                this.OnCompleted(fault, e.Cancelled, e.UserState);
            }
            else
            {
                this.OnProgress(e.Result.ProgressPosition);
                if (e.Result.Status == TaskStatus.Complete)
                {
                    this.ProcessGetPrintDocument();
                }
                else
                {
                    this.QueryPrintStatus();
                }
            }
        }

        private void client_StartPrintCompleted(object sender, ScalarOperationCompletedEventArgs<PrintId> e)
        {
            if (e.Cancelled || (e.Error != null))
            {
                this.OnCompleted(e.Error, e.Cancelled, e.UserState);
            }
            else
            {
                this.printId = e.Result;
                this.printingStatus = PrintingStatus.Generating;
                this.OnStarted();
                base.RaiseCanStopChanged();
                this.QueryPrintStatus();
            }
        }

        private void client_StopPrintCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.OnCompleted(e.Error, e.Cancelled, e.UserState);
        }

        private void OnCompleted(Exception fault, bool cancelled, object userState)
        {
            this.OnCompleted(null, fault, cancelled, userState);
        }

        private void OnCompleted(string[] pages, Exception fault, bool cancelled, object userState)
        {
            this.UnsubscribeClientEvents();
            base.Delayer.Abort();
            this.printId = null;
            this.printingStatus = PrintingStatus.None;
            if (this.Completed != null)
            {
                this.Completed(this, new PrintDocumentCompletedEventArgs(pages, this.stopped, this.printId, fault, cancelled, userState));
            }
        }

        private void OnProgress(int progressPosition)
        {
            if (this.Progress != null)
            {
                this.Progress(this, new PrintDocumentProgressEventArgs(progressPosition));
            }
        }

        private void OnStarted()
        {
            if (this.Started != null)
            {
                this.Started(this, new PrintDocumentStartedEventArgs(this.printId));
            }
        }

        private void ProcessGetPrintDocument()
        {
            base.Client.GetPrintDocumentAsync(this.printId, base.instanceId);
        }

        private void QueryPrintStatus()
        {
            base.Delayer.Execute(() => base.Client.GetPrintStatusAsync(this.printId, base.instanceId));
        }

        public override void Start()
        {
            base.Client.StartPrintCompleted += new EventHandler<ScalarOperationCompletedEventArgs<PrintId>>(this.client_StartPrintCompleted);
            base.Client.StopPrintCompleted += new EventHandler<AsyncCompletedEventArgs>(this.client_StopPrintCompleted);
            base.Client.GetPrintStatusCompleted += new EventHandler<ScalarOperationCompletedEventArgs<PrintStatus>>(this.client_GetPrintStatusCompleted);
            base.Client.GetPrintDocumentCompleted += new EventHandler<ScalarOperationCompletedEventArgs<byte[]>>(this.client_GetPrintDocumentCompleted);
            base.Client.StartPrintAsync(base.documentId, this.compatibility, base.instanceId);
            this.stopped = false;
        }

        public override void Stop()
        {
            base.Client.StopPrintAsync(this.printId, base.instanceId);
            this.stopped = true;
        }

        protected override void UnsubscribeClientEvents()
        {
            base.Client.StartPrintCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<PrintId>>(this.client_StartPrintCompleted);
            base.Client.StopPrintCompleted -= new EventHandler<AsyncCompletedEventArgs>(this.client_StopPrintCompleted);
            base.Client.GetPrintStatusCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<PrintStatus>>(this.client_GetPrintStatusCompleted);
            base.Client.GetPrintDocumentCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<byte[]>>(this.client_GetPrintDocumentCompleted);
        }

        public bool IsDirectPrinting =>
            this.fIsDirectPrinting;

        public override bool CanStop =>
            !this.stopped && ((this.printId != null) && (this.printingStatus == PrintingStatus.Generating));
    }
}

