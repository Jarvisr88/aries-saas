namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Printing;
    using DevExpress.Printing.Native;
    using DevExpress.Printing.Native.PrintEditor;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Printing.PreviewControl;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interop;

    public class PrintOptions : BindableBase, IPrintForm, IDataErrorInfo
    {
        private System.Drawing.Printing.PrintDocument printDocument;
        private readonly System.Drawing.Printing.PrinterSettings printerSettings;
        private readonly PrinterItemContainer printerItemContainer;
        private PrintEditorController controller;
        private readonly ICommand preferencesCommand;
        private readonly Locker paperSourceLocker;
        private readonly ObservableCollection<PrinterItem> availablePrinters;
        private PrinterItem selectedPrinter;
        private string paperSource;
        private readonly IEnumerable<PageMarginInfo> data;
        private bool landscape;
        private PaperSize selectedPaperSize;
        private PageMarginInfo selectedPageMargin;
        private readonly Locker pageSettingsLocker;
        private string customPageRange;
        private bool canDuplex;
        private System.Drawing.Printing.Duplex duplex;

        public PrintOptions() : this(PrintOptionsHelper.CreatePrinterSettings())
        {
        }

        public PrintOptions(System.Drawing.Printing.PrinterSettings printerSettings)
        {
            this.printerItemContainer = new PrinterItemContainer();
            this.paperSourceLocker = new Locker();
            this.availablePrinters = new ObservableCollection<PrinterItem>();
            PageMarginInfo[] infoArray1 = new PageMarginInfo[] { new PageMarginInfo(PreviewStringId.RibbonPreview_GalleryItem_PageMarginsNormal_Caption.GetString(), new Margins(100, 100, 100, 100)), new PageMarginInfo(PreviewStringId.RibbonPreview_GalleryItem_PageMarginsNarrow_Caption.GetString(), new Margins(50, 50, 50, 50)), new PageMarginInfo(PreviewStringId.RibbonPreview_GalleryItem_PageMarginsModerate_Caption.GetString(), new Margins(0x4b, 100, 0x4b, 100)), new PageMarginInfo(PreviewStringId.RibbonPreview_GalleryItem_PageMarginsWide_Caption.GetString(), new Margins(200, 200, 200, 200)) };
            this.data = infoArray1;
            this.pageSettingsLocker = new Locker();
            this.printerSettings = printerSettings;
            System.Drawing.Printing.PrintDocument document1 = new System.Drawing.Printing.PrintDocument();
            document1.PrinterSettings = printerSettings;
            this.printDocument = document1;
            this.controller = new PrintEditorController(this);
            this.controller.LoadForm(this.printerItemContainer);
            this.preferencesCommand = DelegateCommandFactory.Create(new Action(this.ShowPreferences), new Func<bool>(this.CanShowPreferences));
        }

        private void AssignPageDataIfNeeded()
        {
            if (this.ShouldUpdatePageSettings && !this.pageSettingsLocker.IsLocked)
            {
                PageData data = this.DocumentModel.PageSettings.Data;
                System.Drawing.Size pageSize = new System.Drawing.Size(this.SelectedPaperSize.Width, this.SelectedPaperSize.Height);
                PageData val = CreatePageData(this.SelectedPageMargins.Margins, data.MinMargins, this.SelectedPaperSize.Kind, pageSize, this.Landscape);
                this.DocumentModel.PageSettings.Assign(val);
            }
        }

        protected bool CanShowPageSetup()
        {
            Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> evaluator = <>c.<>9__90_0;
            if (<>c.<>9__90_0 == null)
            {
                Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> local1 = <>c.<>9__90_0;
                evaluator = <>c.<>9__90_0 = x => x.IsCreated && x.CanChangePageSettings;
            }
            return this.DocumentModel.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(evaluator, (<>c.<>9__90_1 ??= () => false));
        }

        private bool CanShowPreferences()
        {
            if (this.printDocument == null)
            {
                return false;
            }
            Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> evaluator = <>c.<>9__58_0;
            if (<>c.<>9__58_0 == null)
            {
                Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> local1 = <>c.<>9__58_0;
                evaluator = <>c.<>9__58_0 = x => x.IsCreated;
            }
            return this.DocumentModel.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(evaluator, (<>c.<>9__58_1 ??= () => false));
        }

        private static PageData CreatePageData(Margins margins, Margins minMargins, PaperKind paperKind, System.Drawing.Size pageSize, bool landscape) => 
            new PageData(margins, minMargins, paperKind, pageSize, landscape);

        void IPrintForm.AddPrinterItem(PrinterItem item)
        {
            if (!this.availablePrinters.Contains(item))
            {
                this.availablePrinters.Add(item);
            }
        }

        void IPrintForm.SetPrintRange(System.Drawing.Printing.PrintRange printRange)
        {
            this.PrintRange = printRange;
        }

        void IPrintForm.SetSelectedPrinter(string printerName)
        {
            this.SetSelectedPrinter(printerName);
        }

        private bool GetActualCanEditPaperSource()
        {
            Func<IEnumerable<string>, int> evaluator = <>c.<>9__49_0;
            if (<>c.<>9__49_0 == null)
            {
                Func<IEnumerable<string>, int> local1 = <>c.<>9__49_0;
                evaluator = <>c.<>9__49_0 = x => x.Count<string>();
            }
            int num = this.AvailablePaperSources.Return<IEnumerable<string>, int>(evaluator, <>c.<>9__49_1 ??= () => 0);
            return ((this.PaperSource != null) ? (num > 1) : (num > 0));
        }

        private void HookDocument()
        {
            this.DocumentModel.StartDocumentCreation += new EventHandler(this.OnStartDocumentCreation);
            this.DocumentModel.DocumentCreated += new EventHandler(this.OnDocumentCreated);
        }

        private void Initialize()
        {
            if (this.AllowEditOptions)
            {
                this.printDocument = PrintOptionsHelper.CreatePrintDocument(this.DocumentModel.PrintingSystem, this.printerSettings);
                this.UpdatePageSettings();
                this.controller = new PrintEditorController(this);
                this.controller.LoadForm(this.printerItemContainer);
            }
            this.HookDocument();
            base.RaisePropertiesChanged(new string[0]);
        }

        protected void OnCopiesChanged()
        {
            this.PrinterSettings.Copies = (this.Copies > 0) ? this.Copies : ((short) 1);
        }

        protected void OnCustomPageRangeChanged()
        {
            this.PopulatePageNumbers();
        }

        private void OnDocumentCreated(object sender, EventArgs e)
        {
            PageScope scope = new PageScope(1, this.DocumentModel.PrintingSystem.PageCount);
            this.printerSettings.MaximumPage = this.DocumentModel.PrintingSystem.PageCount;
            this.printerSettings.FromPage = scope.FromPage;
            this.printerSettings.ToPage = scope.ToPage;
            this.CustomPageRange = scope.PageRange;
            this.printDocument = PrintOptionsHelper.CreatePrintDocument(this.DocumentModel.PrintingSystem, this.PrinterSettings);
            this.controller.LoadForm(this.printerItemContainer);
            this.UpdatePageSettings();
            base.RaisePropertiesChanged(new string[0]);
        }

        protected void OnLandscapeChanged()
        {
            this.PrintDocument.DefaultPageSettings.Landscape = this.Landscape;
            this.AssignPageDataIfNeeded();
        }

        protected void OnPrintRangeChanged()
        {
            if (this.printDocument != null)
            {
                this.PopulatePageNumbers();
                this.AllowCustomPageRange = this.PrintRange == System.Drawing.Printing.PrintRange.SomePages;
                ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PrintOptions), "x");
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                this.RaisePropertyChanged<PrintOptions, string>(System.Linq.Expressions.Expression.Lambda<Func<PrintOptions, string>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PrintOptions.get_CustomPageRange)), parameters));
            }
        }

        private void OnSelectedPageMarginsChanged()
        {
            this.PrintDocument.DefaultPageSettings.Margins = this.SelectedPageMargins.Margins;
            this.AssignPageDataIfNeeded();
        }

        protected void OnSelectedPaperSizeChanged()
        {
            if (this.SelectedPaperSize != null)
            {
                this.PrintDocument.DefaultPageSettings.PaperSize = this.SelectedPaperSize;
                this.AssignPageDataIfNeeded();
            }
        }

        protected void OnSelectedPrinterChanged()
        {
            short copies;
            if (this.PrinterIsValid)
            {
                this.PrinterSettings.PrinterName = this.SelectedPrinter.FullName;
            }
            this.UpdatePaperSources();
            this.UpdateDuplex();
            if (this.DocumentModel != null)
            {
                this.UpdatePageSettings();
            }
            if (this.Copies <= this.MaxCopies)
            {
                copies = this.Copies;
            }
            else
            {
                copies = this.Copies = Convert.ToInt16(this.MaxCopies);
            }
            this.Copies = copies;
            base.RaisePropertiesChanged(new string[0]);
        }

        private void OnStartDocumentCreation(object sender, EventArgs e)
        {
            base.RaisePropertiesChanged(new string[0]);
        }

        private void PopulatePageNumbers()
        {
            if (this.PrintDocument != null)
            {
                IPrintForm form = this;
                System.Drawing.Printing.PrintRange printRange = this.PrintRange;
                if (printRange == System.Drawing.Printing.PrintRange.AllPages)
                {
                    form.PageRangeText = new PageScope(1, this.DocumentModel.PrintingSystem.Pages.Count).PageRange;
                }
                else if (printRange == System.Drawing.Printing.PrintRange.SomePages)
                {
                    form.PageRangeText = this.CustomPageRange;
                }
                else if (printRange == System.Drawing.Printing.PrintRange.CurrentPage)
                {
                    form.PageRangeText = $"{this.CurrentPageNumber}";
                }
            }
        }

        internal void SavePrinterSettings()
        {
            this.controller.AssignPrinterSettings();
        }

        private void SetCopies(short value)
        {
            if (this.AllowCopies)
            {
                short num = ((value <= 0) || (value > this.MaxCopies)) ? this.Copies : value;
                this.PrinterSettings.Copies = num;
            }
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PrintOptions), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            this.RaisePropertyChanged<PrintOptions, short>(System.Linq.Expressions.Expression.Lambda<Func<PrintOptions, short>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PrintOptions.get_Copies)), parameters));
        }

        internal void SetDocumentModel(DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel documentModel)
        {
            this.UnhookDocument();
            this.DocumentModel = documentModel;
            if (this.AllowEditOptions)
            {
                this.Initialize();
            }
            if (documentModel != null)
            {
                this.HookDocument();
            }
        }

        private void SetSelectedPrinter(string printerName)
        {
            this.SelectedPrinter = this.AvailablePrinters.Single<PrinterItem>(x => x.FullName == printerName);
        }

        public void ShowPageSetup()
        {
            new PageSettingsConfiguratorService().Configure(this.DocumentModel.PrintingSystem.PageSettings, ActiveWindowFinderBase<ActiveWindowFinder>.Instance.GetActiveWindow(null));
        }

        private void ShowPreferences()
        {
            try
            {
                Func<HwndSource, IntPtr> selector = <>c.<>9__57_0;
                if (<>c.<>9__57_0 == null)
                {
                    Func<HwndSource, IntPtr> local1 = <>c.<>9__57_0;
                    selector = <>c.<>9__57_0 = x => x.Handle;
                }
                IntPtr hwnd = PresentationSource.CurrentSources.OfType<HwndSource>().Select<HwndSource, IntPtr>(selector).LastOrDefault<IntPtr>();
                this.controller.ShowPrinterPreferences(hwnd);
                this.UpdatePaperSources();
            }
            catch
            {
            }
        }

        private void UnhookDocument()
        {
            this.printDocument = null;
            this.DocumentModel.Do<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel>(delegate (DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel x) {
                x.StartDocumentCreation -= new EventHandler(this.OnStartDocumentCreation);
                x.DocumentCreated -= new EventHandler(this.OnDocumentCreated);
            });
        }

        private void UpdateDuplex()
        {
            if (this.PrinterSettings.CanDuplex)
            {
                this.CanDuplex = true;
                DevExpress.Printing.Native.PrintHelper.GetDuplexAsync(this.PrinterSettings).ContinueWith<System.Drawing.Printing.Duplex>(delegate (Task<System.Drawing.Printing.Duplex> t) {
                    System.Drawing.Printing.Duplex duplex;
                    this.Duplex = duplex = t.Result;
                    return duplex;
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                this.CanDuplex = false;
                this.Duplex = System.Drawing.Printing.Duplex.Simplex;
            }
        }

        private void UpdatePageSettings()
        {
            this.pageSettingsLocker.Lock();
            PaperSize appropriatePaperSize = PrintOptionsHelper.GetAppropriatePaperSize(this.DocumentModel.PrintingSystem, this.PrinterSettings.PaperSizes);
            IEnumerable<PaperSize> source = this.PrinterSettings.PaperSizes.Cast<PaperSize>();
            this.AvailablePaperSizes = source.Contains<PaperSize>(appropriatePaperSize) ? ((IEnumerable<PaperSize>) source.OfType<PaperSize>().ToArray<PaperSize>()) : ((IEnumerable<PaperSize>) source.Concat<PaperSize>(appropriatePaperSize.Yield<PaperSize>()).ToArray<PaperSize>());
            PageMarginInfo margins = new PageMarginInfo("Custom Page Margins", this.DocumentModel.PrintingSystem.PageMargins);
            PageMarginInfo info = this.data.SingleOrDefault<PageMarginInfo>(x => x == margins);
            this.AvailablePageMargins = (info != null) ? this.data : ((IEnumerable<PageMarginInfo>) this.data.Concat<PageMarginInfo>(margins.Yield<PageMarginInfo>()).ToArray<PageMarginInfo>());
            this.SelectedPaperSize = appropriatePaperSize;
            this.Landscape = this.DocumentModel.PrintingSystem.Landscape;
            this.SelectedPageMargins = this.AvailablePageMargins.SingleOrDefault<PageMarginInfo>(x => x.Margins == this.DocumentModel.PrintingSystem.PageMargins);
            this.pageSettingsLocker.Unlock();
        }

        private void UpdatePaperSources()
        {
            this.paperSourceLocker.Lock();
            this.AvailablePaperSources = Enumerable.Empty<string>();
            this.PaperSource = null;
            if (!this.PrinterIsValid)
            {
                this.paperSourceLocker.Unlock();
            }
            else
            {
                PrintOptionsHelper.UpdatePaperSources(this.printerSettings, delegate (IEnumerable<string> sources) {
                    this.AvailablePaperSources = sources;
                    ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PrintOptions), "x");
                    ParameterExpression[] parameters = new ParameterExpression[] { expression };
                    this.RaisePropertyChanged<PrintOptions, IEnumerable<string>>(System.Linq.Expressions.Expression.Lambda<Func<PrintOptions, IEnumerable<string>>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PrintOptions.get_AvailablePaperSources)), parameters));
                }, delegate (string source) {
                    this.PaperSource = source;
                    this.paperSourceLocker.Unlock();
                    ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PrintOptions), "x");
                    ParameterExpression[] parameters = new ParameterExpression[] { expression };
                    this.RaisePropertyChanged<PrintOptions, bool>(System.Linq.Expressions.Expression.Lambda<Func<PrintOptions, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PrintOptions.get_AllowEditPaperSource)), parameters));
                });
            }
        }

        private DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel DocumentModel { get; set; }

        private System.Drawing.Printing.PrinterSettings PrinterSettings =>
            this.printerSettings;

        internal PSPrintDocument PrintDocument =>
            this.printDocument as PSPrintDocument;

        public bool AllowEditOptions
        {
            get
            {
                Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> evaluator = <>c.<>9__14_0;
                if (<>c.<>9__14_0 == null)
                {
                    Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> local1 = <>c.<>9__14_0;
                    evaluator = <>c.<>9__14_0 = x => x.IsCreated;
                }
                return this.DocumentModel.Return<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool>(evaluator, (<>c.<>9__14_1 ??= () => false));
            }
        }

        public virtual int CurrentPageNumber { get; set; }

        public IEnumerable<PrinterItem> AvailablePrinters =>
            this.availablePrinters;

        public virtual PrinterItem SelectedPrinter
        {
            get => 
                this.selectedPrinter;
            set => 
                base.SetProperty<PrinterItem>(ref this.selectedPrinter, value, System.Linq.Expressions.Expression.Lambda<Func<PrinterItem>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptions)), (MethodInfo) methodof(PrintOptions.get_SelectedPrinter)), new ParameterExpression[0]), new Action(this.OnSelectedPrinterChanged));
        }

        public IEnumerable<string> AvailablePaperSources { get; private set; }

        public string PaperSource
        {
            get => 
                this.paperSource;
            set => 
                base.SetProperty<string>(ref this.paperSource, value, System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptions)), (MethodInfo) methodof(PrintOptions.get_PaperSource)), new ParameterExpression[0]));
        }

        public bool AllowEditPaperSource =>
            this.GetActualCanEditPaperSource();

        public ICommand ShowPreferencesCommand =>
            this.preferencesCommand;

        private bool PrinterIsValid =>
            this.AvailablePrinters.Any<PrinterItem>() || this.AvailablePrinters.Contains<PrinterItem>(this.SelectedPrinter);

        public bool Landscape
        {
            get => 
                this.landscape;
            set => 
                base.SetProperty<bool>(ref this.landscape, value, System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptions)), (MethodInfo) methodof(PrintOptions.get_Landscape)), new ParameterExpression[0]), new Action(this.OnLandscapeChanged));
        }

        public IEnumerable<PaperSize> AvailablePaperSizes { get; protected set; }

        public PaperSize SelectedPaperSize
        {
            get => 
                this.selectedPaperSize;
            set => 
                base.SetProperty<PaperSize>(ref this.selectedPaperSize, value, System.Linq.Expressions.Expression.Lambda<Func<PaperSize>>(System.Linq.Expressions.Expression.Field(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptions)), fieldof(PrintOptions.selectedPaperSize)), new ParameterExpression[0]), new Action(this.OnSelectedPaperSizeChanged));
        }

        public IEnumerable<PageMarginInfo> AvailablePageMargins { get; private set; }

        public PageMarginInfo SelectedPageMargins
        {
            get => 
                this.selectedPageMargin;
            set => 
                base.SetProperty<PageMarginInfo>(ref this.selectedPageMargin, value, System.Linq.Expressions.Expression.Lambda<Func<PageMarginInfo>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptions)), (MethodInfo) methodof(PrintOptions.get_SelectedPageMargins)), new ParameterExpression[0]), new Action(this.OnSelectedPageMarginsChanged));
        }

        private bool ShouldUpdatePageSettings
        {
            get
            {
                System.Drawing.Size size = GraphicsUnitConverter.Convert(new System.Drawing.Size(this.SelectedPaperSize.Width, this.SelectedPaperSize.Height), (float) 100f, (float) 300f);
                return ((this.DocumentModel.PageSettings.PageSize != size) || ((this.DocumentModel.PageSettings.Landscape != this.Landscape) || ((this.SelectedPageMargins != null) ? (this.DocumentModel.PageSettings.Margins != this.SelectedPageMargins.Margins) : false)));
            }
        }

        [BindableProperty(OnPropertyChangedMethodName="OnPrintRangeChanged")]
        public virtual System.Drawing.Printing.PrintRange PrintRange
        {
            get => 
                this.PrinterSettings.PrintRange;
            set => 
                this.PrinterSettings.PrintRange = value;
        }

        [BindableProperty(OnPropertyChangedMethodName="OnCustomPageRangeChanged")]
        public virtual string CustomPageRange
        {
            get
            {
                string customPageRange = this.customPageRange;
                if (this.customPageRange == null)
                {
                    string local1 = this.customPageRange;
                    Func<PSPrintDocument, string> evaluator = <>c.<>9__97_0;
                    if (<>c.<>9__97_0 == null)
                    {
                        Func<PSPrintDocument, string> local2 = <>c.<>9__97_0;
                        evaluator = <>c.<>9__97_0 = x => x.PageRange;
                    }
                    customPageRange = this.customPageRange = this.PrintDocument.With<PSPrintDocument, string>(evaluator);
                }
                return customPageRange;
            }
            set => 
                this.customPageRange = value;
        }

        public virtual bool AllowCustomPageRange { get; protected set; }

        bool IPrintForm.AllowSomePages
        {
            get => 
                this.AllowCustomPageRange;
            set => 
                this.AllowCustomPageRange = value && (this.PrinterSettings.PrintRange != System.Drawing.Printing.PrintRange.SomePages);
        }

        string IPrintForm.PageRangeText
        {
            get
            {
                Func<PSPrintDocument, string> evaluator = <>c.<>9__109_0;
                if (<>c.<>9__109_0 == null)
                {
                    Func<PSPrintDocument, string> local1 = <>c.<>9__109_0;
                    evaluator = <>c.<>9__109_0 = x => x.PageRange;
                }
                return this.PrintDocument.With<PSPrintDocument, string>(evaluator);
            }
            set
            {
                this.PrintDocument.Do<PSPrintDocument>(x => x.PageRange = value);
                ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PrintOptions), "x");
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                this.RaisePropertyChanged<PrintOptions, System.Drawing.Printing.PrintRange>(System.Linq.Expressions.Expression.Lambda<Func<PrintOptions, System.Drawing.Printing.PrintRange>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PrintOptions.get_PrintRange)), parameters));
                expression = System.Linq.Expressions.Expression.Parameter(typeof(PrintOptions), "x");
                ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
                this.RaisePropertyChanged<PrintOptions, bool>(System.Linq.Expressions.Expression.Lambda<Func<PrintOptions, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PrintOptions.get_AllowCustomPageRange)), expressionArray2));
                expression = System.Linq.Expressions.Expression.Parameter(typeof(PrintOptions), "x");
                ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
                this.RaisePropertyChanged<PrintOptions, string>(System.Linq.Expressions.Expression.Lambda<Func<PrintOptions, string>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PrintOptions.get_CustomPageRange)), expressionArray3));
            }
        }

        public bool AllowCollate =>
            this.Copies > 1;

        public bool AllowCopies
        {
            get
            {
                Func<System.Drawing.Printing.PrinterSettings, bool> evaluator = <>c.<>9__115_0;
                if (<>c.<>9__115_0 == null)
                {
                    Func<System.Drawing.Printing.PrinterSettings, bool> local1 = <>c.<>9__115_0;
                    evaluator = <>c.<>9__115_0 = x => x.MaximumCopies > 1;
                }
                return this.PrinterSettings.Return<System.Drawing.Printing.PrinterSettings, bool>(evaluator, (<>c.<>9__115_1 ??= () => false));
            }
        }

        public int MaxCopies
        {
            get
            {
                Func<System.Drawing.Printing.PrinterSettings, int> evaluator = <>c.<>9__117_0;
                if (<>c.<>9__117_0 == null)
                {
                    Func<System.Drawing.Printing.PrinterSettings, int> local1 = <>c.<>9__117_0;
                    evaluator = <>c.<>9__117_0 = x => x.MaximumCopies;
                }
                return this.PrinterSettings.Return<System.Drawing.Printing.PrinterSettings, int>(evaluator, (<>c.<>9__117_1 ??= () => 1));
            }
        }

        [BindableProperty]
        public virtual short Copies
        {
            get => 
                this.PrinterSettings.Copies;
            set => 
                this.SetCopies(value);
        }

        [BindableProperty(true)]
        public virtual bool Collate
        {
            get => 
                this.PrinterSettings.Collate;
            set => 
                this.PrinterSettings.Collate = value;
        }

        [BindableProperty]
        public virtual bool CanDuplex
        {
            get => 
                this.canDuplex;
            set => 
                base.SetProperty<bool>(ref this.canDuplex, value, System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptions)), (MethodInfo) methodof(PrintOptions.get_CanDuplex)), new ParameterExpression[0]));
        }

        [BindableProperty]
        public virtual System.Drawing.Printing.Duplex Duplex
        {
            get => 
                this.duplex;
            set => 
                base.SetProperty<System.Drawing.Printing.Duplex>(ref this.duplex, value, System.Linq.Expressions.Expression.Lambda<Func<System.Drawing.Printing.Duplex>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptions)), (MethodInfo) methodof(PrintOptions.get_Duplex)), new ParameterExpression[0]));
        }

        string IPrintForm.PrintFileName
        {
            get => 
                string.Empty;
            set
            {
            }
        }

        bool IPrintForm.PrintToFile
        {
            get => 
                false;
            set
            {
            }
        }

        System.Drawing.Printing.PrintDocument IPrintForm.Document
        {
            get => 
                this.printDocument;
            set
            {
            }
        }

        string IDataErrorInfo.Error =>
            (this.PrintRange == System.Drawing.Printing.PrintRange.SomePages) ? PrintOptionsHelper.ValidatePageRange(this.CustomPageRange, this.PrinterSettings.MaximumPage) : string.Empty;

        public bool IsValid =>
            this.PrinterIsValid && ((this.PrintDocument != null) && (this.AllowEditOptions && string.IsNullOrEmpty(((IDataErrorInfo) this).Error)));

        string IDataErrorInfo.this[string columnName] =>
            ((columnName != ExpressionHelper.GetPropertyName<string>(System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptions)), (MethodInfo) methodof(PrintOptions.get_CustomPageRange)), new ParameterExpression[0]))) || (this.PrintRange != System.Drawing.Printing.PrintRange.SomePages)) ? string.Empty : PrintOptionsHelper.ValidatePageRange(this.CustomPageRange, this.PrinterSettings.MaximumPage);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PrintOptions.<>c <>9 = new PrintOptions.<>c();
            public static Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> <>9__14_0;
            public static Func<bool> <>9__14_1;
            public static Func<IEnumerable<string>, int> <>9__49_0;
            public static Func<int> <>9__49_1;
            public static Func<HwndSource, IntPtr> <>9__57_0;
            public static Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> <>9__58_0;
            public static Func<bool> <>9__58_1;
            public static Func<DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel, bool> <>9__90_0;
            public static Func<bool> <>9__90_1;
            public static Func<PSPrintDocument, string> <>9__97_0;
            public static Func<PSPrintDocument, string> <>9__109_0;
            public static Func<PrinterSettings, bool> <>9__115_0;
            public static Func<bool> <>9__115_1;
            public static Func<PrinterSettings, int> <>9__117_0;
            public static Func<int> <>9__117_1;

            internal bool <CanShowPageSetup>b__90_0(DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel x) => 
                x.IsCreated && x.CanChangePageSettings;

            internal bool <CanShowPageSetup>b__90_1() => 
                false;

            internal bool <CanShowPreferences>b__58_0(DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel x) => 
                x.IsCreated;

            internal bool <CanShowPreferences>b__58_1() => 
                false;

            internal string <DevExpress.Printing.Native.PrintEditor.IPrintForm.get_PageRangeText>b__109_0(PSPrintDocument x) => 
                x.PageRange;

            internal bool <get_AllowCopies>b__115_0(PrinterSettings x) => 
                x.MaximumCopies > 1;

            internal bool <get_AllowCopies>b__115_1() => 
                false;

            internal bool <get_AllowEditOptions>b__14_0(DevExpress.Xpf.Printing.PreviewControl.IDocumentViewModel x) => 
                x.IsCreated;

            internal bool <get_AllowEditOptions>b__14_1() => 
                false;

            internal string <get_CustomPageRange>b__97_0(PSPrintDocument x) => 
                x.PageRange;

            internal int <get_MaxCopies>b__117_0(PrinterSettings x) => 
                x.MaximumCopies;

            internal int <get_MaxCopies>b__117_1() => 
                1;

            internal int <GetActualCanEditPaperSource>b__49_0(IEnumerable<string> x) => 
                x.Count<string>();

            internal int <GetActualCanEditPaperSource>b__49_1() => 
                0;

            internal IntPtr <ShowPreferences>b__57_0(HwndSource x) => 
                x.Handle;
        }
    }
}

