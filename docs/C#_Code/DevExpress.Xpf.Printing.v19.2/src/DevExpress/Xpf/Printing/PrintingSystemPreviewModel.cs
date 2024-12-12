namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Printing.Exports;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Preview;
    using System;
    using System.IO;
    using System.Linq.Expressions;
    using System.Printing;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Markup;
    using System.Windows.Threading;

    public abstract class PrintingSystemPreviewModel : DocumentPreviewModelBase, IDisposable
    {
        internal const int DefaultPageCacheSize = 20;
        private IPageSettingsConfiguratorService pageSettingsConfiguratorService;
        private IPrintService printService;
        private IExportSendService exportSendService;
        private IHighlightingService highlightingService;
        private PagesCache pagesCache;
        private bool isCreating;
        private bool isExporting;
        private bool disposed;
        private bool firstPageDisplayed;
        private bool isReflectorPositionChanged;
        private bool isSaving;

        public PrintingSystemPreviewModel() : this(new PageSettingsConfiguratorService(), new PrintService(), new DevExpress.Xpf.Printing.ExportSendService(), new DevExpress.Xpf.Printing.HighlightingService())
        {
        }

        internal PrintingSystemPreviewModel(IPageSettingsConfiguratorService pageSettingsConfiguratorService, IPrintService printService, IExportSendService exportSendService, IHighlightingService highlightingService)
        {
            this.pageSettingsConfiguratorService = pageSettingsConfiguratorService;
            this.printService = printService;
            this.exportSendService = exportSendService;
            this.highlightingService = highlightingService;
            this.pagesCache = new PagesCache(20);
        }

        private void cancellationService_StateChanged(object sender, EventArgs e)
        {
            ICancellationService serv = (ICancellationService) sender;
            this.IsCreating = serv.CanBeCanceled() || this.PrintingSystem.Document.IsCreating;
        }

        protected override bool CanExport(object parameter) => 
            this.BuildPagesComplete && (!this.IsExporting && !this.IsSaving);

        protected override bool CanOpen(object parameter) => 
            (this.PrintingSystem != null) && (!this.IsCreating && (!this.IsExporting && !this.IsSaving));

        protected override bool CanPageSetup(object parameter) => 
            this.BuildPagesComplete && (this.PrintingSystem.Document.CanChangePageSettings && (!this.IsExporting && !this.IsSaving));

        protected override bool CanPrint(object parameter) => 
            this.BuildPagesComplete && (!this.IsExporting && !this.IsSaving);

        protected override bool CanPrintDirect(object parameter) => 
            this.BuildPagesComplete && (!this.IsExporting && !this.IsSaving);

        protected override bool CanSave(object parameter) => 
            this.BuildPagesComplete && (!this.IsExporting && !this.IsSaving);

        protected override bool CanSend(object parameter) => 
            this.BuildPagesComplete && (!this.IsExporting && !this.IsSaving);

        protected override bool CanShowDocumentMap(object parameter) => 
            this.BuildPagesComplete && (this.PrintingSystem.Document.BookmarkNodes.Count != 0);

        protected override bool CanShowSearchPanel(object parameter) => 
            this.BuildPagesComplete && (!this.IsExporting && !this.IsSaving);

        protected override bool CanStop(object parameter) => 
            this.IsCreating;

        protected void ClearCache()
        {
            this.pagesCache.Clear();
            this.firstPageDisplayed = false;
        }

        protected abstract void CreateDocument(bool buildPagesInBackground);
        private void CreateDocumentIfEmpty(bool buildPagesInBackground)
        {
            if (this.PrintingSystem.Document.PageCount == 0)
            {
                this.CreateDocument(buildPagesInBackground);
            }
        }

        protected DocumentPaginator CreatePaginator() => 
            new DelegatePaginator(new Func<int, FrameworkElement>(this.VisualizePage), () => this.PageCount);

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing && (this.PrintingSystem != null))
                {
                    this.UnhookPrintingSystem();
                }
                this.disposed = true;
            }
        }

        protected override void Export(object parameter)
        {
            ExportFormat format = (parameter != null) ? ((ExportFormat) Enum.Parse(typeof(ExportFormat), parameter.ToString())) : ExportFormatConverter.ToExportFormat(this.PrintingSystem.ExportOptions.PrintPreview.DefaultExportFormat);
            this.IsExporting = true;
            try
            {
                this.exportSendService.Export(this.PrintingSystem, this.DialogService.GetParentWindow(), this.GetExportOptions(format), this.DialogService);
                this.PrintingSystem.ExportOptions.PrintPreview.DefaultExportFormat = ExportFormatConverter.ToExportCommand(format);
            }
            finally
            {
                this.IsExporting = false;
            }
        }

        private ExportOptionsBase GetExportOptions(ExportFormat format)
        {
            switch (format)
            {
                case ExportFormat.Pdf:
                    return this.PrintingSystem.ExportOptions.Options[typeof(PdfExportOptions)];

                case ExportFormat.Htm:
                    return this.PrintingSystem.ExportOptions.Options[typeof(HtmlExportOptions)];

                case ExportFormat.Mht:
                    return this.PrintingSystem.ExportOptions.Options[typeof(MhtExportOptions)];

                case ExportFormat.Rtf:
                    return this.PrintingSystem.ExportOptions.Options[typeof(RtfExportOptions)];

                case ExportFormat.Xls:
                    return this.PrintingSystem.ExportOptions.Options[typeof(XlsExportOptions)];

                case ExportFormat.Xlsx:
                    return this.PrintingSystem.ExportOptions.Options[typeof(XlsxExportOptions)];

                case ExportFormat.Csv:
                    return this.PrintingSystem.ExportOptions.Options[typeof(CsvExportOptions)];

                case ExportFormat.Txt:
                    return this.PrintingSystem.ExportOptions.Options[typeof(TextExportOptions)];

                case ExportFormat.Image:
                    return this.PrintingSystem.ExportOptions.Options[typeof(ImageExportOptions)];

                case ExportFormat.Xps:
                    return this.PrintingSystem.ExportOptions.Options[typeof(XpsExportOptions)];
            }
            throw new ArgumentException("format");
        }

        protected override FrameworkElement GetPageContent()
        {
            if (this.PageCount == 0)
            {
                return null;
            }
            FrameworkElement page = null;
            FrameworkElement element2 = this.pagesCache.GetPage(base.CurrentPageIndex);
            if (element2 != null)
            {
                page = element2;
            }
            else
            {
                try
                {
                    page = this.VisualizePage(base.CurrentPageIndex);
                    this.pagesCache.AddPage(page, base.CurrentPageIndex);
                    base.IsIncorrectPageContent = false;
                }
                catch (XamlParseException)
                {
                    base.IsIncorrectPageContent = true;
                }
            }
            return page;
        }

        protected virtual void HookPrintingSystem()
        {
            this.PrintingSystem.DocumentChanged += new EventHandler(this.PrintingSystem_DocumentChanged);
            this.PrintingSystem.BeforeBuildPages += new EventHandler(this.PrintingSystemBase_BeforeBuildPages);
            this.PrintingSystem.AfterBuildPages += new EventHandler(this.PrintingSystem_AfterBuildPages);
            this.PrintingSystem.ProgressReflector.PositionChanged += new EventHandler(this.ProgressReflector_PositionChanged);
            this.PrintingSystem.ReplaceService<XpsExportServiceBase>(new XpsExportService(this.CreatePaginator()));
            BackgroundServiceHelper.ReplaceBackgroundService(this.PrintingSystem);
            ICancellationService service = this.PrintingSystem.GetService<ICancellationService>();
            if (service != null)
            {
                service.StateChanged += new EventHandler(this.cancellationService_StateChanged);
            }
        }

        protected override bool IsHitTestResult(FrameworkElement element) => 
            VisualHelper.GetIsVisualBrickBorder(element);

        private bool IsProgressMarquee() => 
            (this.PrintingSystem.ProgressReflector.Ranges.Count != 0) && float.IsNaN(this.PrintingSystem.ProgressReflector.Ranges[0]);

        private bool IsProgressReflectorComplited() => 
            this.PrintingSystem.ProgressReflector.PositionCore == this.PrintingSystem.ProgressReflector.MaximumCore;

        protected override void OnCurrentPageIndexChanged()
        {
            this.UpdateCurrentPageContent();
        }

        protected void OnSourceChanged()
        {
            if (this.PrintingSystem != null)
            {
                this.HookPrintingSystem();
                if (this.PageCount > 0)
                {
                    base.CurrentPageIndex = 0;
                }
                if (!this.PrintingSystem.ExportOptions.Options.ContainsKey(typeof(XpsExportOptions)))
                {
                    this.PrintingSystem.ExportOptions.Options.Add(typeof(XpsExportOptions), new XpsExportOptions());
                }
                base.RaiseAllPropertiesChanged();
                base.RaiseAllCommandsCanExecuteChanged();
            }
        }

        protected void OnSourceChanging()
        {
            if (this.PrintingSystem != null)
            {
                this.UnhookPrintingSystem();
            }
            this.ClearCache();
        }

        protected override void Open(object parameter)
        {
            string caption = PreviewLocalizer.GetString(PreviewStringId.OpenFileDialog_Title);
            using (Stream stream = this.DialogService.ShowOpenFileDialog(caption, string.Format(PreviewLocalizer.GetString(PreviewStringId.OpenFileDialog_Filter), ".prnx")))
            {
                if (stream != null)
                {
                    try
                    {
                        this.PrintingSystem.LoadDocument(stream);
                    }
                    catch
                    {
                        this.DialogService.ShowError(PreviewLocalizer.GetString(PreviewStringId.Msg_CannotLoadDocument), PrintingLocalizer.GetString(PrintingStringId.Error));
                    }
                    finally
                    {
                        this.IsCreating = false;
                        this.ClearCache();
                        base.commands[PrintingSystemCommand.DocumentMap].RaiseCanExecuteChanged();
                        if (!this.CanShowDocumentMap(null))
                        {
                            this.IsDocumentMapVisible = false;
                        }
                        base.commands[PrintingSystemCommand.Parameters].RaiseCanExecuteChanged();
                        if (!this.CanShowParametersPanel(null))
                        {
                            this.IsParametersPanelVisible = false;
                        }
                        if (base.CurrentPageIndex == 0)
                        {
                            this.UpdateCurrentPageContent();
                        }
                        else
                        {
                            base.CurrentPageIndex = 0;
                        }
                    }
                }
            }
        }

        protected override void PageSetup(object parameter)
        {
            this.ShowPageSetupDialog(this.DialogService.GetParentWindow());
        }

        protected override void Print(object parameter)
        {
            this.PrintDialog();
        }

        public bool? PrintDialog()
        {
            this.CreateDocumentIfEmpty(false);
            return this.printService.PrintDialog(this.CreatePaginator(), PrintHelper.CollectPageData(this.PrintingSystem.Pages.ToArray()), this.PrintingSystem.Document.Name, true);
        }

        public void PrintDirect()
        {
            this.CreateDocumentIfEmpty(false);
            this.printService.PrintDirect(this.CreatePaginator(), PrintHelper.CollectPageData(this.PrintingSystem.Pages.ToArray()), this.PrintingSystem.Document.Name, true);
        }

        protected override void PrintDirect(object parameter)
        {
            if (parameter is string)
            {
                this.PrintDirect((string) parameter);
            }
            else if (parameter is PrintQueue)
            {
                this.PrintDirect((PrintQueue) parameter);
            }
            else
            {
                this.PrintDirect();
            }
        }

        public void PrintDirect(PrintQueue printQueue)
        {
            this.CreateDocumentIfEmpty(false);
            this.printService.PrintDirect(this.CreatePaginator(), PrintHelper.CollectPageData(this.PrintingSystem.Pages.ToArray()), this.PrintingSystem.Document.Name, printQueue, true);
        }

        public void PrintDirect(string printerName)
        {
            this.CreateDocumentIfEmpty(false);
            this.printService.PrintDirect(this.CreatePaginator(), PrintHelper.CollectPageData(this.PrintingSystem.Pages.ToArray()), this.PrintingSystem.Document.Name, printerName, true);
        }

        private void PrintingSystem_AfterBuildPages(object sender, EventArgs e)
        {
            this.IsCreating = false;
            if (this.PageCount != 0)
            {
                this.pagesCache.Clear();
                this.UpdateCurrentPageContent();
            }
        }

        private void PrintingSystem_DocumentChanged(object sender, EventArgs e)
        {
            if ((this.PageCount > 0) && !this.firstPageDisplayed)
            {
                this.firstPageDisplayed = true;
                base.CurrentPageIndex = 0;
                this.UpdateCurrentPageContent();
            }
            base.RaisePropertyChanged<int>(System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintingSystemPreviewModel)), (MethodInfo) methodof(PreviewModelBase.get_PageCount)), new ParameterExpression[0]));
            base.RaiseNavigationCommandsCanExecuteChanged();
        }

        private void PrintingSystemBase_BeforeBuildPages(object sender, EventArgs e)
        {
            this.ClearCache();
            this.IsCreating = true;
        }

        private void ProgressReflector_PositionChanged(object sender, EventArgs e)
        {
            ProgressReflector reflector = (ProgressReflector) sender;
            this.IsReflectorPositionChanged = true;
            if ((reflector.PositionCore == reflector.MaximumCore) && this.IsSaving)
            {
                this.IsSaving = false;
            }
            if (this.PrintingSystem.Document.State == DocumentState.Created)
            {
                TimeSpan timeout = new TimeSpan(0x186a0L);
                ThreadStart method = <>c.<>9__43_0;
                if (<>c.<>9__43_0 == null)
                {
                    ThreadStart local1 = <>c.<>9__43_0;
                    method = <>c.<>9__43_0 = delegate {
                    };
                }
                Dispatcher.CurrentDispatcher.Invoke(method, timeout, DispatcherPriority.Background, new object[0]);
            }
        }

        protected override void Save(object parameter)
        {
            string caption = PreviewLocalizer.GetString(PreviewStringId.SaveDlg_Title);
            using (Stream stream = this.DialogService.ShowSaveFileDialog(caption, string.Format("{0} (*{1})|*{1}", PreviewLocalizer.GetString(PreviewStringId.SaveDlg_FilterNativeFormat), ".prnx"), 0, this.PrintingSystem.ExportOptions.PrintPreview.DefaultDirectory, this.PrintingSystem.ExportOptions.PrintPreview.DefaultFileName))
            {
                if (stream != null)
                {
                    this.IsSaving = true;
                    this.PrintingSystem.SaveDocument(stream);
                }
            }
        }

        protected override void Send(object parameter)
        {
            ExportFormat format = (parameter != null) ? ((ExportFormat) Enum.Parse(typeof(ExportFormat), parameter.ToString())) : ExportFormatConverter.ToExportFormat(this.PrintingSystem.ExportOptions.PrintPreview.DefaultSendFormat);
            this.IsExporting = true;
            try
            {
                this.exportSendService.SendFileByEmail(this.PrintingSystem, new EmailSender(), this.DialogService.GetParentWindow(), this.GetExportOptions(format), this.DialogService);
                this.PrintingSystem.ExportOptions.PrintPreview.DefaultSendFormat = ExportFormatConverter.ToSendCommand(format);
            }
            finally
            {
                this.IsExporting = false;
            }
        }

        public bool? ShowPageSetupDialog(Window ownerWindow)
        {
            bool? nullable = this.pageSettingsConfiguratorService.Configure(this.PrintingSystem.PageSettings, ownerWindow);
            bool? nullable2 = nullable;
            bool flag = true;
            if ((nullable2.GetValueOrDefault() == flag) ? (nullable2 != null) : false)
            {
                this.ClearCache();
            }
            return nullable;
        }

        protected override void Stop(object parameter)
        {
            this.PrintingSystem.Document.StopPageBuilding();
        }

        protected void ToggleDocumentMap()
        {
            base.RaisePropertyChanged<DocumentMapTreeViewNode>(System.Linq.Expressions.Expression.Lambda<Func<DocumentMapTreeViewNode>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintingSystemPreviewModel)), (MethodInfo) methodof(DocumentPreviewModelBase.get_DocumentMapRootNode)), new ParameterExpression[0]));
            base.commands[PrintingSystemCommand.DocumentMap].RaiseCanExecuteChanged();
            this.IsDocumentMapVisible = this.CanShowDocumentMap(null);
        }

        protected virtual void UnhookPrintingSystem()
        {
            this.PrintingSystem.DocumentChanged -= new EventHandler(this.PrintingSystem_DocumentChanged);
            this.PrintingSystem.BeforeBuildPages -= new EventHandler(this.PrintingSystemBase_BeforeBuildPages);
            this.PrintingSystem.AfterBuildPages -= new EventHandler(this.PrintingSystem_AfterBuildPages);
            this.PrintingSystem.ProgressReflector.PositionChanged -= new EventHandler(this.ProgressReflector_PositionChanged);
            this.PrintingSystem.RemoveService(typeof(IBackgroundService));
            ICancellationService service = this.PrintingSystem.GetService<ICancellationService>();
            if (service != null)
            {
                service.StateChanged -= new EventHandler(this.cancellationService_StateChanged);
            }
        }

        protected void UpdateCurrentPageContent()
        {
            base.RaisePropertyChanged<FrameworkElement>(System.Linq.Expressions.Expression.Lambda<Func<FrameworkElement>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintingSystemPreviewModel)), (MethodInfo) methodof(PreviewModelBase.get_PageContent)), new ParameterExpression[0]));
            base.RaisePropertyChanged<double>(System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintingSystemPreviewModel)), (MethodInfo) methodof(PreviewModelBase.get_PageViewWidth)), new ParameterExpression[0]));
            base.RaisePropertyChanged<double>(System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintingSystemPreviewModel)), (MethodInfo) methodof(PreviewModelBase.get_PageViewHeight)), new ParameterExpression[0]));
        }

        protected abstract FrameworkElement VisualizePage(int pageIndex);

        public int PageCacheSize
        {
            get => 
                this.pagesCache.CacheSize;
            set
            {
                if (value != this.pagesCache.CacheSize)
                {
                    this.pagesCache = new PagesCache(value);
                }
            }
        }

        protected internal abstract PrintingSystemBase PrintingSystem { get; }

        protected internal bool IsSaving
        {
            get => 
                this.isSaving;
            set
            {
                if (this.isSaving != value)
                {
                    this.isSaving = value;
                    base.RaiseAllCommandsCanExecuteChanged();
                }
            }
        }

        protected bool IsExporting
        {
            get => 
                this.isExporting;
            set
            {
                this.isExporting = value;
                base.RaiseAllCommandsCanExecuteChanged();
                base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintingSystemPreviewModel)), (MethodInfo) methodof(DocumentPreviewModelBase.get_ProgressVisibility)), new ParameterExpression[0]));
            }
        }

        public override bool IsCreating
        {
            get => 
                this.isCreating;
            protected set
            {
                this.isCreating = value;
                base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintingSystemPreviewModel)), (MethodInfo) methodof(DocumentPreviewModelBase.get_ProgressMarqueeVisibility)), new ParameterExpression[0]));
                base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintingSystemPreviewModel)), (MethodInfo) methodof(PreviewModelBase.get_IsCreating)), new ParameterExpression[0]));
                base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintingSystemPreviewModel)), (MethodInfo) methodof(DocumentPreviewModelBase.get_ProgressVisibility)), new ParameterExpression[0]));
                base.RaisePropertyChanged<int>(System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintingSystemPreviewModel)), (MethodInfo) methodof(DocumentPreviewModelBase.get_ProgressValue)), new ParameterExpression[0]));
                base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintingSystemPreviewModel)), (MethodInfo) methodof(DocumentPreviewModelBase.get_IsEmptyDocument)), new ParameterExpression[0]));
                if (this.isCreating)
                {
                    base.IsDocumentMapToggledDuringDocumentCreating = true;
                }
                this.ToggleDocumentMap();
                if (!this.isCreating)
                {
                    base.IsDocumentMapToggledDuringDocumentCreating = false;
                }
                base.RaiseAllCommandsCanExecuteChanged();
            }
        }

        protected bool BuildPagesComplete =>
            (this.PrintingSystem != null) && (!this.IsCreating && (this.PrintingSystem.Document.Pages.Count != 0));

        protected bool IsReflectorPositionChanged
        {
            get => 
                this.isReflectorPositionChanged;
            set
            {
                this.isReflectorPositionChanged = value;
                base.RaisePropertyChanged<int>(System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintingSystemPreviewModel)), (MethodInfo) methodof(DocumentPreviewModelBase.get_ProgressValue)), new ParameterExpression[0]));
                base.RaisePropertyChanged<int>(System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintingSystemPreviewModel)), (MethodInfo) methodof(DocumentPreviewModelBase.get_ProgressMaximum)), new ParameterExpression[0]));
            }
        }

        internal IExportSendService ExportSendService
        {
            get => 
                this.exportSendService;
            set => 
                this.exportSendService = value;
        }

        public override bool IsScaleVisible =>
            true;

        public override bool IsSearchVisible =>
            true;

        public override int PageCount =>
            (this.PrintingSystem != null) ? this.PrintingSystem.Document.Pages.Count : 0;

        public override bool ProgressMarqueeVisibility =>
            this.IsCreating && this.IsProgressMarquee();

        public override bool ProgressVisibility =>
            (this.IsProgressReflectorComplited() || (this.ProgressMarqueeVisibility || !this.IsCreating)) ? this.IsExporting : true;

        public override int ProgressMaximum =>
            (this.PrintingSystem != null) ? this.PrintingSystem.ProgressReflector.MaximumCore : 0;

        public override int ProgressValue =>
            (this.PrintingSystem != null) ? this.PrintingSystem.ProgressReflector.PositionCore : 0;

        public override DocumentMapTreeViewNode DocumentMapRootNode =>
            (this.PrintingSystem != null) ? DocumentMapTreeViewNodeBuilder.Build(this.PrintingSystem.Document) : null;

        protected override IHighlightingService HighlightingService =>
            this.highlightingService;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PrintingSystemPreviewModel.<>c <>9 = new PrintingSystemPreviewModel.<>c();
            public static ThreadStart <>9__43_0;

            internal void <ProgressReflector_PositionChanged>b__43_0()
            {
            }
        }
    }
}

