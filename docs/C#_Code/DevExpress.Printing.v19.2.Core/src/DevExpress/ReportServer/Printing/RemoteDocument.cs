namespace DevExpress.ReportServer.Printing
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.DocumentServices.ServiceModel.Native;
    using DevExpress.DocumentServices.ServiceModel.ServiceOperations;
    using DevExpress.Printing.Core.ReportServer.Services;
    using DevExpress.ReportServer.Printing.Services;
    using DevExpress.ReportServer.ServiceModel.Native.RemoteOperations;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.DrillDown;
    using DevExpress.XtraReports;
    using DevExpress.XtraReports.Parameters;
    using DevExpress.XtraReports.Parameters.Native;
    using DevExpress.XtraReports.UI;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class RemoteDocument : PrintingDocument
    {
        private bool shouldRequestParameters;
        private DocumentId documentId;
        private readonly IServiceContainer serviceContainer;
        private AvailableExportModes availableExportModes;
        private InstanceIdentity identity;
        private CreateDocumentOperation operation;
        private IParameterContainer parameterContainer;

        public RemoteDocument(PrintingSystemBase ps) : base(ps, null)
        {
            this.serviceContainer = ps;
        }

        protected internal override DocumentBand AddReportContainer()
        {
            throw new NotImplementedException();
        }

        protected internal override void AfterBuild()
        {
            base.OnContentChanged();
            base.state = DocumentState.Created;
            base.IsModified = false;
            base.ps.ProgressReflector.MaximizeRange();
            base.PrintingSystem.OnAfterBuildPages(EventArgs.Empty);
        }

        private void ApproveParameters(ClientParameterContainer reportParameters, bool shouldRequestParameters)
        {
            IReportPrintTool printTool = this.GetPrintTool();
            if (printTool != null)
            {
                Func<Parameter, bool> predicate = <>c.<>9__30_0;
                if (<>c.<>9__30_0 == null)
                {
                    Func<Parameter, bool> local1 = <>c.<>9__30_0;
                    predicate = <>c.<>9__30_0 = param => param.Visible;
                }
                printTool.ApproveParameters(reportParameters.OriginalParameters.Where<Parameter>(predicate).ToArray<Parameter>(), shouldRequestParameters);
            }
            this.operation.SetParameters(this.parameterContainer);
            RemoteLookUpValuesProvider service = new RemoteLookUpValuesProvider(reportParameters, this.identity, () => this.Client);
            this.RemotePrintingSystem.ReplaceService<ILookUpValuesProvider>(service);
            this.RemotePrintingSystem.OnReportParametersApproved(reportParameters.Cast<ClientParameter>().ToList<ClientParameter>(), service);
        }

        private void AssignDrillDownKeys(Dictionary<string, bool> drillDownKeys)
        {
            IDrillDownServiceBase drillDownService = this.GetDrillDownService();
            if (drillDownService != null)
            {
                drillDownService.Keys.Clear();
                drillDownKeys.ForEach<KeyValuePair<string, bool>>(x => drillDownService.Keys[DrillDownKey.Parse(x.Key)] = x.Value);
            }
        }

        private void AssignSortingKeys(Dictionary<string, List<SortingFieldInfoContract>> sortingKeys)
        {
            RemoteInteractionService interactionService = this.GetInteractonService();
            if (interactionService != null)
            {
                interactionService.Reset();
                sortingKeys.ForEach<KeyValuePair<string, List<SortingFieldInfoContract>>>(x => interactionService.BandSorting[x.Key] = x.Value);
            }
        }

        protected internal override void BeginReport(DocumentBand docBand, PointF offset)
        {
            throw new NotImplementedException();
        }

        protected override void Clear()
        {
            if (this.operation != null)
            {
                this.UnsubscribeOperationEvents();
                this.operation.Abort();
                this.operation = null;
            }
            base.Clear();
            if (this.documentId != null)
            {
                this.Client.ClearDocumentAsync(this.documentId, null);
            }
            this.documentId = null;
        }

        private void Client_GetDocumentDataCompleted(object sender, ScalarOperationCompletedEventArgs<DocumentData> e)
        {
            IReportServiceClient client = sender as IReportServiceClient;
            if (client != null)
            {
                client.GetDocumentDataCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<DocumentData>>(this.Client_GetDocumentDataCompleted);
            }
            if (e.Error != null)
            {
                this.AfterBuild();
                if (base.PrintingSystem.RaiseCreateDocumentException(e.Error))
                {
                    throw e.Error;
                }
            }
            else
            {
                ExportOptions instance = new ExportOptions();
                DocumentData result = e.Result;
                Helper.DeserializeExportOptions(instance, result.ExportOptions);
                this.RemotePrintingSystem.CustomizeExportOptions(instance, result.HiddenOptions);
                XtraPageSettingsBase base2 = new XtraPageSettingsBase(base.PrintingSystem);
                Helper.DeserializePageSettings(base2, result.SerializedPageData);
                this.RemotePrintingSystem.PageSettings.Assign(base2.Margins, base2.PaperKind, base2.Landscape);
                this.availableExportModes = e.Result.AvailableExportModes;
                if (e.Result.DrillDownKeys != null)
                {
                    this.AssignDrillDownKeys(e.Result.DrillDownKeys);
                }
                if (e.Result.BandSortingKeys != null)
                {
                    this.AssignSortingKeys(e.Result.BandSortingKeys);
                }
                this.RestoreBookmarkNodes(result.DocumentMap, base.RootBookmark);
                this.AfterBuild();
            }
        }

        private void Client_GetLookUpValuesCompleted(object sender, ScalarOperationCompletedEventArgs<ParameterLookUpValues[]> e)
        {
            ((IReportServiceClient) sender).GetLookUpValuesCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<ParameterLookUpValues[]>>(this.Client_GetLookUpValuesCompleted);
            foreach (ParameterLookUpValues values in e.Result)
            {
                ClientParameter parameter = this.parameterContainer.Cast<ClientParameter>().FirstOrDefault<ClientParameter>(p => string.Equals(values.Path, p.Path));
                StaticListLookUpSettings lookUpSettings = parameter.OriginalParameter.LookUpSettings as StaticListLookUpSettings;
                if (lookUpSettings != null)
                {
                    lookUpSettings.LookUpValues.Clear();
                    lookUpSettings.LookUpValues.AddRange(values.LookUpValues);
                }
            }
            this.ApproveParameters((ClientParameterContainer) this.parameterContainer, this.shouldRequestParameters);
        }

        private void CopyParameters(DefaultValueParameterContainer source, ClientParameterContainer dest)
        {
            Exception exception;
            if (!source.CopyTo(dest, out exception))
            {
                throw exception;
            }
        }

        protected virtual CreateDocumentOperation CreateOperation(IReportServiceClient client, InstanceIdentity instanceId, ReportBuildArgs args, bool shouldRequestReportInformation, TimeSpan statusUpdateInterval) => 
            new CreateDocumentOperation(client, instanceId, args, shouldRequestReportInformation, statusUpdateInterval);

        protected override PageList CreatePageList()
        {
            IPageListService pageListService = base.ps.GetService<IPageListService>();
            return new RemotePageList(this, new RemoteInnerPageList(pageListService));
        }

        public override void Dispose()
        {
            this.UnsubscribeOperationEvents();
            if (this.Client != null)
            {
                this.Client.GetDocumentDataCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<DocumentData>>(this.Client_GetDocumentDataCompleted);
            }
            this.RemotePages.Dispose();
            base.Dispose();
        }

        protected internal override void EndReport()
        {
            throw new NotImplementedException();
        }

        protected override object[] GetAvailableExportModes(Type exportModeType) => 
            (this.availableExportModes == null) ? base.GetAvailableExportModes(exportModeType) : this.availableExportModes.GetExportModesByType(exportModeType);

        private Dictionary<string, bool> GetDrillDownKeys()
        {
            IDrillDownServiceBase drillDownService = this.GetDrillDownService();
            if (drillDownService == null)
            {
                return null;
            }
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            drillDownService.Keys.ForEach<KeyValuePair<DrillDownKey, bool>>(delegate (KeyValuePair<DrillDownKey, bool> x) {
                result[x.Key.ToString()] = x.Value;
            });
            return result;
        }

        private IDrillDownServiceBase GetDrillDownService()
        {
            IReport service = base.ps.GetService<IReport>();
            return ((service != null) ? service.GetService<IDrillDownServiceBase>() : null);
        }

        private RemoteInteractionService GetInteractonService()
        {
            IReport service = base.ps.GetService<IReport>();
            return (((service != null) ? ((RemoteInteractionService) service.GetService<IInteractionService>()) : null) as RemoteInteractionService);
        }

        private IReportPrintTool GetPrintTool()
        {
            IReport service = base.ps.GetService<IReport>();
            return service?.PrintTool;
        }

        private Dictionary<string, List<SortingFieldInfoContract>> GetSortingFieldInfos()
        {
            RemoteInteractionService interactonService = this.GetInteractonService();
            if (interactonService == null)
            {
                return null;
            }
            Dictionary<string, List<SortingFieldInfoContract>> result = new Dictionary<string, List<SortingFieldInfoContract>>();
            interactonService.BandSorting.ForEach<KeyValuePair<string, List<SortingFieldInfoContract>>>(delegate (KeyValuePair<string, List<SortingFieldInfoContract>> x) {
                result[x.Key.ToString()] = x.Value;
            });
            return result;
        }

        protected internal override void InsertPageBreak(float pos)
        {
            throw new NotImplementedException();
        }

        protected internal override void InsertPageBreak(float pos, CustomPageData nextPageData)
        {
            throw new NotImplementedException();
        }

        private void operation_Completed(object sender, CreateDocumentCompletedEventArgs e)
        {
            this.UnsubscribeOperationEvents();
            if (e.Error != null)
            {
                this.AfterBuild();
                if (base.PrintingSystem.RaiseCreateDocumentException(e.Error))
                {
                    throw e.Error;
                }
            }
            else if (this.documentId == null)
            {
                this.AfterBuild();
            }
            else
            {
                this.operation = null;
                this.Client.GetDocumentDataCompleted += new EventHandler<ScalarOperationCompletedEventArgs<DocumentData>>(this.Client_GetDocumentDataCompleted);
                this.availableExportModes = null;
                this.Client.GetDocumentDataAsync(this.documentId, e.UserState);
            }
        }

        private void operation_GetReportParameters(object sender, CreateDocumentReportParametersEventArgs e)
        {
            ClientParameterContainer dest = new ClientParameterContainer(e.ReportParameters);
            if (this.parameterContainer is DefaultValueParameterContainer)
            {
                try
                {
                    this.CopyParameters((DefaultValueParameterContainer) this.parameterContainer, dest);
                }
                catch (Exception exception)
                {
                    if (base.ps.RaiseCreateDocumentException(exception))
                    {
                        throw;
                    }
                    return;
                }
            }
            this.parameterContainer = dest;
            e.ReportParameters.Parameters = dest.ToParameterStubs();
            Func<ReportParameter, bool> predicate = <>c.<>9__29_0;
            if (<>c.<>9__29_0 == null)
            {
                Func<ReportParameter, bool> local1 = <>c.<>9__29_0;
                predicate = <>c.<>9__29_0 = item => item.IsFilteredLookUpSettings;
            }
            Func<ReportParameter, string> selector = <>c.<>9__29_1;
            if (<>c.<>9__29_1 == null)
            {
                Func<ReportParameter, string> local2 = <>c.<>9__29_1;
                selector = <>c.<>9__29_1 = item => item.Path;
            }
            string[] requiredParameterPaths = e.ReportParameters.Parameters.Where<ReportParameter>(predicate).Select<ReportParameter, string>(selector).ToArray<string>();
            if (requiredParameterPaths.Length != 0)
            {
                this.Client.GetLookUpValuesCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<ParameterLookUpValues[]>>(this.Client_GetLookUpValuesCompleted);
                this.Client.GetLookUpValuesCompleted += new EventHandler<ScalarOperationCompletedEventArgs<ParameterLookUpValues[]>>(this.Client_GetLookUpValuesCompleted);
                this.shouldRequestParameters = e.ReportParameters.ShouldRequestParameters;
                this.Client.GetLookUpValuesAsync(this.identity, e.ReportParameters.Parameters, requiredParameterPaths, null);
            }
            else
            {
                this.ApproveParameters(dest, e.ReportParameters.ShouldRequestParameters);
            }
        }

        private void operation_Progress(object sender, CreateDocumentProgressEventArgs e)
        {
            this.RemotePages.SetCount(e.PageCount);
            base.ps.ProgressReflector.SetPosition(e.ProgressPosition);
        }

        private void operation_Started(object sender, CreateDocumentStartedEventArgs e)
        {
            this.documentId = e.DocumentId;
            this.serviceContainer.RemoveService(typeof(RemoteOperationFactory));
            this.serviceContainer.AddService(typeof(RemoteOperationFactory), new RemoteOperationFactory(this.Client, this.documentId, Helper.DefaultStatusUpdateInterval));
        }

        public void RequestDocumentAsync(InstanceIdentity reportId, IParameterContainer defaultValueParameters)
        {
            this.Clear();
            this.identity = reportId;
            base.state = DocumentState.Creating;
            this.parameterContainer = defaultValueParameters;
            this.Start(defaultValueParameters is DefaultValueParameterContainer);
        }

        private void RestoreBookmarkNodes(DocumentMapTreeViewNode rootNode, BookmarkNode rootBookmarkNode)
        {
            if ((rootNode != null) && (rootNode.Nodes.Count > 0))
            {
                foreach (DocumentMapTreeViewNode node in rootNode.Nodes)
                {
                    int[] brickIndexes = BrickPagePairHelper.ParseIndices(DocumentMapTreeViewNodeHelper.GetBrickIndicesByTag(node.AssociatedElementTag));
                    BrickPagePair bpPair = this.BrickPagePairFactory.CreateBrickPagePair(brickIndexes, node.PageIndex);
                    BookmarkNode item = new BookmarkNode(node.Text, bpPair);
                    rootBookmarkNode.Nodes.Add(item);
                    this.RestoreBookmarkNodes(node, item);
                }
            }
        }

        public override void ShowFromNewPage(Brick brick)
        {
            throw new NotImplementedException();
        }

        private void Start(bool shouldRequestReportInformation)
        {
            ReportBuildArgs args = Helper.CreateReportBuildArgs(this.parameterContainer, null, null, null);
            args.DrillDownKeys = this.GetDrillDownKeys();
            args.BandSorting = this.GetSortingFieldInfos();
            this.operation = this.CreateOperation(this.Client, this.identity, args, shouldRequestReportInformation, Helper.DefaultStatusUpdateInterval);
            this.operation.Progress += new EventHandler<CreateDocumentProgressEventArgs>(this.operation_Progress);
            this.operation.Completed += new EventHandler<CreateDocumentCompletedEventArgs>(this.operation_Completed);
            this.operation.GetReportParameters += new EventHandler<CreateDocumentReportParametersEventArgs>(this.operation_GetReportParameters);
            this.operation.Started += new EventHandler<CreateDocumentStartedEventArgs>(this.operation_Started);
            base.ps.OnBeforeBuildPages(EventArgs.Empty);
            this.operation.Start();
        }

        protected internal override void StopPageBuilding()
        {
            if ((this.operation != null) && this.operation.CanStop)
            {
                this.operation.Stop();
            }
        }

        internal void SubmitParameters()
        {
            this.Clear();
            base.state = DocumentState.Creating;
            this.Start(false);
        }

        private void UnsubscribeOperationEvents()
        {
            if (this.operation != null)
            {
                this.operation.Progress -= new EventHandler<CreateDocumentProgressEventArgs>(this.operation_Progress);
                this.operation.Completed -= new EventHandler<CreateDocumentCompletedEventArgs>(this.operation_Completed);
                this.operation.Started -= new EventHandler<CreateDocumentStartedEventArgs>(this.operation_Started);
                this.operation.GetReportParameters -= new EventHandler<CreateDocumentReportParametersEventArgs>(this.operation_GetReportParameters);
            }
        }

        private RemotePageList RemotePages =>
            (RemotePageList) base.Pages;

        public override int PageCount =>
            this.RemotePages.Count;

        private IReportServiceClient Client =>
            this.serviceContainer.GetService<IReportServiceClient>();

        private DevExpress.ReportServer.Printing.RemotePrintingSystem RemotePrintingSystem =>
            base.ps as DevExpress.ReportServer.Printing.RemotePrintingSystem;

        private IBrickPagePairFactory BrickPagePairFactory =>
            this.serviceContainer.GetService<IBrickPagePairFactory>();

        public override bool IsEmpty =>
            (base.Pages == null) || (base.Pages.Count == 0);

        public override bool CanPerformContinuousExport =>
            !this.IsEmpty && !base.IsModified;

        internal IParameterContainer ParameterContainer =>
            this.parameterContainer;

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal bool IsDocumentDisposed =>
            base.IsDisposed;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RemoteDocument.<>c <>9 = new RemoteDocument.<>c();
            public static Func<ReportParameter, bool> <>9__29_0;
            public static Func<ReportParameter, string> <>9__29_1;
            public static Func<Parameter, bool> <>9__30_0;

            internal bool <ApproveParameters>b__30_0(Parameter param) => 
                param.Visible;

            internal bool <operation_GetReportParameters>b__29_0(ReportParameter item) => 
                item.IsFilteredLookUpSettings;

            internal string <operation_GetReportParameters>b__29_1(ReportParameter item) => 
                item.Path;
        }
    }
}

