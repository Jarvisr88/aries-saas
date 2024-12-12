namespace DevExpress.Xpf.Printing.PreviewControl
{
    using DevExpress.DocumentView;
    using DevExpress.Mvvm.Native;
    using DevExpress.Printing;
    using DevExpress.Printing.Native;
    using DevExpress.Printing.Native.PrintEditor;
    using DevExpress.Xpf.Editors.DataPager;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;

    public class PrintOptionsViewModel : PreviewSettingsViewModelBase, IPrintForm, IDataErrorInfo
    {
        private int pagePreviewIndex;
        private readonly PrintEditorController controller;
        private System.Drawing.Printing.PrintDocument printDocument;
        private IPagedCollectionView dataPagerSource;
        private readonly PrintingSystemBase printingSystem;
        private Func<string, bool> showFileReplacingRequest;
        private ImageSource previewImage;
        private PrinterItem selectedPrinter;
        private string customPageRange;
        private string paperSource;
        private string printFileName;
        private const string printFileNameProperty = "PrintFileName";
        private const string printToFileProperty = "PrintToFile";
        private const string pageRangeTextProperty = "CustomPageRange";

        private PrintOptionsViewModel(PrintingSystemBase printingSystem)
        {
            this.printingSystem = printingSystem;
            this.PrinterItems = new ObservableCollection<PrinterItem>();
            this.printDocument = this.CreatePrintDocument();
            this.UpdatePreview();
            Func<Page, int> selector = <>c.<>9__31_0;
            if (<>c.<>9__31_0 == null)
            {
                Func<Page, int> local1 = <>c.<>9__31_0;
                selector = <>c.<>9__31_0 = x => x.Index;
            }
            this.dataPagerSource = new PagedCollectionView(printingSystem.Document.Pages.Select<Page, int>(selector).ToList<int>());
            this.controller = new PrintEditorController(this);
            this.controller.LoadForm(new PrinterItemContainer());
            this.PreferencesCommand = DelegateCommandFactory.Create(new Action(this.OnPreferencesClick));
            this.FileSelectCommand = DelegateCommandFactory.Create(new Action(this.OnFileSelectClick));
            this.UpdatePreviewCommand = DelegateCommandFactory.Create(new Action(this.UpdatePreview));
        }

        private void CoerceCopies()
        {
            short copies;
            if (this.Copies <= this.MaxCopies)
            {
                copies = this.Copies;
            }
            else
            {
                copies = this.Copies = Convert.ToInt16(this.MaxCopies);
            }
            this.Copies = copies;
            base.RaisePropertiesChanged<bool, int>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_AllowCopies)), new ParameterExpression[0]), System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_MaxCopies)), new ParameterExpression[0]));
        }

        public static PrintOptionsViewModel Create(PrintingSystemBase printingSystem) => 
            new PrintOptionsViewModel(printingSystem);

        private PSPrintDocument CreatePrintDocument()
        {
            PageScope scope = new PageScope(this.printingSystem.Extender.PredefinedPageRange, this.printingSystem.Document.PageCount);
            PageSettings settings = (PageSettings) PageSettingsHelper.DefaultPageSettings.Clone();
            if (settings.PrinterSettings != null)
            {
                settings.PrinterSettings = (System.Drawing.Printing.PrinterSettings) this.printingSystem.Extender.PrinterSettings.Clone();
                PageSettings pageSettings = (PageSettings) PageSettingsHelper.DefaultPageSettings.Clone();
                pageSettings.PrinterSettings = settings.PrinterSettings;
                PageSettingsHelper.SetDefaultPageSettings(settings.PrinterSettings, pageSettings);
            }
            System.Drawing.Printing.PrinterSettings printerSettings = settings.PrinterSettings;
            printerSettings.MinimumPage = 1;
            printerSettings.MaximumPage = this.printingSystem.PageCount;
            printerSettings.FromPage = scope.FromPage;
            printerSettings.ToPage = scope.ToPage;
            PrintController printController = this.printingSystem.ShowPrintStatusDialog ? ((PrintController) new PrintProgressController()) : ((PrintController) new StandardPrintController());
            PrintingSystemBase printingSystem = this.printingSystem;
            if (<>c.<>9__40_0 == null)
            {
                PrintingSystemBase local1 = this.printingSystem;
                printingSystem = (PrintingSystemBase) (<>c.<>9__40_0 = () => true);
            }
            PSPrintDocument document1 = new PSPrintDocument((PrintingSystemBase) <>c.<>9__40_0, (System.Drawing.Color) printerSettings, printController, (System.Drawing.Printing.PrinterSettings) this.printingSystem.Graph.PageBackColor, (Func<bool>) printingSystem);
            document1.PageRange = this.printingSystem.Extender.PredefinedPageRange;
            PSPrintDocument document = document1;
            System.Drawing.Printing.PrinterSettings.PaperSizeCollection paperSizes = document.PrinterSettings.PaperSizes;
            PageData data = this.printingSystem.PageSettings.Data;
            document.DefaultPageSettings.PaperSize = PageSizeInfo.GetPaperSize(data.PaperKind, data.Size, paperSizes);
            document.DefaultPageSettings.Landscape = this.printingSystem.PageSettings.Landscape;
            return document;
        }

        void IPrintForm.AddPrinterItem(PrinterItem item)
        {
            this.PrinterItems.Add(item);
        }

        void IPrintForm.SetSelectedPrinter(string printerName)
        {
            PrinterItem item = this.PrinterItems.FirstOrDefault<PrinterItem>(x => x.FullName == printerName);
            if (item != null)
            {
                this.SelectedPrinter = item;
            }
        }

        private void OnFileSelectClick()
        {
            SaveFileDialog dialog1 = new SaveFileDialog();
            dialog1.DefaultExt = ".prn";
            dialog1.Filter = "Printable files (.prn)|*.prn";
            SaveFileDialog dialog = dialog1;
            bool? nullable = dialog.ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag) ? (nullable != null) : false)
            {
                this.PrintFileName = dialog.FileName;
            }
        }

        private void OnPreferencesClick()
        {
            try
            {
                System.Drawing.Printing.PrintRange printRange = this.PrintRange;
                Func<HwndSource, IntPtr> selector = <>c.<>9__108_0;
                if (<>c.<>9__108_0 == null)
                {
                    Func<HwndSource, IntPtr> local1 = <>c.<>9__108_0;
                    selector = <>c.<>9__108_0 = x => x.Handle;
                }
                IntPtr hwnd = PresentationSource.CurrentSources.OfType<HwndSource>().Select<HwndSource, IntPtr>(selector).LastOrDefault<IntPtr>();
                this.controller.ShowPrinterPreferences(hwnd);
                this.PopulatePaperSources();
            }
            catch
            {
            }
        }

        private void PopulatePageNumbers()
        {
            IPrintForm form = this;
            System.Drawing.Printing.PrintRange printRange = this.PrintRange;
            if (printRange == System.Drawing.Printing.PrintRange.AllPages)
            {
                form.PageRangeText = new PageScope(1, this.printingSystem.Pages.Count).PageRange;
            }
            else if (printRange == System.Drawing.Printing.PrintRange.SomePages)
            {
                form.PageRangeText = this.CustomPageRange;
            }
            else if (printRange == System.Drawing.Printing.PrintRange.CurrentPage)
            {
                form.PageRangeText = $"{this.PagePreviewIndex + 1}";
            }
        }

        private void PopulatePaperSources()
        {
            this.paperSource = this.PaperSources.Contains<string>(this.PrintDocument.DefaultPageSettings.PaperSource.SourceName) ? this.PrintDocument.DefaultPageSettings.PaperSource.SourceName : null;
            base.RaisePropertiesChanged<IEnumerable<string>, string>(System.Linq.Expressions.Expression.Lambda<Func<IEnumerable<string>>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_PaperSources)), new ParameterExpression[0]), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_PaperSource)), new ParameterExpression[0]));
        }

        public void SavePrinterSettings()
        {
            this.controller.AssignPrinterSettings();
            this.printingSystem.Extender.PrinterSettings.PrinterName = this.PrinterSettings.PrinterName;
            this.printingSystem.Extender.PrinterSettings.PrintRange = this.PrinterSettings.PrintRange;
            this.printingSystem.Extender.PrinterSettings.Copies = this.PrinterSettings.Copies;
            this.printingSystem.Extender.PrinterSettings.Collate = this.PrinterSettings.Collate;
            if (this.PrinterSettings.PrintToFile && !string.IsNullOrEmpty(this.PrinterSettings.PrintFileName))
            {
                this.printingSystem.Extender.PrinterSettings.PrintToFile = this.PrinterSettings.PrintToFile;
                this.printingSystem.Extender.PrinterSettings.PrintFileName = this.PrinterSettings.PrintFileName;
            }
        }

        public void SetPrintRange(System.Drawing.Printing.PrintRange printRange)
        {
            if (this.PrintRange != printRange)
            {
                System.Drawing.Printing.PrintRange range = this.PrintRange;
                this.PrinterSettings.PrintRange = printRange;
                base.RaisePropertiesChanged<bool, string>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_AllowSomePages)), new ParameterExpression[0]), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_CustomPageRange)), new ParameterExpression[0]));
                this.PopulatePageNumbers();
            }
        }

        private void UpdateDuplex()
        {
            base.RaisePropertiesChanged<bool, System.Drawing.Printing.Duplex>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_CanDuplex)), new ParameterExpression[0]), System.Linq.Expressions.Expression.Lambda<Func<System.Drawing.Printing.Duplex>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_Duplex)), new ParameterExpression[0]));
        }

        private void UpdatePreview()
        {
            IPreviewAreaProvider service = this.GetService<IPreviewAreaProvider>();
            if (service != null)
            {
                System.Windows.Size previewArea = service.PreviewArea;
                if (!previewArea.IsEmpty)
                {
                    Page page = this.printingSystem.Pages[this.PagePreviewIndex];
                    this.PreviewImage = PreviewImageHelper.GetPreview(page, previewArea, service.GetScaleX(), delegate {
                        int[] pageIndices = new int[] { this.PagePreviewIndex };
                        ((IDocument) this.printingSystem).AfterDrawPages(this, pageIndices);
                    });
                }
            }
        }

        private string Validate(string columnName)
        {
            DevExpress.Xpf.Printing.Native.ValidationResult result = null;
            if ((columnName == "PrintFileName") || (columnName == "PrintToFile"))
            {
                result = this.ValidateFileName();
            }
            else if (columnName == "CustomPageRange")
            {
                result = this.ValidatePageNumbers();
            }
            return result?.ErrorMessage;
        }

        private DevExpress.Xpf.Printing.Native.ValidationResult ValidateFileName()
        {
            string messageText = string.Empty;
            return (this.PrintToFile ? (PrintEditorController.ValidateFilePath(this.PrintFileName, out messageText) ? ((!File.Exists(this.PrintFileName) || ((File.GetAttributes(this.PrintFileName) & FileAttributes.ReadOnly) <= 0)) ? null : new DevExpress.Xpf.Printing.Native.ValidationResult(false, string.Format(PreviewStringId.Msg_FileReadOnly.GetString(), this.PrintFileName))) : new DevExpress.Xpf.Printing.Native.ValidationResult(false, messageText)) : null);
        }

        private DevExpress.Xpf.Printing.Native.ValidationResult ValidatePageNumbers()
        {
            IPrintForm form = this;
            int[] indices = PageRangeParser.GetIndices(form.PageRangeText, this.PrinterSettings.MaximumPage);
            return (((this.PrintRange != System.Drawing.Printing.PrintRange.SomePages) || (!string.IsNullOrEmpty(form.PageRangeText) && ((indices.Length != 0) && ((indices.Length != 1) || (indices[0] != -1))))) ? null : new DevExpress.Xpf.Printing.Native.ValidationResult(false, PreviewStringId.Msg_IncorrectPageRange.GetString()));
        }

        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public IPagedCollectionView DataPagerSource =>
            this.dataPagerSource;

        private System.Drawing.Printing.PrinterSettings PrinterSettings =>
            this.PrintDocument.PrinterSettings;

        public Func<string, bool> ShowFileReplacingRequest
        {
            get => 
                this.showFileReplacingRequest;
            set
            {
                if (this.showFileReplacingRequest != value)
                {
                    this.showFileReplacingRequest = value;
                }
            }
        }

        public ICommand PreferencesCommand { get; private set; }

        public ICommand FileSelectCommand { get; private set; }

        public ICommand PrintCommand { get; private set; }

        public ICommand UpdatePreviewCommand { get; private set; }

        public int PagePreviewIndex
        {
            get => 
                this.pagePreviewIndex;
            set => 
                base.SetProperty<int>(ref this.pagePreviewIndex, value, System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_PagePreviewIndex)), new ParameterExpression[0]), delegate {
                    this.PopulatePageNumbers();
                    this.UpdatePreview();
                });
        }

        public ImageSource PreviewImage
        {
            get => 
                this.previewImage;
            set => 
                base.SetProperty<ImageSource>(ref this.previewImage, value, System.Linq.Expressions.Expression.Lambda<Func<ImageSource>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_PreviewImage)), new ParameterExpression[0]));
        }

        public ObservableCollection<PrinterItem> PrinterItems { get; private set; }

        public IEnumerable<string> PaperSources
        {
            get
            {
                Func<System.Drawing.Printing.PaperSource, string> selector = <>c.<>9__46_0;
                if (<>c.<>9__46_0 == null)
                {
                    Func<System.Drawing.Printing.PaperSource, string> local1 = <>c.<>9__46_0;
                    selector = <>c.<>9__46_0 = x => x.SourceName;
                }
                return this.PrinterSettings.PaperSources.Cast<System.Drawing.Printing.PaperSource>().Select<System.Drawing.Printing.PaperSource, string>(selector);
            }
        }

        public PrinterItem SelectedPrinter
        {
            get => 
                this.selectedPrinter;
            set
            {
                this.PrinterSettings.PrinterName = value.FullName;
                base.SetProperty<PrinterItem>(ref this.selectedPrinter, value, System.Linq.Expressions.Expression.Lambda<Func<PrinterItem>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_SelectedPrinter)), new ParameterExpression[0]), delegate {
                    this.CoerceCopies();
                    this.UpdateDuplex();
                    this.PopulatePaperSources();
                });
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public override DevExpress.Xpf.Printing.PreviewControl.Native.SettingsType SettingsType =>
            DevExpress.Xpf.Printing.PreviewControl.Native.SettingsType.Print;

        public bool AllowSomePages
        {
            get => 
                this.PrinterSettings.PrintRange == System.Drawing.Printing.PrintRange.SomePages;
            set
            {
                if (value && (this.PrinterSettings.PrintRange != System.Drawing.Printing.PrintRange.SomePages))
                {
                    this.SetPrintRange(System.Drawing.Printing.PrintRange.SomePages);
                }
            }
        }

        public bool AllowCollate =>
            this.Copies > 1;

        public bool AllowCopies =>
            this.PrinterSettings.MaximumCopies > 1;

        public bool Collate
        {
            get => 
                this.PrinterSettings.Collate;
            set
            {
                this.PrinterSettings.Collate = value;
                base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_Collate)), new ParameterExpression[0]));
            }
        }

        public int MaxCopies =>
            this.PrinterSettings.MaximumCopies;

        public short Copies
        {
            get => 
                this.PrinterSettings.Copies;
            set
            {
                value = (value > 0) ? value : ((short) 1);
                if (this.PrinterSettings.Copies != value)
                {
                    this.PrinterSettings.Copies = value;
                    base.RaisePropertyChanged<short>(System.Linq.Expressions.Expression.Lambda<Func<short>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_Copies)), new ParameterExpression[0]));
                    base.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_AllowCollate)), new ParameterExpression[0]));
                }
            }
        }

        private PSPrintDocument PrintDocument =>
            (PSPrintDocument) this.printDocument;

        System.Drawing.Printing.PrintDocument IPrintForm.Document
        {
            get
            {
                if (this.printDocument == null)
                {
                    this.printDocument = this.CreatePrintDocument();
                    this.printDocument.PrinterSettings.PrintRange = System.Drawing.Printing.PrintRange.AllPages;
                }
                return this.printDocument;
            }
            set => 
                base.SetProperty<System.Drawing.Printing.PrintDocument>(ref this.printDocument, value, System.Linq.Expressions.Expression.Lambda<Func<System.Drawing.Printing.PrintDocument>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Convert(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), typeof(IPrintForm)), (MethodInfo) methodof(IPrintForm.get_Document)), new ParameterExpression[0]));
        }

        public string CustomPageRange
        {
            get
            {
                string customPageRange = this.customPageRange;
                if (this.customPageRange == null)
                {
                    string local1 = this.customPageRange;
                    customPageRange = this.customPageRange = this.PrintDocument.PageRange;
                }
                return customPageRange;
            }
            set => 
                base.SetProperty<string>(ref this.customPageRange, value, System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_CustomPageRange)), new ParameterExpression[0]), new Action(this.PopulatePageNumbers));
        }

        string IPrintForm.PageRangeText
        {
            get => 
                this.PrintDocument.PageRange;
            set => 
                this.PrintDocument.PageRange = value;
        }

        public string PaperSource
        {
            get => 
                this.paperSource;
            set
            {
                if (this.paperSource != value)
                {
                    this.paperSource = value;
                    foreach (System.Drawing.Printing.PaperSource source in this.PrinterSettings.PaperSources)
                    {
                        if (source.SourceName == this.paperSource)
                        {
                            this.PrintDocument.DefaultPageSettings.PaperSource = source;
                        }
                    }
                    base.RaisePropertyChanged<string>(System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_PaperSource)), new ParameterExpression[0]));
                }
            }
        }

        public System.Drawing.Printing.PrintRange PrintRange
        {
            get => 
                this.PrinterSettings.PrintRange;
            set
            {
                this.SetPrintRange(value);
                base.RaisePropertiesChanged<System.Drawing.Printing.PrintRange, string>(System.Linq.Expressions.Expression.Lambda<Func<System.Drawing.Printing.PrintRange>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_PrintRange)), new ParameterExpression[0]), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_CustomPageRange)), new ParameterExpression[0]));
            }
        }

        public bool PrintToFile
        {
            get => 
                this.PrinterSettings.PrintToFile;
            set
            {
                if (this.PrinterSettings.PrintToFile != value)
                {
                    this.PrinterSettings.PrintToFile = value;
                    base.RaisePropertiesChanged<bool, string>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_PrintToFile)), new ParameterExpression[0]), System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_PrintFileName)), new ParameterExpression[0]));
                }
            }
        }

        public string PrintFileName
        {
            get => 
                this.printFileName;
            set => 
                base.SetProperty<string>(ref this.printFileName, value, System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_PrintFileName)), new ParameterExpression[0]), delegate {
                    if (!string.IsNullOrEmpty(this.PrintFileName))
                    {
                        this.PrinterSettings.PrintFileName = this.PrintFileName;
                    }
                });
        }

        public bool CanDuplex =>
            this.PrinterSettings.CanDuplex;

        bool IPrintForm.CanDuplex
        {
            get => 
                this.CanDuplex;
            set
            {
            }
        }

        public System.Drawing.Printing.Duplex Duplex
        {
            get => 
                this.PrinterSettings.Duplex;
            set
            {
                this.PrinterSettings.Duplex = value;
                base.RaisePropertyChanged<System.Drawing.Printing.Duplex>(System.Linq.Expressions.Expression.Lambda<Func<System.Drawing.Printing.Duplex>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PrintOptionsViewModel)), (MethodInfo) methodof(PrintOptionsViewModel.get_Duplex)), new ParameterExpression[0]));
            }
        }

        string IDataErrorInfo.Error =>
            string.Empty;

        string IDataErrorInfo.this[string columnName] =>
            this.Validate(columnName);

        public bool IsValid
        {
            get
            {
                Func<DevExpress.Xpf.Printing.Native.ValidationResult, bool> evaluator = <>c.<>9__122_0;
                if (<>c.<>9__122_0 == null)
                {
                    Func<DevExpress.Xpf.Printing.Native.ValidationResult, bool> local1 = <>c.<>9__122_0;
                    evaluator = <>c.<>9__122_0 = x => x.IsValid;
                }
                if (!this.ValidateFileName().Return<DevExpress.Xpf.Printing.Native.ValidationResult, bool>(evaluator, (<>c.<>9__122_1 ??= () => true)))
                {
                    return false;
                }
                Func<DevExpress.Xpf.Printing.Native.ValidationResult, bool> func2 = <>c.<>9__122_2;
                if (<>c.<>9__122_2 == null)
                {
                    Func<DevExpress.Xpf.Printing.Native.ValidationResult, bool> local3 = <>c.<>9__122_2;
                    func2 = <>c.<>9__122_2 = x => x.IsValid;
                }
                return this.ValidatePageNumbers().Return<DevExpress.Xpf.Printing.Native.ValidationResult, bool>(func2, (<>c.<>9__122_3 ??= () => true));
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PrintOptionsViewModel.<>c <>9 = new PrintOptionsViewModel.<>c();
            public static Func<Page, int> <>9__31_0;
            public static Func<bool> <>9__40_0;
            public static Func<PaperSource, string> <>9__46_0;
            public static Func<HwndSource, IntPtr> <>9__108_0;
            public static Func<DevExpress.Xpf.Printing.Native.ValidationResult, bool> <>9__122_0;
            public static Func<bool> <>9__122_1;
            public static Func<DevExpress.Xpf.Printing.Native.ValidationResult, bool> <>9__122_2;
            public static Func<bool> <>9__122_3;

            internal int <.ctor>b__31_0(Page x) => 
                x.Index;

            internal bool <CreatePrintDocument>b__40_0() => 
                true;

            internal bool <get_IsValid>b__122_0(DevExpress.Xpf.Printing.Native.ValidationResult x) => 
                x.IsValid;

            internal bool <get_IsValid>b__122_1() => 
                true;

            internal bool <get_IsValid>b__122_2(DevExpress.Xpf.Printing.Native.ValidationResult x) => 
                x.IsValid;

            internal bool <get_IsValid>b__122_3() => 
                true;

            internal string <get_PaperSources>b__46_0(PaperSource x) => 
                x.SourceName;

            internal IntPtr <OnPreferencesClick>b__108_0(HwndSource x) => 
                x.Handle;
        }

        private class PagedCollectionView : List<int>, IPagedCollectionView
        {
            private readonly IEnumerable<int> enumerable;

            public PagedCollectionView(IEnumerable<int> enumerable) : base(enumerable)
            {
                this.enumerable = enumerable;
            }

            public bool MoveToFirstPage()
            {
                this.PageIndex = this.enumerable.First<int>();
                return true;
            }

            public bool MoveToPage(int pageIndex)
            {
                if (!this.enumerable.Contains<int>(pageIndex))
                {
                    return false;
                }
                this.PageIndex = pageIndex;
                return true;
            }

            public bool CanChangePage =>
                true;

            public int ItemCount =>
                this.enumerable.Count<int>();

            public int PageIndex { get; private set; }

            public int PageSize { get; set; }

            public int TotalItemCount =>
                this.ItemCount;
        }
    }
}

