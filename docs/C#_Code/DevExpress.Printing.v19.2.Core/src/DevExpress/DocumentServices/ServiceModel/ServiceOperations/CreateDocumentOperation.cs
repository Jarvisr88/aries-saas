namespace DevExpress.DocumentServices.ServiceModel.ServiceOperations
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class CreateDocumentOperation : ReportServiceOperation
    {
        private readonly InstanceIdentity instanceIdentity;
        private readonly ReportBuildArgs buildArgs;
        private readonly bool shouldRequestReportInfo;

        public event EventHandler<CreateDocumentCompletedEventArgs> Completed;

        public event EventHandler<CreateDocumentReportParametersEventArgs> GetReportParameters;

        public event EventHandler<CreateDocumentProgressEventArgs> Progress;

        public event EventHandler<CreateDocumentStartedEventArgs> Started;

        public CreateDocumentOperation(IReportServiceClient client, InstanceIdentity identity, ReportBuildArgs buildArgs, bool shouldRequestReportInfo, TimeSpan statusUpdateInterval) : base(client, statusUpdateInterval, null)
        {
            Guard.ArgumentNotNull(identity, "identity");
            Guard.ArgumentNotNull(buildArgs, "buildArgs");
            this.instanceIdentity = identity;
            this.buildArgs = buildArgs;
            this.shouldRequestReportInfo = shouldRequestReportInfo;
        }

        private void client_GetReportParametersCompleted(object sender, ScalarOperationCompletedEventArgs<ReportParameterContainer> e)
        {
            if (base.IsSameInstanceId(e.UserState))
            {
                if (this.TryProcessError(e))
                {
                    this.OnCompleted(base.documentId, e.Error, e.Cancelled, e.UserState);
                }
                else
                {
                    bool flag1;
                    ReportParameterContainer result = e.Result;
                    this.OnGetReportParameters(result);
                    if (!result.ShouldRequestParameters)
                    {
                        flag1 = false;
                    }
                    else
                    {
                        Func<ReportParameter, bool> predicate = <>c.<>9__24_0;
                        if (<>c.<>9__24_0 == null)
                        {
                            Func<ReportParameter, bool> local1 = <>c.<>9__24_0;
                            predicate = <>c.<>9__24_0 = x => x.Visible;
                        }
                        flag1 = result.Parameters.Any<ReportParameter>(predicate);
                    }
                    result.ShouldRequestParameters = flag1;
                    if (result.ShouldRequestParameters)
                    {
                        this.OnCompleted(base.documentId, e.Error, e.Cancelled, e.UserState);
                    }
                    else
                    {
                        base.Client.StartBuildAsync(this.instanceIdentity, this.buildArgs, base.instanceId);
                    }
                }
            }
        }

        private void client_StartBuildCompleted(object sender, ScalarOperationCompletedEventArgs<DocumentId> e)
        {
            if (base.IsSameInstanceId(e.UserState))
            {
                if (this.TryProcessError(e))
                {
                    this.OnCompleted(null, e.Error, e.Cancelled, e.UserState);
                }
                else
                {
                    base.documentId = e.Result;
                    this.StartBuildCompleted();
                }
            }
        }

        protected void ClientGetBuildStatusCompleted(object sender, ScalarOperationCompletedEventArgs<BuildStatus> e)
        {
            if (base.IsSameInstanceId(e.UserState))
            {
                if (this.TryProcessError(e))
                {
                    this.OnCompleted(base.documentId, e.Error, e.Cancelled, e.UserState);
                }
                else
                {
                    this.RaiseProgress(e.Result.ProgressPosition, e.Result.PageCount);
                    if (e.Result.Status == TaskStatus.InProgress)
                    {
                        this.QueryBuildStatus();
                    }
                    else
                    {
                        Exception error = (e.Result.Status == TaskStatus.Fault) ? e.Result.Fault.ToException() : null;
                        this.OnCompleted(base.documentId, error, e.Cancelled, e.UserState);
                    }
                }
            }
        }

        protected void OnCompleted(DocumentId documentId, Exception error, bool cancelled, object userState)
        {
            this.UnsubscribeClientEvents();
            if (!base.Aborted && (this.Completed != null))
            {
                this.Completed(this, new CreateDocumentCompletedEventArgs(documentId, error, cancelled, userState));
            }
        }

        protected void OnGetReportParameters(ReportParameterContainer reportParameters)
        {
            if (this.GetReportParameters != null)
            {
                this.GetReportParameters(this, new CreateDocumentReportParametersEventArgs(reportParameters));
            }
        }

        private void QueryBuildStatus()
        {
            base.Delayer.Execute(() => base.Client.GetBuildStatusAsync(base.documentId, base.instanceId));
        }

        protected void RaiseProgress(int progressPosition, int pageCount)
        {
            if (this.Progress != null)
            {
                this.Progress(this, new CreateDocumentProgressEventArgs(progressPosition, pageCount));
            }
        }

        protected void RaiseStarted(DocumentId documentId)
        {
            if (this.Started != null)
            {
                this.Started(this, new CreateDocumentStartedEventArgs(documentId));
            }
        }

        public void SetParameters(IParameterContainer parameters)
        {
            Guard.ArgumentNotNull(parameters, "parameters");
            this.buildArgs.Parameters = parameters.ToParameterStubs();
        }

        public override void Start()
        {
            base.Client.GetReportParametersCompleted += new EventHandler<ScalarOperationCompletedEventArgs<ReportParameterContainer>>(this.client_GetReportParametersCompleted);
            base.Client.StartBuildCompleted += new EventHandler<ScalarOperationCompletedEventArgs<DocumentId>>(this.client_StartBuildCompleted);
            base.Client.GetBuildStatusCompleted += new EventHandler<ScalarOperationCompletedEventArgs<BuildStatus>>(this.ClientGetBuildStatusCompleted);
            if (base.documentId != null)
            {
                this.StartBuildCompleted();
            }
            else if (this.shouldRequestReportInfo)
            {
                base.Client.GetReportParametersAsync(this.instanceIdentity, base.instanceId);
            }
            else
            {
                base.Client.StartBuildAsync(this.instanceIdentity, this.buildArgs, base.instanceId);
            }
        }

        private void StartBuildCompleted()
        {
            this.RaiseStarted(base.documentId);
            base.RaiseCanStopChanged();
            this.QueryBuildStatus();
        }

        public override void Stop()
        {
            base.Client.StopBuildAsync(base.documentId, null);
        }

        protected override void UnsubscribeClientEvents()
        {
            base.Client.GetReportParametersCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<ReportParameterContainer>>(this.client_GetReportParametersCompleted);
            base.Client.StartBuildCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<DocumentId>>(this.client_StartBuildCompleted);
            base.Client.GetBuildStatusCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<BuildStatus>>(this.ClientGetBuildStatusCompleted);
        }

        public override bool CanStop =>
            base.documentId != null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CreateDocumentOperation.<>c <>9 = new CreateDocumentOperation.<>c();
            public static Func<ReportParameter, bool> <>9__24_0;

            internal bool <client_GetReportParametersCompleted>b__24_0(ReportParameter x) => 
                x.Visible;
        }
    }
}

