namespace DevExpress.Xpf.Printing
{
    using DevExpress.Printing;
    using DevExpress.Printing.Native.PrintEditor;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class BackstageDocumentPreview : Control
    {
        internal static readonly DependencyPropertyKey IsPrintersAvailablePropertyKey = DependencyProperty.RegisterReadOnly("IsPrintersAvailable", typeof(bool), typeof(BackstageDocumentPreview), new FrameworkPropertyMetadata(true));
        public static readonly DependencyProperty IsPrintersAvailableProperty = IsPrintersAvailablePropertyKey.DependencyProperty;
        internal static readonly DependencyPropertyKey PrintersErrorMessagePropertyKey = DependencyProperty.RegisterReadOnly("PrintersErrorMessage", typeof(string), typeof(BackstageDocumentPreview), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty PrintersErrorMessageProperty = PrintersErrorMessagePropertyKey.DependencyProperty;
        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register("Model", typeof(IDocumentPreviewModel), typeof(BackstageDocumentPreview), new PropertyMetadata(null));
        public static readonly DependencyProperty SelectedPrinterProperty = DependencyProperty.Register("SelectedPrinter", typeof(PrinterViewModel), typeof(BackstageDocumentPreview), new PropertyMetadata(null));
        private static readonly DependencyPropertyKey PrintersPropertyKey = DependencyProperty.RegisterReadOnly("Printers", typeof(List<PrinterViewModel>), typeof(BackstageDocumentPreview), new PropertyMetadata(null));
        public static readonly DependencyProperty PrintersProperty = PrintersPropertyKey.DependencyProperty;
        public static readonly DependencyProperty OptionsModeProperty;
        private static readonly DependencyPropertyKey IsPrintOptionsVisiblePropertyKey;
        public static readonly DependencyProperty IsPrintOptionsVisibleProperty;
        public static readonly DependencyProperty SelectedFormatProperty;
        private static readonly DependencyPropertyKey ExportFormatsPropertyKey;
        public static readonly DependencyProperty ExportFormatsProperty;
        public static readonly DependencyProperty CustomSettingsHeaderProperty;
        public static readonly DependencyProperty CustomSettingsProperty;

        static BackstageDocumentPreview()
        {
            OptionsModeProperty = DependencyProperty.Register("OptionsMode", typeof(BackstagePreviewOptionsMode), typeof(BackstageDocumentPreview), new PropertyMetadata(BackstagePreviewOptionsMode.Printing, (d, e) => ((BackstageDocumentPreview) d).IsPrintOptionsVisible = ((BackstageDocumentPreview) d).OptionsMode == BackstagePreviewOptionsMode.Printing));
            IsPrintOptionsVisiblePropertyKey = DependencyProperty.RegisterReadOnly("IsPrintOptionsVisible", typeof(bool), typeof(BackstageDocumentPreview), new PropertyMetadata(true));
            IsPrintOptionsVisibleProperty = IsPrintOptionsVisiblePropertyKey.DependencyProperty;
            SelectedFormatProperty = DependencyProperty.Register("SelectedFormat", typeof(string), typeof(BackstageDocumentPreview), new PropertyMetadata(null));
            ExportFormatsPropertyKey = DependencyProperty.RegisterReadOnly("ExportFormats", typeof(IEnumerable<string>), typeof(BackstageDocumentPreview), new PropertyMetadata(null));
            ExportFormatsProperty = ExportFormatsPropertyKey.DependencyProperty;
            CustomSettingsHeaderProperty = DependencyProperty.Register("CustomSettingsHeader", typeof(object), typeof(BackstageDocumentPreview), new PropertyMetadata(null));
            CustomSettingsProperty = DependencyProperty.Register("CustomSettings", typeof(object), typeof(BackstageDocumentPreview), new PropertyMetadata(null));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(BackstageDocumentPreview), new FrameworkPropertyMetadata(typeof(BackstageDocumentPreview)));
        }

        public BackstageDocumentPreview()
        {
            try
            {
                this.Printers = new List<PrinterViewModel>();
                this.LoadPrinters();
            }
            catch (Exception exception)
            {
                this.IsPrintersAvailable = false;
                this.PrintersErrorMessage = exception.Message;
            }
            string[] second = new string[] { "Prnx" };
            this.ExportFormats = Enum.GetNames(typeof(ExportFormat)).Except<string>(second);
            this.SelectedFormat = this.ExportFormats.FirstOrDefault<string>();
        }

        private void LoadPrinters()
        {
            PrinterTypeToImageUriConverter converter = new PrinterTypeToImageUriConverter();
            foreach (PrinterItem item in new PrinterItemContainer().Items)
            {
                PrinterViewModel model1 = new PrinterViewModel();
                model1.IsOffline = item.PrinterType.HasFlag(PrinterType.Offline);
                model1.DisplayName = item.DisplayName;
                model1.Name = item.FullName;
                model1.Status = item.Status;
                model1.ImageUri = converter.Convert(item.PrinterType, null, null, null).ToString();
                PrinterViewModel model = model1;
                this.Printers.Add(model);
                if (item.PrinterType.HasFlag(PrinterType.Default) && !model.IsOffline)
                {
                    this.SelectedPrinter = model;
                }
            }
            if (this.SelectedPrinter == null)
            {
                Func<PrinterViewModel, bool> predicate = <>c.<>9__51_0;
                if (<>c.<>9__51_0 == null)
                {
                    Func<PrinterViewModel, bool> local1 = <>c.<>9__51_0;
                    predicate = <>c.<>9__51_0 = x => !x.IsOffline;
                }
                this.SelectedPrinter = this.Printers.FirstOrDefault<PrinterViewModel>(predicate);
            }
        }

        public bool IsPrintersAvailable
        {
            get => 
                (bool) base.GetValue(IsPrintersAvailableProperty);
            protected set => 
                base.SetValue(IsPrintersAvailablePropertyKey, value);
        }

        public string PrintersErrorMessage
        {
            get => 
                (string) base.GetValue(PrintersErrorMessageProperty);
            protected set => 
                base.SetValue(PrintersErrorMessagePropertyKey, value);
        }

        public IDocumentPreviewModel Model
        {
            get => 
                (IDocumentPreviewModel) base.GetValue(ModelProperty);
            set => 
                base.SetValue(ModelProperty, value);
        }

        public PrinterViewModel SelectedPrinter
        {
            get => 
                (PrinterViewModel) base.GetValue(SelectedPrinterProperty);
            set => 
                base.SetValue(SelectedPrinterProperty, value);
        }

        public List<PrinterViewModel> Printers
        {
            get => 
                (List<PrinterViewModel>) base.GetValue(PrintersProperty);
            protected set => 
                base.SetValue(PrintersPropertyKey, value);
        }

        public BackstagePreviewOptionsMode OptionsMode
        {
            get => 
                (BackstagePreviewOptionsMode) base.GetValue(OptionsModeProperty);
            set => 
                base.SetValue(OptionsModeProperty, value);
        }

        public bool IsPrintOptionsVisible
        {
            get => 
                (bool) base.GetValue(IsPrintOptionsVisibleProperty);
            protected set => 
                base.SetValue(IsPrintOptionsVisiblePropertyKey, value);
        }

        public string SelectedFormat
        {
            get => 
                (string) base.GetValue(SelectedFormatProperty);
            set => 
                base.SetValue(SelectedFormatProperty, value);
        }

        public IEnumerable<string> ExportFormats
        {
            get => 
                (IEnumerable<string>) base.GetValue(ExportFormatsProperty);
            protected set => 
                base.SetValue(ExportFormatsPropertyKey, value);
        }

        public object CustomSettingsHeader
        {
            get => 
                base.GetValue(CustomSettingsHeaderProperty);
            set => 
                base.SetValue(CustomSettingsHeaderProperty, value);
        }

        public object CustomSettings
        {
            get => 
                base.GetValue(CustomSettingsProperty);
            set => 
                base.SetValue(CustomSettingsProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BackstageDocumentPreview.<>c <>9 = new BackstageDocumentPreview.<>c();
            public static Func<PrinterViewModel, bool> <>9__51_0;

            internal void <.cctor>b__0_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BackstageDocumentPreview) d).IsPrintOptionsVisible = ((BackstageDocumentPreview) d).OptionsMode == BackstagePreviewOptionsMode.Printing;
            }

            internal bool <LoadPrinters>b__51_0(PrinterViewModel x) => 
                !x.IsOffline;
        }
    }
}

