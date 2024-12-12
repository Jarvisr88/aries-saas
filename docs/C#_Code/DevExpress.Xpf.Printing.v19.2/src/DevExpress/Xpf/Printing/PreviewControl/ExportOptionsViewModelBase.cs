namespace DevExpress.Xpf.Printing.PreviewControl
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Printing.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Printing.Native.Dialogs;
    using DevExpress.Xpf.Printing.Native.Lines;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.ExportOptionsControllers;
    using DevExpress.XtraPrinting.Native.Lines;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    public abstract class ExportOptionsViewModelBase : PreviewSettingsViewModelBase
    {
        private readonly DevExpress.XtraPrinting.ExportOptions options;
        private readonly string proposedFileName;
        private DevExpress.XtraPrinting.ExportFormat exportFormat;
        private IEnumerable<DevExpress.XtraPrinting.ExportFormat> hiddenExportFormats = Enumerable.Empty<DevExpress.XtraPrinting.ExportFormat>();
        private ExportOptionsBase exportOptions;
        private ExportOptionsControllerBase controller;
        private LineBase[] exportOptionLines;
        private ObservableCollection<ExportOptionKind> hiddenOptions;
        private string fileName = string.Empty;
        private bool pageRangeIsValid = true;
        private readonly PrintingSystemBase printingSystem;
        private readonly DelegateCommand submitCommand;
        private bool fileWasOverwritenByOpenFileDialog;

        protected ExportOptionsViewModelBase(PrintingSystemBase printingSystem)
        {
            this.options = printingSystem.ExportOptions;
            this.AvailableExportModes = printingSystem.Document.AvailableExportModes;
            this.proposedFileName = ValidateFileName(this.PreviewOptions.DefaultFileName, "Document");
            this.submitCommand = DelegateCommandFactory.Create(new Action(this.Submit), new Func<bool>(this.ValidateModel), false);
            this.SelectFileCommand = DelegateCommandFactory.Create(() => this.SelectFile());
            this.ExportOptions = this.GetExportOptions(this.ExportFormat).CloneOptions();
            this.controller = ExportOptionsControllerBase.GetControllerByOptions(this.ExportOptions);
            this.HiddenOptions = new ObservableCollection<ExportOptionKind>(this.options.HiddenOptions);
            this.ClosingCommand = new DelegateCommand<CancelEventArgs>(new Action<CancelEventArgs>(this.WindowClosing));
            this.printingSystem = printingSystem;
        }

        internal static IEnumerable<DevExpress.XtraPrinting.ExportFormat> AllExportFormats()
        {
            DevExpress.XtraPrinting.ExportFormat[] second = new DevExpress.XtraPrinting.ExportFormat[] { DevExpress.XtraPrinting.ExportFormat.Prnx, DevExpress.XtraPrinting.ExportFormat.Xps };
            return Enum.GetValues(typeof(DevExpress.XtraPrinting.ExportFormat)).Cast<DevExpress.XtraPrinting.ExportFormat>().Except<DevExpress.XtraPrinting.ExportFormat>(second);
        }

        private void ApplyExportLines(IEnumerable<ILine> lines)
        {
            this.ExportOptionLines.Do<LineBase[]>(delegate (LineBase[] l) {
                GetPageRangeLineEdit(l).Do<BaseEdit>(delegate (BaseEdit x) {
                    x.Validate -= new ValidateEventHandler(this.ValidatePageRange);
                });
                GetImageFormatLineEdit(l).Do<ComboBoxEdit>(delegate (ComboBoxEdit x) {
                    x.SelectedIndexChanged -= new RoutedEventHandler(this.OnSelectedIndexCHanged);
                });
            });
            Converter<ILine, LineBase> converter = <>c.<>9__62_3;
            if (<>c.<>9__62_3 == null)
            {
                Converter<ILine, LineBase> local1 = <>c.<>9__62_3;
                converter = <>c.<>9__62_3 = line => (LineBase) line;
            }
            this.ExportOptionLines = Array.ConvertAll<ILine, LineBase>(lines.ToArray<ILine>(), converter);
            GetPageRangeLineEdit(this.ExportOptionLines).Do<BaseEdit>(delegate (BaseEdit x) {
                x.Validate += new ValidateEventHandler(this.ValidatePageRange);
            });
            GetImageFormatLineEdit(this.ExportOptionLines).Do<ComboBoxEdit>(delegate (ComboBoxEdit x) {
                x.SelectedIndexChanged += new RoutedEventHandler(this.OnSelectedIndexCHanged);
            });
        }

        private DevExpress.XtraPrinting.ExportFormat CoerceExportFormat(DevExpress.XtraPrinting.ExportFormat value) => 
            !this.HiddenExportFormats.Contains<DevExpress.XtraPrinting.ExportFormat>(value) ? value : this.AvailableExportFormats.FirstOrDefault<DevExpress.XtraPrinting.ExportFormat>();

        private ObservableCollection<ExportOptionKind> CreateEmptyHiddenOptions()
        {
            ObservableCollection<ExportOptionKind> observables = new ObservableCollection<ExportOptionKind>();
            observables.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnHiddenOptionsCollectionChanged);
            return observables;
        }

        private void DoWithCurrentWindowService(Action<CurrentWindowService> action)
        {
            (this.GetService<ICurrentWindowService>() as CurrentWindowService).Do<CurrentWindowService>(action);
        }

        private string GetDefaultDirectory() => 
            string.IsNullOrEmpty(this.PreviewOptions.DefaultDirectory) ? this.GetDefaultSystemDirectory() : this.PreviewOptions.DefaultDirectory;

        private string GetDefaultFilePath() => 
            (this.PreviewOptions.SaveMode != SaveMode.UsingDefaultPath) ? Path.Combine(this.GetDefaultSystemDirectory(), this.proposedFileName + this.controller.GetFileExtension(this.ExportOptions)) : Path.Combine(this.GetDefaultDirectory(), this.proposedFileName + this.controller.GetFileExtension(this.ExportOptions));

        private string GetDefaultSystemDirectory()
        {
            string defaultDirectory = SystemFileDialogHelper.GetDefaultDirectory();
            return ((string.IsNullOrEmpty(defaultDirectory) || !Directory.Exists(defaultDirectory)) ? Directory.GetCurrentDirectory() : defaultDirectory);
        }

        private ExportOptionsBase GetExportOptions(DevExpress.XtraPrinting.ExportFormat format)
        {
            switch (format)
            {
                case DevExpress.XtraPrinting.ExportFormat.Pdf:
                    return this.options.Options[typeof(PdfExportOptions)];

                case DevExpress.XtraPrinting.ExportFormat.Htm:
                    return this.options.Options[typeof(HtmlExportOptions)];

                case DevExpress.XtraPrinting.ExportFormat.Mht:
                    return this.options.Options[typeof(MhtExportOptions)];

                case DevExpress.XtraPrinting.ExportFormat.Rtf:
                    return this.options.Options[typeof(RtfExportOptions)];

                case DevExpress.XtraPrinting.ExportFormat.Docx:
                    return this.options.Options[typeof(DocxExportOptions)];

                case DevExpress.XtraPrinting.ExportFormat.Xls:
                    return this.options.Options[typeof(XlsExportOptions)];

                case DevExpress.XtraPrinting.ExportFormat.Xlsx:
                    return this.options.Options[typeof(XlsxExportOptions)];

                case DevExpress.XtraPrinting.ExportFormat.Csv:
                    return this.options.Options[typeof(CsvExportOptions)];

                case DevExpress.XtraPrinting.ExportFormat.Txt:
                    return this.options.Options[typeof(TextExportOptions)];

                case DevExpress.XtraPrinting.ExportFormat.Image:
                    return this.options.Options[typeof(ImageExportOptions)];

                case DevExpress.XtraPrinting.ExportFormat.Xps:
                    return this.options.Options[typeof(XpsExportOptions)];
            }
            throw new ArgumentException("format");
        }

        private static ComboBoxEdit GetImageFormatLineEdit(IEnumerable<ILine> lines)
        {
            Func<PropertyLine, bool> predicate = <>c.<>9__65_0;
            if (<>c.<>9__65_0 == null)
            {
                Func<PropertyLine, bool> local1 = <>c.<>9__65_0;
                predicate = <>c.<>9__65_0 = delegate (PropertyLine x) {
                    ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(ImageExportOptions), "y");
                    ParameterExpression[] parameters = new ParameterExpression[] { expression };
                    return x.Property.Name == ExpressionHelper.GetPropertyName<ImageExportOptions, ImageFormat>(System.Linq.Expressions.Expression.Lambda<Func<ImageExportOptions, ImageFormat>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ImageExportOptions.get_Format)), parameters));
                };
            }
            Func<PropertyLine, ComboBoxEdit> evaluator = <>c.<>9__65_1;
            if (<>c.<>9__65_1 == null)
            {
                Func<PropertyLine, ComboBoxEdit> local2 = <>c.<>9__65_1;
                evaluator = <>c.<>9__65_1 = x => x.Content as ComboBoxEdit;
            }
            return lines.OfType<PropertyLine>().FirstOrDefault<PropertyLine>(predicate).With<PropertyLine, ComboBoxEdit>(evaluator);
        }

        private IEnumerable<ILine> GetLines()
        {
            DevExpress.XtraPrinting.Native.AvailableExportModes availableExportModes = this.AvailableExportModes;
            DevExpress.XtraPrinting.Native.AvailableExportModes modes2 = availableExportModes;
            if (availableExportModes == null)
            {
                DevExpress.XtraPrinting.Native.AvailableExportModes local1 = availableExportModes;
                modes2 = new DevExpress.XtraPrinting.Native.AvailableExportModes();
            }
            List<ILine> source = this.controller.GetExportLines(this.ExportOptions, new LineFactory(), modes2, this.HiddenOptions.ToList<ExportOptionKind>()).ToList<ILine>();
            ILine item = source.LastOrDefault<ILine>();
            if ((item != null) && (item is EmptyLine))
            {
                source.Remove(item);
            }
            return source;
        }

        private static BaseEdit GetPageRangeLineEdit(IEnumerable<ILine> lines)
        {
            if (lines == null)
            {
                return null;
            }
            Func<PropertyLine, bool> predicate = <>c.<>9__64_0;
            if (<>c.<>9__64_0 == null)
            {
                Func<PropertyLine, bool> local1 = <>c.<>9__64_0;
                predicate = <>c.<>9__64_0 = delegate (PropertyLine x) {
                    ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PageByPageExportOptionsBase), "y");
                    ParameterExpression[] parameters = new ParameterExpression[] { expression };
                    return x.Property.Name == ExpressionHelper.GetPropertyName<PageByPageExportOptionsBase, string>(System.Linq.Expressions.Expression.Lambda<Func<PageByPageExportOptionsBase, string>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageByPageExportOptionsBase.get_PageRange)), parameters));
                };
            }
            Func<PropertyLine, BaseEdit> evaluator = <>c.<>9__64_1;
            if (<>c.<>9__64_1 == null)
            {
                Func<PropertyLine, BaseEdit> local2 = <>c.<>9__64_1;
                evaluator = <>c.<>9__64_1 = x => x.Content as BaseEdit;
            }
            return lines.OfType<PropertyLine>().FirstOrDefault<PropertyLine>(predicate).With<PropertyLine, BaseEdit>(evaluator);
        }

        protected virtual void OnExportFormatChanged()
        {
            this.ExportOptions = this.GetExportOptions(this.ExportFormat).CloneOptions();
            this.controller = ExportOptionsControllerBase.GetControllerByOptions(this.ExportOptions);
            this.FileName = string.IsNullOrEmpty(this.FileName) ? this.GetDefaultFilePath() : this.UpdateFilePath();
            IEnumerable<ILine> lines = this.GetLines();
            this.ApplyExportLines(lines);
        }

        private void OnHiddenOptionsCollectionChanged(object s = null, NotifyCollectionChangedEventArgs e = null)
        {
            this.ApplyExportLines(this.GetLines());
        }

        private void OnSelectedIndexCHanged(object sender, RoutedEventArgs e)
        {
            this.FileName = string.IsNullOrEmpty(this.FileName) ? this.GetDefaultFilePath() : this.UpdateFilePath();
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public virtual bool SelectFile()
        {
            SaveFileDialog dialog = new SaveFileDialog {
                Title = PreviewLocalizer.GetString(PreviewStringId.SaveDlg_Title)
            };
            string str = SystemFileDialogHelper.CoerceDirectory(Path.GetDirectoryName(this.FileName));
            dialog.ValidateNames = true;
            if (!string.IsNullOrEmpty(this.FileName))
            {
                dialog.InitialDirectory = str;
                dialog.FileName = Path.GetFileName(this.FileName);
            }
            else
            {
                dialog.InitialDirectory = this.PreviewOptions.DefaultDirectory;
                dialog.FileName = this.proposedFileName;
            }
            dialog.Filter = this.controller.Filter;
            dialog.FilterIndex = this.controller.GetFilterIndex(this.exportOptions);
            dialog.OverwritePrompt = this.controller.ValidateInputFileName(this.exportOptions);
            bool? nullable = dialog.ShowDialog();
            bool flag2 = true;
            bool flag = ((nullable.GetValueOrDefault() == flag2) ? (nullable != null) : false) && !string.IsNullOrEmpty(dialog.FileName);
            if (flag)
            {
                this.FileName = DevExpress.XtraPrinting.Native.FileHelper.SetValidExtension(dialog.FileName, this.controller.GetFileExtension(this.exportOptions), this.controller.FileExtensions);
            }
            this.fileWasOverwritenByOpenFileDialog = flag;
            CommandManager.InvalidateRequerySuggested();
            return flag;
        }

        private void Submit()
        {
            this.DoWithCurrentWindowService(windowService => windowService.ClosingCommand = this.ClosingCommand);
        }

        private string UpdateFilePath()
        {
            string directoryName = Path.GetDirectoryName(this.FileName);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.FileName);
            return Path.Combine(string.IsNullOrEmpty(directoryName) ? this.GetDefaultDirectory() : directoryName, (string.IsNullOrEmpty(fileNameWithoutExtension) ? this.proposedFileName : fileNameWithoutExtension) + this.controller.GetFileExtension(this.ExportOptions));
        }

        internal static string ValidateFileName(string fileName, string defaultFileName)
        {
            if (!File.Exists(fileName))
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    return defaultFileName;
                }
                try
                {
                    string path = Path.Combine(Path.GetTempPath(), fileName);
                    File.Create(path).Close();
                    File.Delete(path);
                }
                catch (Exception)
                {
                    return defaultFileName;
                }
            }
            return fileName;
        }

        protected virtual bool ValidateModel() => 
            !string.IsNullOrEmpty(this.FileName) && this.pageRangeIsValid;

        internal virtual void ValidatePageRange(object sender, ValidationEventArgs e)
        {
            string str = e.Value as string;
            if (string.IsNullOrEmpty(str))
            {
                this.pageRangeIsValid = true;
            }
            else
            {
                int[] indices = PageRangeParser.GetIndices(str, this.printingSystem.PageCount);
                if ((indices.Length == 0) || ((indices.Length == 1) && (indices[0] == -1)))
                {
                    e.ErrorContent = PreviewLocalizer.GetString(PreviewStringId.Msg_IncorrectPageRange);
                    e.IsValid = false;
                    e.Handled = true;
                }
                this.pageRangeIsValid = e.IsValid;
            }
        }

        private void WindowClosing(CancelEventArgs e)
        {
            if (!File.Exists(this.FileName) || this.fileWasOverwritenByOpenFileDialog)
            {
                e.Cancel = false;
            }
            else
            {
                MessageResult result = this.GetService<IMessageBoxService>().Show(PreviewStringId.Msg_FileAlreadyExists.GetString(), PreviewStringId.Msg_OpenFileQuestionCaption.GetString(), MessageButton.OKCancel, MessageIcon.Question, MessageResult.None);
                e.Cancel = result == MessageResult.Cancel;
            }
            Action<CurrentWindowService> action = <>c.<>9__57_0;
            if (<>c.<>9__57_0 == null)
            {
                Action<CurrentWindowService> local1 = <>c.<>9__57_0;
                action = <>c.<>9__57_0 = service => service.ClosingCommand = null;
            }
            this.DoWithCurrentWindowService(action);
        }

        protected internal DevExpress.XtraPrinting.ExportOptions Options =>
            this.options;

        internal PrintPreviewOptions PreviewOptions =>
            this.Options.PrintPreview;

        public DevExpress.XtraPrinting.ExportFormat ExportFormat
        {
            get => 
                this.exportFormat;
            set
            {
                this.exportFormat = this.CoerceExportFormat(value);
                this.OnExportFormatChanged();
                base.RaisePropertyChanged<DevExpress.XtraPrinting.ExportFormat>(System.Linq.Expressions.Expression.Lambda<Func<DevExpress.XtraPrinting.ExportFormat>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ExportOptionsViewModelBase)), (MethodInfo) methodof(ExportOptionsViewModelBase.get_ExportFormat)), new ParameterExpression[0]));
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public LineBase[] ExportOptionLines
        {
            get => 
                this.exportOptionLines;
            private set => 
                base.SetProperty<LineBase[]>(ref this.exportOptionLines, value, System.Linq.Expressions.Expression.Lambda<Func<LineBase[]>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ExportOptionsViewModelBase)), (MethodInfo) methodof(ExportOptionsViewModelBase.get_ExportOptionLines)), new ParameterExpression[0]));
        }

        public ExportOptionsBase ExportOptions
        {
            get => 
                this.exportOptions;
            private set => 
                base.SetProperty<ExportOptionsBase>(ref this.exportOptions, value, System.Linq.Expressions.Expression.Lambda<Func<ExportOptionsBase>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ExportOptionsViewModelBase)), (MethodInfo) methodof(ExportOptionsViewModelBase.get_ExportOptions)), new ParameterExpression[0]));
        }

        public string FileName
        {
            get => 
                this.fileName;
            set
            {
                base.SetProperty<string>(ref this.fileName, value, System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ExportOptionsViewModelBase)), (MethodInfo) methodof(ExportOptionsViewModelBase.get_FileName)), new ParameterExpression[0]));
                this.fileWasOverwritenByOpenFileDialog = false;
                this.submitCommand.RaiseCanExecuteChanged();
            }
        }

        private DevExpress.XtraPrinting.Native.AvailableExportModes AvailableExportModes { get; set; }

        public IEnumerable<DevExpress.XtraPrinting.ExportFormat> HiddenExportFormats
        {
            get => 
                this.hiddenExportFormats;
            set
            {
                base.SetProperty<IEnumerable<DevExpress.XtraPrinting.ExportFormat>>(ref this.hiddenExportFormats, value, System.Linq.Expressions.Expression.Lambda<Func<IEnumerable<DevExpress.XtraPrinting.ExportFormat>>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ExportOptionsViewModelBase)), (MethodInfo) methodof(ExportOptionsViewModelBase.get_HiddenExportFormats)), new ParameterExpression[0]), () => base.RaisePropertyChanged<IEnumerable<DevExpress.XtraPrinting.ExportFormat>>(System.Linq.Expressions.Expression.Lambda<Func<IEnumerable<DevExpress.XtraPrinting.ExportFormat>>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ExportOptionsViewModelBase)), (MethodInfo) methodof(ExportOptionsViewModelBase.get_AvailableExportFormats)), new ParameterExpression[0])));
                this.ExportFormat = this.CoerceExportFormat(this.ExportFormat);
            }
        }

        internal ObservableCollection<ExportOptionKind> HiddenOptions
        {
            get
            {
                ObservableCollection<ExportOptionKind> hiddenOptions = this.hiddenOptions;
                if (this.hiddenOptions == null)
                {
                    ObservableCollection<ExportOptionKind> local1 = this.hiddenOptions;
                    hiddenOptions = this.hiddenOptions = this.CreateEmptyHiddenOptions();
                }
                return hiddenOptions;
            }
            set
            {
                this.hiddenOptions.Do<ObservableCollection<ExportOptionKind>>(delegate (ObservableCollection<ExportOptionKind> x) {
                    x.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnHiddenOptionsCollectionChanged);
                });
                base.SetProperty<ObservableCollection<ExportOptionKind>>(ref this.hiddenOptions, value, System.Linq.Expressions.Expression.Lambda<Func<ObservableCollection<ExportOptionKind>>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(ExportOptionsViewModelBase)), (MethodInfo) methodof(ExportOptionsViewModelBase.get_HiddenOptions)), new ParameterExpression[0]), () => this.OnHiddenOptionsCollectionChanged(null, null));
                this.hiddenOptions.Do<ObservableCollection<ExportOptionKind>>(delegate (ObservableCollection<ExportOptionKind> x) {
                    x.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnHiddenOptionsCollectionChanged);
                });
            }
        }

        public virtual IEnumerable<DevExpress.XtraPrinting.ExportFormat> AvailableExportFormats
        {
            get
            {
                IEnumerable<DevExpress.XtraPrinting.ExportFormat> first = AllExportFormats();
                return ((this.hiddenExportFormats != null) ? first.Except<DevExpress.XtraPrinting.ExportFormat>(this.hiddenExportFormats) : first);
            }
        }

        public bool ShowOptionsBeforeExport =>
            ExportOptionsHelper.GetShowOptionsBeforeExport(this.ExportOptions, this.PreviewOptions.ShowOptionsBeforeExport);

        public ICommand SubmitCommand =>
            this.submitCommand;

        public ICommand SelectFileCommand { get; private set; }

        private ICommand ClosingCommand { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExportOptionsViewModelBase.<>c <>9 = new ExportOptionsViewModelBase.<>c();
            public static Action<CurrentWindowService> <>9__57_0;
            public static Converter<ILine, LineBase> <>9__62_3;
            public static Func<PropertyLine, bool> <>9__64_0;
            public static Func<PropertyLine, BaseEdit> <>9__64_1;
            public static Func<PropertyLine, bool> <>9__65_0;
            public static Func<PropertyLine, ComboBoxEdit> <>9__65_1;

            internal LineBase <ApplyExportLines>b__62_3(ILine line) => 
                (LineBase) line;

            internal bool <GetImageFormatLineEdit>b__65_0(PropertyLine x)
            {
                ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(ImageExportOptions), "y");
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return (x.Property.Name == ExpressionHelper.GetPropertyName<ImageExportOptions, ImageFormat>(System.Linq.Expressions.Expression.Lambda<Func<ImageExportOptions, ImageFormat>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(ImageExportOptions.get_Format)), parameters)));
            }

            internal ComboBoxEdit <GetImageFormatLineEdit>b__65_1(PropertyLine x) => 
                x.Content as ComboBoxEdit;

            internal bool <GetPageRangeLineEdit>b__64_0(PropertyLine x)
            {
                ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PageByPageExportOptionsBase), "y");
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return (x.Property.Name == ExpressionHelper.GetPropertyName<PageByPageExportOptionsBase, string>(System.Linq.Expressions.Expression.Lambda<Func<PageByPageExportOptionsBase, string>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PageByPageExportOptionsBase.get_PageRange)), parameters)));
            }

            internal BaseEdit <GetPageRangeLineEdit>b__64_1(PropertyLine x) => 
                x.Content as BaseEdit;

            internal void <WindowClosing>b__57_0(CurrentWindowService service)
            {
                service.ClosingCommand = null;
            }
        }
    }
}

