namespace DevExpress.DocumentServices.ServiceModel.ServiceOperations
{
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class ReportServiceOperation : ServiceOperationBase
    {
        public event EventHandler CanStopChanged;

        protected ReportServiceOperation(IReportServiceClient client, TimeSpan statusUpdateInterval, DocumentId documentId) : base(client, statusUpdateInterval)
        {
            base.documentId = documentId;
        }

        public void Abort()
        {
            base.Delayer.Abort();
            this.UnsubscribeClientEvents();
            this.Aborted = true;
        }

        [Conditional("DEBUG")]
        protected void AssertAlive()
        {
        }

        protected void RaiseCanStopChanged()
        {
            if (this.CanStopChanged != null)
            {
                this.CanStopChanged(this, EventArgs.Empty);
            }
        }

        public abstract void Start();
        public abstract void Stop();
        protected override bool TryProcessError(AsyncCompletedEventArgs e) => 
            base.TryProcessError(e) || this.Aborted;

        protected bool Aborted { get; private set; }

        protected IReportServiceClient Client =>
            (IReportServiceClient) base.Client;

        public abstract bool CanStop { get; }
    }
}

