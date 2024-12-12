namespace DevExpress.Xpf.Printing
{
    using DevExpress.Data.Utils.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel.Client;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.DocumentServices.ServiceModel.Native;
    using DevExpress.DocumentServices.ServiceModel.ServiceOperations;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Printing.Parameters;
    using DevExpress.Xpf.Printing.Parameters.Models;
    using DevExpress.Xpf.Printing.Parameters.Models.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraReports.Parameters.Native;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.ServiceModel;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    [Obsolete("The DocumentPreview and corresponding xxxPreviewModel classes and interfaces are now obsolete. Use the DocumentPreviewControl class to display a print preview. More information: https://www.devexpress.com/Support/WhatsNew/DXperience/files/16.1.2.bc.xml#BC3444")]
    public class ReportServicePreviewModel : DocumentPreviewModelBase
    {
        private static readonly DevExpress.DocumentServices.ServiceModel.DataContracts.PageCompatibility PageCompatibility = DevExpress.DocumentServices.ServiceModel.DataContracts.PageCompatibility.WPF;
        private TimeSpan statusUpdateInterval;
        private IReportServiceClient client;
        private bool isCreating;
        private bool isReportInformationRequested;
        private DevExpress.DocumentServices.ServiceModel.DataContracts.InstanceIdentity instanceIdentity;
        private IParameterContainer parameters;
        private FrameworkElement pageContent;
        private int pageCount;
        private DevExpress.DocumentServices.ServiceModel.DataContracts.DocumentId documentId;
        private string documentName;
        private string[] printData;
        private ReportServiceOperation serviceOperation;
        private BinaryWriter exportWriter;
        private bool canRefresh;
        private DocumentMapTreeViewNode documentMapRootNode;
        private readonly DevExpress.Xpf.Printing.HighlightingService highlightingService;
        private ExportOptions exportOptions;
        private AvailableExportModes exportModes;
        private Action<ExportId, byte[], Exception> completeSaveExportedDocumentDelegate;
        private bool shouldRequestParameters;
        private readonly DevExpress.Xpf.Printing.Parameters.Models.ParametersModel parametersModel;
        private readonly IPageSettingsConfiguratorService pageSetupConfigurator;
        private bool progressVisibility;
        private int progressValue;
        private PrintingSystemBase printingSystem;
        private readonly ServiceClientCreator<IReportServiceClient, IReportServiceClientFactory> clientCreator;
        private IWatermarkService watermarkService;
        private XpfWatermark xpfWatermark;
        private bool isWatermarkChanged;
        private Func<bool> startSaveExportedDocumentDelegate;
        private bool autoShowParametersPanel;
        private bool canChangePageSettings;

        public event EventHandler<FaultEventArgs> CreateDocumentError;

        public event EventHandler<FaultEventArgs> ExportError;

        public event EventHandler<SimpleFaultEventArgs> GetPageError;

        public event EventHandler<ParametersMergingErrorEventArgs> ParametersMergingError;

        public event EventHandler<FaultEventArgs> PrintError;

        public ReportServicePreviewModel() : this(null)
        {
        }

        public ReportServicePreviewModel(string serviceUri)
        {
            this.statusUpdateInterval = Helper.DefaultStatusUpdateInterval;
            this.parameters = new DefaultValueParameterContainer();
            this.highlightingService = new DevExpress.Xpf.Printing.HighlightingService();
            this.pageSetupConfigurator = new PageSettingsConfiguratorService();
            Func<EndpointAddress, IReportServiceClientFactory> createFactory = <>c.<>9__93_0;
            if (<>c.<>9__93_0 == null)
            {
                Func<EndpointAddress, IReportServiceClientFactory> local1 = <>c.<>9__93_0;
                createFactory = <>c.<>9__93_0 = address => new ReportServiceClientFactory(address);
            }
            this.clientCreator = new ServiceClientCreator<IReportServiceClient, IReportServiceClientFactory>(createFactory);
            this.watermarkService = new DevExpress.Xpf.Printing.Native.WatermarkService();
            this.xpfWatermark = new XpfWatermark();
            this.autoShowParametersPanel = true;
            this.ServiceUri = serviceUri;
            this.parametersModel = DevExpress.Xpf.Printing.Parameters.Models.ParametersModel.CreateParametersModel();
            this.ParametersModel.Submit += new EventHandler(this.OnSubmit);
            this.watermarkService.EditCompleted += new EventHandler<WatermarkServiceEventArgs>(this.watermarkService_EditCompleted);
        }

        protected override void BeginExport(ExportFormat format)
        {
            this.startSaveExportedDocumentDelegate = () => this.StartExportShowSaveFileDialog(format);
        }

        protected override bool CanExport(object parameter) => 
            (this.serviceOperation == null) && base.CanExport(parameter);

        protected override bool CanOpen(object parameter) => 
            false;

        protected override bool CanPageSetup(object parameter) => 
            (this.serviceOperation == null) && (this.canChangePageSettings && (this.PageCount > 0));

        protected override bool CanPrint(object parameter) => 
            (this.serviceOperation == null) && (this.PageCount > 0);

        protected override bool CanPrintDirect(object parameter) => 
            (this.serviceOperation == null) && (this.PageCount > 0);

        protected override bool CanSave(object parameter) => 
            false;

        protected override bool CanScale(object parameter) => 
            false;

        protected override bool CanSend(object parameter) => 
            false;

        protected override bool CanSetWatermark(object parameter) => 
            (this.documentId != null) && ((this.printingSystem != null) && ((this.serviceOperation == null) && !this.IsCreating));

        protected override bool CanShowDocumentMap(object parameter) => 
            (this.serviceOperation == null) && (this.DocumentMapRootNode != null);

        protected override bool CanShowParametersPanel(object parameter) => 
            this.ParametersModel.Parameters.Count != 0;

        protected override bool CanShowSearchPanel(object parameter) => 
            !this.IsCreating && ((this.serviceOperation == null) && (this.PageCount > 0));

        protected override bool CanStop(object parameter) => 
            (this.serviceOperation != null) && this.serviceOperation.CanStop;

        public void Clear()
        {
            this.Clear(false);
        }

        private void Clear(bool enableRefresh)
        {
            if (this.exportWriter != null)
            {
                this.exportWriter.Dispose();
                this.exportWriter = null;
            }
            if (this.serviceOperation != null)
            {
                this.serviceOperation.Abort();
                this.serviceOperation = null;
            }
            this.ClearDocumentOnServer();
            if (this.client != null)
            {
                this.client.GetPagesCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<byte[]>>(this.client_GetPagesCompleted);
                this.client.GetDocumentDataCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<DocumentData>>(this.client_GetDocumentDataCompleted);
                this.client.CloseAsync();
                this.client = null;
            }
            this.documentMapRootNode = null;
            this.IsDocumentMapVisible = false;
            this.canRefresh = enableRefresh;
            this.DocumentExportOptions = null;
            this.exportModes = null;
            base.IsLoading = false;
            this.SetProgressVisibility(false);
            this.IsCreating = false;
            this.printData = null;
            this.ClearParameters();
            base.SetCurrentPageIndex(-1);
            this.SetPageCount(0);
            base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ReportServicePreviewModel)), (MethodInfo) methodof(DocumentPreviewModelBase.get_IsEmptyDocument)), new ParameterExpression[0]));
            base.RaiseAllCommandsCanExecuteChanged();
        }

        protected virtual void ClearDocumentOnServer()
        {
            if (this.documentId != null)
            {
                this.client.ClearDocumentAsync(this.documentId, null);
                this.documentId = null;
            }
        }

        private void ClearParameters()
        {
            if (!this.isReportInformationRequested)
            {
                ClientParameterContainer parameters = this.parameters as ClientParameterContainer;
                if (parameters != null)
                {
                    DefaultValueParameterContainer container2 = new DefaultValueParameterContainer();
                    container2.CopyFrom(parameters);
                    this.parameters = container2;
                }
            }
        }

        private void client_GetDocumentDataCompleted(object sender, ScalarOperationCompletedEventArgs<DocumentData> e)
        {
            (sender as IReportServiceClient).GetDocumentDataCompleted -= new EventHandler<ScalarOperationCompletedEventArgs<DocumentData>>(this.client_GetDocumentDataCompleted);
            if (e.Error != null)
            {
                base.RaiseOperationError(e.Error, this.CreateDocumentError);
            }
            else
            {
                this.serviceOperation = null;
                if (this.PageCount != 0)
                {
                    DocumentData result = e.Result;
                    this.canChangePageSettings = result.CanChangePageSettings;
                    this.documentName = result.Name;
                    this.documentMapRootNode = result.DocumentMap;
                    base.RaisePropertyChanged<DocumentMapTreeViewNode>(System.Linq.Expressions.Expression.Lambda<Func<DocumentMapTreeViewNode>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ReportServicePreviewModel)), (MethodInfo) methodof(DocumentPreviewModelBase.get_DocumentMapRootNode)), new ParameterExpression[0]));
                    base.commands[PrintingSystemCommand.DocumentMap].RaiseCanExecuteChanged();
                    this.IsDocumentMapVisible = true;
                    this.CreatePrintingSystemIfNeeded();
                    this.UnsubscribePageSettingsChangedEvent();
                    Helper.DeserializePageSettings(this.DocumentPageSettings, e.Result.SerializedPageData);
                    this.SubscribePageSettingsChangedEvent();
                    this.exportModes = result.AvailableExportModes;
                    Helper.DeserializeExportOptions(this.DocumentExportOptions, result.ExportOptions);
                    this.DocumentExportOptions.HiddenOptions.Clear();
                    this.DocumentExportOptions.HiddenOptions.AddRange(result.HiddenOptions.FlagsToList());
                    base.DefaultExportFormat = ExportFormatConverter.ToExportFormat(this.DocumentExportOptions.PrintPreview.DefaultExportFormat);
                    this.RestoreWatermark(result.SerializedWatermark);
                }
                this.SetProgressVisibility(false);
                base.RaiseAllCommandsCanExecuteChanged();
            }
        }

        private void client_GetPagesCompleted(object sender, ScalarOperationCompletedEventArgs<byte[]> e)
        {
            if (((int) e.UserState) == base.CurrentPageIndex)
            {
                base.IsLoading = false;
                if (e.Error != null)
                {
                    base.IsIncorrectPageContent = true;
                    if (this.GetPageError != null)
                    {
                        this.GetPageError(this, new SimpleFaultEventArgs(e.Error));
                    }
                }
                else
                {
                    try
                    {
                        string[] source = Helper.DeserializePages(e.Result);
                        this.SetPageContent(DevExpress.Xpf.Printing.Native.XamlReaderHelper.Load(source.First<string>()));
                        base.IsIncorrectPageContent = false;
                    }
                    catch (XamlParseException)
                    {
                        base.IsIncorrectPageContent = true;
                    }
                }
            }
        }

        protected virtual CreateDocumentOperation ConstructCreateDocumentOperation(ReportBuildArgs buildArgs) => 
            new CreateDocumentOperation(this.client, this.InstanceIdentity, buildArgs, !this.isReportInformationRequested, this.statusUpdateInterval);

        private void ContinuePrint(bool isDirectPrinting)
        {
            DocumentPrinter printer = new DocumentPrinter();
            ReadonlyPageData[] pageData = new ReadonlyPageData[] { new ReadonlyPageData(this.DocumentPageSettings.Data) };
            if (isDirectPrinting)
            {
                printer.PrintDirect(new XamlDocumentPaginator(this.printData), pageData, this.documentName, true);
            }
            else
            {
                printer.PrintDialog(new XamlDocumentPaginator(this.printData), pageData, this.documentName, true);
            }
        }

        private void CopyParameters(DefaultValueParameterContainer source, ClientParameterContainer dest)
        {
            Exception error = null;
            if (!source.CopyTo(dest, out error) && (this.ParametersMergingError != null))
            {
                this.ParametersMergingError(this, new ParametersMergingErrorEventArgs(error));
            }
        }

        private IReportServiceClient CreateClient() => 
            this.clientCreator.Create();

        public void CreateDocument()
        {
            this.CreateDocumentInternal(null);
        }

        public void CreateDocument(DefaultValueParameterContainer parameters)
        {
            this.parameters = parameters;
            this.CreateDocumentInternal(null);
        }

        private void CreateDocumentCore(CreateDocumentOperation operation)
        {
            operation.GetReportParameters += new EventHandler<CreateDocumentReportParametersEventArgs>(this.createDocumentOperation_GetReportParameters);
            operation.Started += new EventHandler<CreateDocumentStartedEventArgs>(this.createDocumentOperation_Started);
            operation.Progress += new EventHandler<CreateDocumentProgressEventArgs>(this.createDocumentOperation_Progress);
            operation.Completed += new EventHandler<CreateDocumentCompletedEventArgs>(this.createDocumentOperation_Completed);
            operation.CanStopChanged += new EventHandler(this.createDocumentOperation_CanStopChanged);
            this.serviceOperation = operation;
            this.serviceOperation.Start();
            this.IsCreating = true;
            base.IsLoading = true;
            base.RaiseAllCommandsCanExecuteChanged();
        }

        protected void CreateDocumentInternal(object customArgs)
        {
            this.ValidateInstanceIdentity();
            this.Clear();
            this.client = this.CreateClient();
            this.client.GetPagesCompleted += new EventHandler<ScalarOperationCompletedEventArgs<byte[]>>(this.client_GetPagesCompleted);
            XpfWatermark watermark = this.isWatermarkChanged ? this.xpfWatermark : null;
            if (this.parameters is ClientParameterContainer)
            {
                Action<ParameterModel> action = <>c.<>9__96_0;
                if (<>c.<>9__96_0 == null)
                {
                    Action<ParameterModel> local1 = <>c.<>9__96_0;
                    action = <>c.<>9__96_0 = x => x.UpdateParameter(UpdateAction.Submit);
                }
                this.ParametersModel.Parameters.ForEach<ParameterModel>(action);
                ReportParameterContainer container1 = new ReportParameterContainer();
                container1.Parameters = this.ParametersModel.GetReportParameterStubs();
                ReportParameterContainer container = container1;
                this.parameters = new ClientParameterContainer(container);
            }
            ReportBuildArgs buildArgs = Helper.CreateReportBuildArgs(this.parameters, this.DocumentPageSettings, watermark, customArgs);
            this.CreateDocumentCore(this.ConstructCreateDocumentOperation(buildArgs));
        }

        private void createDocumentOperation_CanStopChanged(object sender, EventArgs e)
        {
            base.commands[PrintingSystemCommand.StopPageBuilding].RaiseCanExecuteChanged();
        }

        private void createDocumentOperation_Completed(object sender, CreateDocumentCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.Clear(true);
                base.RaiseOperationError(e.Error, this.CreateDocumentError);
            }
            else
            {
                this.IsCreating = false;
                base.IsLoading = false;
                if (this.documentId != null)
                {
                    this.RefreshPageOnReportBuildCompleted();
                    this.client.GetDocumentDataCompleted += new EventHandler<ScalarOperationCompletedEventArgs<DocumentData>>(this.client_GetDocumentDataCompleted);
                    this.client.GetDocumentDataAsync(this.documentId, null);
                    base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ReportServicePreviewModel)), (MethodInfo) methodof(DocumentPreviewModelBase.get_IsEmptyDocument)), new ParameterExpression[0]));
                }
            }
        }

        private void createDocumentOperation_GetReportParameters(object sender, CreateDocumentReportParametersEventArgs e)
        {
            CreateDocumentOperation operation = (CreateDocumentOperation) sender;
            ClientParameterContainer dest = new ClientParameterContainer(e.ReportParameters);
            if (this.parameters is DefaultValueParameterContainer)
            {
                this.CopyParameters((DefaultValueParameterContainer) this.parameters, dest);
                operation.SetParameters(dest);
            }
            this.parameters = dest;
            e.ReportParameters.Parameters = dest.ToParameterStubs();
            this.PrepareParametersModel();
            this.IsParametersPanelVisible = this.AutoShowParametersPanel && this.ParametersModel.HasVisibleParameters;
            base.RaisePropertyChanged<DevExpress.Xpf.Printing.Parameters.Models.ParametersModel>(System.Linq.Expressions.Expression.Lambda<Func<DevExpress.Xpf.Printing.Parameters.Models.ParametersModel>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ReportServicePreviewModel)), (MethodInfo) methodof(DocumentPreviewModelBase.get_ParametersModel)), new ParameterExpression[0]));
            base.commands[PrintingSystemCommand.Parameters].RaiseCanExecuteChanged();
            this.shouldRequestParameters = e.ReportParameters.ShouldRequestParameters;
            this.isReportInformationRequested = true;
        }

        private void createDocumentOperation_Progress(object sender, CreateDocumentProgressEventArgs e)
        {
            if ((this.pageCount != 0) || (e.PageCount <= 0))
            {
                this.SetPageCount(e.PageCount);
            }
            else
            {
                this.SetPageCount(e.PageCount);
                if (!this.IsValidPageIndex(base.CurrentPageIndex))
                {
                    base.CurrentPageIndex = 0;
                }
            }
            this.SetProgressValue(e.ProgressPosition);
            base.RaiseNavigationCommandsCanExecuteChanged();
        }

        private void createDocumentOperation_Started(object sender, CreateDocumentStartedEventArgs e)
        {
            this.documentId = e.DocumentId;
            this.StartShowingProgress();
        }

        private void CreatePrintingSystemIfNeeded()
        {
            if (this.printingSystem == null)
            {
                this.printingSystem = new PrintingSystemBase();
                this.SubscribePageSettingsChangedEvent();
            }
        }

        protected override void EndExport()
        {
            this.startSaveExportedDocumentDelegate = null;
            this.completeSaveExportedDocumentDelegate = new Action<ExportId, byte[], Exception>(this.ExportCompletedSaveData);
        }

        protected override void Export(ExportFormat format)
        {
            this.Export(format, null);
        }

        protected void Export(ExportFormat format, object customArgs)
        {
            this.ExportConfiguredCompleted(new ExportDocumentOperation(this.client, this.documentId, format, this.DocumentExportOptions, this.StatusUpdateInterval, customArgs));
        }

        private void ExportCompletedSaveData(ExportId exportId, byte[] data, Exception error)
        {
            try
            {
                if (error == null)
                {
                    this.exportWriter.Write(data);
                }
            }
            finally
            {
                this.exportWriter.Dispose();
                this.exportWriter = null;
            }
        }

        private void ExportConfiguredCompleted(ExportDocumentOperation exportDocumentOperation)
        {
            if ((this.startSaveExportedDocumentDelegate == null) || this.startSaveExportedDocumentDelegate())
            {
                exportDocumentOperation.Started += new EventHandler<ExportDocumentStartedEventArgs>(this.exportDocumentOperation_Started);
                exportDocumentOperation.Progress += new EventHandler<ExportDocumentProgressEventArgs>(this.exportDocumentOperation_Progress);
                exportDocumentOperation.Completed += new EventHandler<ExportDocumentCompletedEventArgs>(this.exportDocumentOperation_Completed);
                this.serviceOperation = exportDocumentOperation;
                this.serviceOperation.Start();
                this.RaiseStopPrintExportCommandsCanExecuteChanged();
            }
        }

        private void exportDocumentOperation_Completed(object sender, ExportDocumentCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                base.RaiseOperationError(e.Error, this.ExportError);
            }
            this.serviceOperation = null;
            this.SetProgressVisibility(false);
            this.RaiseStopPrintExportCommandsCanExecuteChanged();
            if (this.completeSaveExportedDocumentDelegate != null)
            {
                this.completeSaveExportedDocumentDelegate(e.OperationId, e.Data, e.Error);
                this.completeSaveExportedDocumentDelegate = null;
            }
        }

        private void exportDocumentOperation_Progress(object sender, ExportDocumentProgressEventArgs e)
        {
            this.SetProgressValue(e.ProgressPosition);
        }

        private void exportDocumentOperation_Started(object sender, ExportDocumentStartedEventArgs e)
        {
            this.StartShowingProgress();
        }

        protected override FrameworkElement GetPageContent() => 
            this.pageContent;

        protected override bool IsHitTestResult(FrameworkElement element) => 
            VisualHelper.GetIsVisualBrickBorder(element);

        private bool IsValidPageIndex(int pageIndex) => 
            (pageIndex >= 0) && (pageIndex < this.PageCount);

        protected override void OnCurrentPageIndexChanged()
        {
            if (this.documentId != null)
            {
                base.IsLoading = true;
                if (this.pageContent != null)
                {
                    Canvas canvas1 = new Canvas();
                    canvas1.Width = this.pageContent.Width;
                    canvas1.Height = this.pageContent.Height;
                    this.pageContent = canvas1;
                }
                base.RaisePropertyChanged<FrameworkElement>(System.Linq.Expressions.Expression.Lambda<Func<FrameworkElement>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ReportServicePreviewModel)), (MethodInfo) methodof(PreviewModelBase.get_PageContent)), new ParameterExpression[0]));
                int[] pageIndexes = new int[] { base.CurrentPageIndex };
                this.client.GetPagesAsync(this.documentId, pageIndexes, PageCompatibility, base.CurrentPageIndex);
            }
        }

        private void OnPrintingCancelled(object sender, EventArgs e)
        {
            if (this.serviceOperation != null)
            {
                this.serviceOperation.Stop();
            }
        }

        private void OnSubmit(object sender, EventArgs e)
        {
            this.CreateDocument();
        }

        protected override void Open(object parameter)
        {
            throw new NotImplementedException();
        }

        protected override void PageSetup(object parameter)
        {
            this.ShowPageSetupDialog();
        }

        private void PrepareParametersModel()
        {
            List<ParameterModel> parameters = ModelsCreator.CreateParameterModels(this.parameters);
            RemoteLookUpValuesProvider provider = new RemoteLookUpValuesProvider((ClientParameterContainer) this.parameters, this.InstanceIdentity, () => this.Client);
            this.ParametersModel.AssignParameters(parameters);
            this.ParametersModel.LookUpValuesProvider = provider;
        }

        protected override void Print(object parameter)
        {
            this.StartPrinting(parameter, false);
        }

        protected override void PrintDirect(object parameter)
        {
            this.StartPrinting(parameter, true);
        }

        private void printDocumentOperation_Completed(object sender, PrintDocumentCompletedEventArgs e)
        {
            PrintDocumentOperation operation = (PrintDocumentOperation) sender;
            operation.Completed -= new EventHandler<PrintDocumentCompletedEventArgs>(this.printDocumentOperation_Completed);
            this.SetProgressVisibility(false);
            if (e.Error != null)
            {
                base.RaiseOperationError(e.Error, this.PrintError);
            }
            else
            {
                this.printData = e.Pages;
                this.ContinuePrint(operation.IsDirectPrinting);
            }
            this.serviceOperation = null;
            this.RaiseStopPrintExportCommandsCanExecuteChanged();
        }

        private void printDocumentOperation_Progress(object sender, PrintDocumentProgressEventArgs e)
        {
            this.SetProgressValue(e.ProgressPosition);
        }

        private void printDocumentOperation_Started(object sender, PrintDocumentStartedEventArgs e)
        {
            this.StartShowingProgress();
        }

        private void printingSystem_PageSettingsChanged(object sender, EventArgs e)
        {
            if (this.serviceOperation == null)
            {
                this.CreateDocument();
            }
        }

        private void RaiseStopPrintExportCommandsCanExecuteChanged()
        {
            base.commands[PrintingSystemCommand.StopPageBuilding].RaiseCanExecuteChanged();
            base.commands[PrintingSystemCommand.Print].RaiseCanExecuteChanged();
            base.commands[PrintingSystemCommand.PageSetup].RaiseCanExecuteChanged();
            base.commands[PrintingSystemCommand.ExportFile].RaiseCanExecuteChanged();
            base.commands[PrintingSystemCommand.Watermark].RaiseCanExecuteChanged();
            base.commands[PrintingSystemCommand.PrintDirect].RaiseCanExecuteChanged();
        }

        protected virtual void RefreshPageOnReportBuildCompleted()
        {
            if (this.PageCount > 0)
            {
                int[] pageIndexes = new int[] { base.CurrentPageIndex };
                this.client.GetPagesAsync(this.documentId, pageIndexes, PageCompatibility, base.CurrentPageIndex);
            }
        }

        private void RestoreWatermark(byte[] watermark)
        {
            if ((this.xpfWatermark != null) && (watermark != null))
            {
                MemoryStream stream = new MemoryStream(watermark);
                try
                {
                    this.xpfWatermark.RestoreFromStream(stream);
                    this.isWatermarkChanged = false;
                }
                catch
                {
                    base.IsIncorrectPageContent = true;
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Dispose();
                    }
                }
            }
        }

        protected override void Save(object parameter)
        {
            throw new NotImplementedException();
        }

        protected override void Scale(object parameter)
        {
            throw new NotSupportedException("Scaling is not supported by this Preview Model");
        }

        protected override void Send(object parameter)
        {
            throw new NotImplementedException();
        }

        private void SetPageContent(FrameworkElement pageContent)
        {
            if (!ReferenceEquals(this.pageContent, pageContent))
            {
                this.pageContent = pageContent;
                base.RaisePropertyChanged<FrameworkElement>(System.Linq.Expressions.Expression.Lambda<Func<FrameworkElement>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ReportServicePreviewModel)), (MethodInfo) methodof(PreviewModelBase.get_PageContent)), new ParameterExpression[0]));
                base.RaisePropertyChanged<double>(System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ReportServicePreviewModel)), (MethodInfo) methodof(PreviewModelBase.get_PageViewWidth)), new ParameterExpression[0]));
                base.RaisePropertyChanged<double>(System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ReportServicePreviewModel)), (MethodInfo) methodof(PreviewModelBase.get_PageViewHeight)), new ParameterExpression[0]));
            }
        }

        private void SetPageCount(int pageCount)
        {
            this.pageCount = pageCount;
            base.RaisePropertyChanged<int>(System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ReportServicePreviewModel)), (MethodInfo) methodof(PreviewModelBase.get_PageCount)), new ParameterExpression[0]));
        }

        private void SetProgressValue(int value)
        {
            if (this.progressValue != value)
            {
                this.progressValue = value;
                base.RaisePropertyChanged<int>(System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ReportServicePreviewModel)), (MethodInfo) methodof(DocumentPreviewModelBase.get_ProgressValue)), new ParameterExpression[0]));
            }
        }

        private void SetProgressVisibility(bool value)
        {
            if (this.progressVisibility != value)
            {
                this.progressVisibility = value;
                base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ReportServicePreviewModel)), (MethodInfo) methodof(DocumentPreviewModelBase.get_ProgressVisibility)), new ParameterExpression[0]));
            }
        }

        protected override void SetWatermark(object parameter)
        {
            if ((this.printingSystem != null) && (this.printingSystem.PageSettings != null))
            {
                Window parentWindow = this.DialogService.GetParentWindow();
                this.watermarkService.Edit(parentWindow, this.printingSystem.PageSettings, this.PageCount, this.xpfWatermark);
            }
        }

        private void ShowPageSetupDialog()
        {
            this.pageSetupConfigurator.Configure(this.DocumentPageSettings, this.DialogService.GetParentWindow());
        }

        private bool StartExportShowSaveFileDialog(ExportFormat format)
        {
            Stream output = base.ShowSaveExportedFileDialog(format);
            bool flag = output != null;
            if (flag)
            {
                this.exportWriter = new BinaryWriter(output);
            }
            return flag;
        }

        protected void StartPrinting(object parameter, bool isDirectPrinting)
        {
            if (this.printData != null)
            {
                this.ContinuePrint(isDirectPrinting);
            }
            else
            {
                PrintDocumentOperation operation = new PrintDocumentOperation(this.client, this.documentId, PageCompatibility, this.documentName, this.statusUpdateInterval, isDirectPrinting);
                operation.Completed += new EventHandler<PrintDocumentCompletedEventArgs>(this.printDocumentOperation_Completed);
                operation.Started += new EventHandler<PrintDocumentStartedEventArgs>(this.printDocumentOperation_Started);
                operation.Progress += new EventHandler<PrintDocumentProgressEventArgs>(this.printDocumentOperation_Progress);
                this.serviceOperation = operation;
                this.serviceOperation.Start();
                this.RaiseStopPrintExportCommandsCanExecuteChanged();
            }
        }

        private void StartShowingProgress()
        {
            this.SetProgressValue(0);
            this.SetProgressVisibility(true);
        }

        protected override void Stop(object parameter)
        {
            this.serviceOperation.Stop();
        }

        private void SubscribePageSettingsChangedEvent()
        {
            this.printingSystem.PageSettingsChanged += new EventHandler(this.printingSystem_PageSettingsChanged);
        }

        private void UnsubscribePageSettingsChangedEvent()
        {
            this.printingSystem.PageSettingsChanged -= new EventHandler(this.printingSystem_PageSettingsChanged);
        }

        protected virtual void ValidateInstanceIdentity()
        {
            if (this.InstanceIdentity == null)
            {
                throw new InvalidOperationException("Can't create a document because InstanceIdentity or ReportName is not set.");
            }
            ReportNameIdentity instanceIdentity = this.InstanceIdentity as ReportNameIdentity;
            if ((instanceIdentity != null) && string.IsNullOrEmpty(instanceIdentity.Value))
            {
                throw new InvalidOperationException("Can't create a document because ReportName is not set.");
            }
        }

        private void watermarkService_EditCompleted(object sender, WatermarkServiceEventArgs e)
        {
            bool? isWatermarkAssigned = e.IsWatermarkAssigned;
            bool flag = true;
            if ((isWatermarkAssigned.GetValueOrDefault() == flag) ? (isWatermarkAssigned != null) : false)
            {
                this.xpfWatermark = e.Watermark;
                this.isWatermarkChanged = true;
                this.CreateDocument();
            }
        }

        private XtraPageSettingsBase DocumentPageSettings =>
            this.printingSystem?.PageSettings;

        protected DevExpress.DocumentServices.ServiceModel.DataContracts.DocumentId DocumentId
        {
            get => 
                this.documentId;
            set => 
                this.documentId = value;
        }

        protected IReportServiceClient Client =>
            this.client;

        internal bool CanCreateServiceClient =>
            this.clientCreator.CanCreateClient;

        public string ReportName
        {
            get
            {
                ReportNameIdentity instanceIdentity = this.instanceIdentity as ReportNameIdentity;
                return instanceIdentity?.Value;
            }
            set => 
                this.InstanceIdentity = new ReportNameIdentity(value);
        }

        public DevExpress.DocumentServices.ServiceModel.DataContracts.InstanceIdentity InstanceIdentity
        {
            get => 
                this.instanceIdentity;
            set
            {
                this.instanceIdentity = value;
                this.isReportInformationRequested = false;
                base.RaisePropertyChanged<string>(System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ReportServicePreviewModel)), (MethodInfo) methodof(ReportServicePreviewModel.get_ReportName)), new ParameterExpression[0]));
                base.RaisePropertyChanged<DevExpress.DocumentServices.ServiceModel.DataContracts.InstanceIdentity>(System.Linq.Expressions.Expression.Lambda<Func<DevExpress.DocumentServices.ServiceModel.DataContracts.InstanceIdentity>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ReportServicePreviewModel)), (MethodInfo) methodof(ReportServicePreviewModel.get_InstanceIdentity)), new ParameterExpression[0]));
            }
        }

        public string ServiceUri
        {
            get => 
                this.clientCreator.ServiceUri;
            set => 
                this.clientCreator.ServiceUri = value;
        }

        public IReportServiceClientFactory ServiceClientFactory
        {
            get => 
                this.clientCreator.Factory;
            set => 
                this.clientCreator.Factory = value;
        }

        [Obsolete("This property has become obsolete. To specify the default parameter values, call the CreateDocument(DefaultValueParameterCotnainer parameters) method overload.")]
        public IParameterContainer Parameters =>
            this.parameters;

        public TimeSpan StatusUpdateInterval
        {
            get => 
                this.statusUpdateInterval;
            set
            {
                if (this.statusUpdateInterval != value)
                {
                    this.statusUpdateInterval = value;
                    base.RaisePropertyChanged<TimeSpan>(System.Linq.Expressions.Expression.Lambda<Func<TimeSpan>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ReportServicePreviewModel)), (MethodInfo) methodof(ReportServicePreviewModel.get_StatusUpdateInterval)), new ParameterExpression[0]));
                }
            }
        }

        public bool AutoShowParametersPanel
        {
            get => 
                this.autoShowParametersPanel;
            set => 
                this.autoShowParametersPanel = value;
        }

        public override DevExpress.Xpf.Printing.Parameters.Models.ParametersModel ParametersModel =>
            this.parametersModel;

        internal bool ShouldRequestParameters =>
            this.shouldRequestParameters;

        internal IWatermarkService WatermarkService
        {
            get => 
                this.watermarkService;
            set => 
                this.watermarkService = value;
        }

        public override bool IsSetWatermarkVisible =>
            true;

        public override bool IsOpenButtonVisible =>
            false;

        public override bool IsSaveButtonVisible =>
            false;

        public override bool IsSendVisible =>
            false;

        public override bool IsCreating
        {
            get => 
                this.isCreating;
            protected set
            {
                if (this.isCreating != value)
                {
                    this.isCreating = value;
                    base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ReportServicePreviewModel)), (MethodInfo) methodof(PreviewModelBase.get_IsCreating)), new ParameterExpression[0]));
                }
            }
        }

        public override bool IsEmptyDocument =>
            (this.documentId != null) && (this.PageCount == 0);

        protected override ExportOptions DocumentExportOptions
        {
            get
            {
                this.exportOptions ??= new ExportOptionsContainer();
                return this.exportOptions;
            }
            set
            {
                this.exportOptions = value;
                base.commands[PrintingSystemCommand.ExportFile].RaiseCanExecuteChanged();
            }
        }

        protected override AvailableExportModes DocumentExportModes =>
            this.exportModes;

        protected override List<ExportOptionKind> DocumentHiddenOptions =>
            this.exportOptions.HiddenOptions;

        public override int PageCount =>
            this.pageCount;

        public override bool ProgressVisibility =>
            this.progressVisibility;

        public override int ProgressMaximum =>
            100;

        public override int ProgressValue =>
            this.progressValue;

        public override bool ProgressMarqueeVisibility =>
            false;

        public override DocumentMapTreeViewNode DocumentMapRootNode =>
            this.documentMapRootNode;

        protected override IHighlightingService HighlightingService =>
            this.highlightingService;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ReportServicePreviewModel.<>c <>9 = new ReportServicePreviewModel.<>c();
            public static Func<EndpointAddress, IReportServiceClientFactory> <>9__93_0;
            public static Action<ParameterModel> <>9__96_0;

            internal IReportServiceClientFactory <.ctor>b__93_0(EndpointAddress address) => 
                new ReportServiceClientFactory(address);

            internal void <CreateDocumentInternal>b__96_0(ParameterModel x)
            {
                x.UpdateParameter(UpdateAction.Submit);
            }
        }
    }
}

