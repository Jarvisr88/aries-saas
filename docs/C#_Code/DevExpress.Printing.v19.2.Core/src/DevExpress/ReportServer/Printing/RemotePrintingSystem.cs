namespace DevExpress.ReportServer.Printing
{
    using DevExpress.DocumentServices.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.ReportServer.Printing.Services;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.DrillDown;
    using DevExpress.XtraReports.Parameters.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class RemotePrintingSystem : PrintingSystemBase
    {
        private IReportServiceClient client;
        private readonly IPageListService pageListService;
        private object padlock;
        private Timer pageTimer;
        private Dictionary<object, int[]> servicePageIndexes;

        internal event GetRemoteParametersCompletedEventHandler GetRemoteParametersCompleted;

        protected internal RemotePrintingSystem()
        {
            this.padlock = new object();
            this.servicePageIndexes = new Dictionary<object, int[]>();
            PageListService service1 = new PageListService(this);
            service1.DefaultPageSettings = base.PageSettings;
            this.pageListService = service1;
            this.pageListService.RequestPagesException += new ExceptionEventHandler(this.pageListService_RequestPagesException);
            base.AddService(typeof(IPageListService), this.pageListService);
            base.AddService(typeof(IUpdateDrillDownReportStrategy), new RemoteUpdateDrillDownReportStrategy());
            base.AddService(typeof(IEmptyPageFactory), new EmptyPageFactory());
            base.AddService(typeof(IPageOwnerProvider), new PageOwnerProvider(base.Pages));
            base.AddService(typeof(IBrickPagePairFactory), new BrickPagePairFactory(this.pageListService));
            base.AddService(typeof(IRemotePrintService), new RemotePrintService(this.pageListService, new Action<int[]>(this.InvalidatePages)));
            base.AddService(typeof(IRemoteExportService), new RemoteExportService(this));
        }

        public RemotePrintingSystem(IReportServiceClient client) : this()
        {
            this.client = client;
            base.AddService(typeof(IReportServiceClient), client);
        }

        protected override void AfterDrawPagesCore(object syncObj, int[] pageIndices)
        {
            base.AfterDrawPagesCore(syncObj, pageIndices);
            object padlock = this.padlock;
            lock (padlock)
            {
                this.servicePageIndexes[syncObj] = pageIndices;
                if (this.pageTimer != null)
                {
                    this.pageTimer.Dispose();
                }
                this.pageTimer = new Timer(new TimerCallback(this.OnTimerCallback), null, 500, -1);
            }
        }

        protected override PrintingDocument CreateDocument() => 
            new DevExpress.ReportServer.Printing.RemoteDocument(this);

        internal void CustomizeExportOptions(ExportOptions exportOptions, ExportOptionKind hiddenOptions)
        {
            base.ExportOptions.Assign(exportOptions);
            this.HideExportOptions(hiddenOptions);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.pageTimer != null)
                {
                    this.pageTimer.Dispose();
                    this.pageTimer = null;
                }
                this.pageListService.Dispose();
            }
            base.Dispose(disposing);
        }

        protected internal override void EnsureBrickOnPage(BrickPagePair pair, Action<BrickPagePair> onEnsured)
        {
            int[] pageIndexes = new int[] { pair.PageIndex };
            if (!this.pageListService.PagesShouldBeLoaded(pageIndexes))
            {
                onEnsured(pair);
            }
            else
            {
                int[] indexes = new int[] { pair.PageIndex };
                this.pageListService.Ensure(indexes, delegate (int[] indexes) {
                    onEnsured(pair);
                    this.InvalidatePages(indexes);
                });
            }
        }

        protected internal void EnsureClient(IReportServiceClient client)
        {
            this.client = client;
            if (this.GetService<IReportServiceClient>() != null)
            {
                base.Document.ClearContent();
                base.RemoveService(typeof(IReportServiceClient));
            }
            base.AddService(typeof(IReportServiceClient), client);
        }

        private void HideExportOptions(ExportOptionKind hiddenOptions)
        {
            long num = 0L;
            long num2 = (long) hiddenOptions;
            foreach (ExportOptionKind kind in (ExportOptionKind[]) Enum.GetValues(typeof(ExportOptionKind)))
            {
                num = (long) kind;
                if ((num & num2) != 0)
                {
                    base.ExportOptions.SetOptionVisibility(kind, false);
                }
            }
        }

        private void InvalidatePages(int[] indexes)
        {
            base.OnDocumentChanged(EventArgs.Empty);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal void OnReportParametersApproved(IList<ClientParameter> parameters, ILookUpValuesProvider lookUpValuesProvider)
        {
            if (this.GetRemoteParametersCompleted != null)
            {
                this.GetRemoteParametersCompleted(this, new GetRemoteParametersCompletedEventArgs(parameters, lookUpValuesProvider));
            }
        }

        private void OnTimerCallback(object state)
        {
            object padlock = this.padlock;
            lock (padlock)
            {
                List<int> list = new List<int>();
                foreach (int[] numArray in this.servicePageIndexes.Values)
                {
                    list.AddRange(numArray);
                }
                this.pageListService.Ensure(list.ToArray(), new Action<int[]>(this.InvalidatePages));
                this.servicePageIndexes.Clear();
            }
        }

        private void pageListService_RequestPagesException(object sender, ExceptionEventArgs args)
        {
            args.Handled = true;
            if (base.RaiseCreateDocumentException(args.Exception))
            {
                throw args.Exception;
            }
        }

        [DXHelpExclude(true), EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void RequestRemoteDocument(InstanceIdentity reportId, IParameterContainer parameters)
        {
            ((DevExpress.ReportServer.Printing.RemoteDocument) base.Document).RequestDocumentAsync(reportId, parameters);
        }

        [DXHelpExclude(true), EditorBrowsable(EditorBrowsableState.Never)]
        public virtual void SubmitParameters()
        {
            ((DevExpress.ReportServer.Printing.RemoteDocument) base.Document).SubmitParameters();
        }

        internal DevExpress.ReportServer.Printing.RemoteDocument RemoteDocument =>
            base.Document as DevExpress.ReportServer.Printing.RemoteDocument;
    }
}

