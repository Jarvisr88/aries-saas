namespace DevExpress.DocumentServices.ServiceModel
{
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.DocumentServices.ServiceModel.ServiceOperations;
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal abstract class ReportServiceTaskBase<TResult>
    {
        protected static readonly TimeSpan DefaultUpdateStatusInterval;
        private readonly IReportServiceClient client;
        [CompilerGenerated]
        private AsyncCompletedEventHandler Completed;

        public event AsyncCompletedEventHandler Completed
        {
            [CompilerGenerated] add
            {
                AsyncCompletedEventHandler completed = this.Completed;
                while (true)
                {
                    AsyncCompletedEventHandler comparand = completed;
                    AsyncCompletedEventHandler handler3 = comparand + value;
                    completed = Interlocked.CompareExchange<AsyncCompletedEventHandler>(ref this.Completed, handler3, comparand);
                    if (ReferenceEquals(completed, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                AsyncCompletedEventHandler completed = this.Completed;
                while (true)
                {
                    AsyncCompletedEventHandler comparand = completed;
                    AsyncCompletedEventHandler handler3 = comparand - value;
                    completed = Interlocked.CompareExchange<AsyncCompletedEventHandler>(ref this.Completed, handler3, comparand);
                    if (ReferenceEquals(completed, comparand))
                    {
                        return;
                    }
                }
            }
        }

        static ReportServiceTaskBase()
        {
            ReportServiceTaskBase<TResult>.DefaultUpdateStatusInterval = TimeSpan.FromMilliseconds(250.0);
        }

        public ReportServiceTaskBase(IReportServiceClient client)
        {
            Guard.ArgumentNotNull(client, "client");
            this.client = client;
        }

        private void createDocument_Completed(object sender, CreateDocumentCompletedEventArgs e)
        {
            ((CreateDocumentOperation) sender).Completed -= new EventHandler<CreateDocumentCompletedEventArgs>(this.createDocument_Completed);
            if (!this.HasErrorOrCancelled(e))
            {
                this.ProcessGeneratedReport(e.DocumentId);
            }
        }

        protected void CreateDocumentAsync(InstanceIdentity reportIdentity, ReportParameter[] parameters, object asyncState)
        {
            Guard.ArgumentNotNull(reportIdentity, "reportIdentity");
            this.UserState = asyncState;
            ReportBuildArgs buildArgs = new ReportBuildArgs();
            buildArgs.Parameters = parameters;
            CreateDocumentOperation operation = new CreateDocumentOperation(this.Client, reportIdentity, buildArgs, false, ReportServiceTaskBase<TResult>.DefaultUpdateStatusInterval);
            operation.Completed += new EventHandler<CreateDocumentCompletedEventArgs>(this.createDocument_Completed);
            operation.Start();
        }

        protected bool HasErrorOrCancelled(AsyncCompletedEventArgs args)
        {
            if ((args.Error == null) && !args.Cancelled)
            {
                return false;
            }
            this.RaiseCompleted(args.Error, args.Cancelled);
            return true;
        }

        protected abstract void ProcessGeneratedReport(DocumentId documentId);
        protected void RaiseCompleted(Exception error, bool cancelled)
        {
            if (this.Completed != null)
            {
                AsyncCompletedEventArgs e = new AsyncCompletedEventArgs(error, cancelled, this.UserState);
                this.Completed(this, e);
            }
        }

        protected object UserState { get; private set; }

        public IReportServiceClient Client =>
            this.client;

        public TResult Result { get; protected set; }
    }
}

