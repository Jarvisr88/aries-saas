namespace DevExpress.Printing.Native.PrintEditor
{
    using DevExpress.Printing;
    using DevExpress.Printing.Native;
    using DevExpress.XtraPrinting.Localization;
    using System;
    using System.Drawing.Printing;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    public class PrintEditorController
    {
        private string oldPrinterName;
        private IPrintForm form;
        private PrintDocument document;

        public PrintEditorController(IPrintForm form)
        {
            this.form = form;
        }

        private void ApplyPrinterSettingFromForm()
        {
            this.PrinterSettings.Copies = this.form.Copies;
            this.PrinterSettings.Collate = this.form.Collate;
            if (this.form.CanDuplex)
            {
                this.PrinterSettings.Duplex = this.form.Duplex;
            }
            if (!string.IsNullOrEmpty(this.form.PaperSource))
            {
                foreach (PaperSource source in this.PrinterSettings.PaperSources)
                {
                    if (source.SourceName == this.form.PaperSource)
                    {
                        this.PageSettings.PaperSource = source;
                        break;
                    }
                }
            }
        }

        private void ApplyPrinterSettingToForm()
        {
            this.form.Copies = this.PrinterSettings.Copies;
            this.form.Collate = this.PrinterSettings.Collate;
            if (this.form.CanDuplex)
            {
                this.form.Duplex = this.PrinterSettings.Duplex;
            }
            this.form.PaperSource = this.PageSettings.PaperSource.SourceName;
        }

        public void AssignPrinterSettings()
        {
            this.PrinterSettings.Copies = this.form.Copies;
            this.PrinterSettings.Collate = this.form.Collate;
            this.PrinterSettings.PrintToFile = this.form.PrintToFile;
            if (this.form.PrintToFile)
            {
                this.PrinterSettings.PrintFileName = this.form.PrintFileName;
            }
            if (!string.IsNullOrEmpty(this.form.PaperSource))
            {
                foreach (PaperSource source in this.PrinterSettings.PaperSources)
                {
                    if (source.SourceName == this.form.PaperSource)
                    {
                        this.PageSettings.PaperSource = source;
                        break;
                    }
                }
            }
            if (this.form.PrintRange == PrintRange.SomePages)
            {
                if (this.document is IPrintDocumentExtension)
                {
                    (this.document as IPrintDocumentExtension).PageRange = this.form.PageRangeText;
                }
                PageScope scope = new PageScope(this.form.PageRangeText, this.PrinterSettings.MaximumPage);
                this.PrinterSettings.FromPage = scope.FromPage;
                this.PrinterSettings.ToPage = scope.ToPage;
            }
            this.PrinterSettings.PrintRange = this.form.PrintRange;
            if (this.form.CanDuplex && (this.form.Duplex != Duplex.Default))
            {
                this.PrinterSettings.Duplex = this.form.Duplex;
            }
        }

        public void CancelPrinterSettings()
        {
            this.PrinterSettings.PrinterName = this.oldPrinterName;
        }

        private static bool IsValidPath(string path)
        {
            try
            {
                using (File.Create(path))
                {
                }
                File.Delete(path);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void LoadForm(PrinterItemContainer printerItemContainer)
        {
            this.document = this.form.Document;
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            string printerName = this.document.PrinterSettings.PrinterName;
            bool isValidPrinterSettings = this.IsValidPrinterSettings;
            this.oldPrinterName = printerName;
            if (!isValidPrinterSettings && !string.IsNullOrEmpty(printerItemContainer.DefaultPrinterName))
            {
                printerName = printerItemContainer.DefaultPrinterName;
                this.oldPrinterName = printerName;
                this.PrinterSettings.PrinterName = printerName;
                isValidPrinterSettings = this.IsValidPrinterSettings;
            }
            foreach (PrinterItem item in printerItemContainer.Items)
            {
                this.form.AddPrinterItem(item);
            }
            this.form.SetSelectedPrinter(printerName);
            if (this.document is IPrintDocumentExtension)
            {
                this.form.PageRangeText = (this.document as IPrintDocumentExtension).PageRange;
            }
            else if (this.PrinterSettings.MaximumPage != 1)
            {
                this.form.PageRangeText = new PageScope(this.PrinterSettings.FromPage, this.PrinterSettings.ToPage).PageRange;
            }
            else
            {
                this.form.PageRangeText = string.Empty;
                this.form.AllowSomePages = false;
            }
            PrintHelper.GetPrintRangeAsync(this.PrinterSettings).ContinueWith(delegate (Task<PrintRange> t) {
                bool? catchException = null;
                this.form.SetPrintRange(SafeGetter.Get<PrintRange>(t, PrintRange.AllPages, catchException));
            }, scheduler);
            PrintHelper.GetPrintToFileAsync(this.PrinterSettings).ContinueWith<bool>(delegate (Task<bool> t) {
                bool flag;
                bool? catchException = null;
                this.form.PrintToFile = flag = SafeGetter.Get<bool>(t, false, catchException);
                return flag;
            }, scheduler);
            PrintHelper.GetPrintFileNameAsync(this.PrinterSettings).ContinueWith<string>(delegate (Task<string> t) {
                string str;
                bool? catchException = null;
                this.form.PrintFileName = str = SafeGetter.Get<string>(t, string.Empty, catchException);
                return str;
            }, scheduler);
            if (isValidPrinterSettings)
            {
                PrintHelper.GetCollateAsync(this.PrinterSettings).ContinueWith<bool>(delegate (Task<bool> t) {
                    bool flag;
                    bool? catchException = null;
                    this.form.Collate = flag = SafeGetter.Get<bool>(t, true, catchException);
                    return flag;
                }, scheduler);
                PrintHelper.GetCopiesAsync(this.PrinterSettings).ContinueWith<short>(delegate (Task<short> t) {
                    short num;
                    bool? catchException = null;
                    this.form.Copies = num = SafeGetter.Get<short>(t, 1, catchException);
                    return num;
                }, scheduler);
            }
        }

        private void RestorePrinterSetting(PrinterSettingsStub settingsStub)
        {
            this.PrinterSettings.Copies = settingsStub.Copies;
            this.PrinterSettings.Collate = settingsStub.Collate;
            this.PageSettings.PaperSource = settingsStub.PaperSource;
            if (settingsStub.CanDuplex)
            {
                this.PrinterSettings.Duplex = settingsStub.Duplex;
            }
        }

        private PrinterSettingsStub SavePrinterSettings()
        {
            FieldInfo field = typeof(System.Drawing.Printing.PageSettings).GetField("paperSource", BindingFlags.NonPublic | BindingFlags.Instance);
            PaperSource paperSource = (field == null) ? this.PageSettings.PaperSource : ((PaperSource) field.GetValue(this.PageSettings));
            return (this.PrinterSettings.CanDuplex ? new PrinterSettingsStub(this.PrinterSettings.Copies, this.PrinterSettings.Collate, paperSource) : new PrinterSettingsStub(this.PrinterSettings.Copies, this.PrinterSettings.Collate, paperSource, this.PrinterSettings.Duplex));
        }

        public void ShowPrinterPreferences(IntPtr hwnd)
        {
            PrinterSettingsStub settingsStub = this.SavePrinterSettings();
            this.ApplyPrinterSettingFromForm();
            try
            {
                new PrinterPreferences().ShowPrinterProperties(this.PageSettings, hwnd);
                this.ApplyPrinterSettingToForm();
            }
            finally
            {
                this.RestorePrinterSetting(settingsStub);
            }
        }

        public static bool ValidateFilePath(string printFileName, out string messageText)
        {
            bool flag;
            messageText = null;
            if (string.IsNullOrEmpty(printFileName))
            {
                messageText = PreviewStringId.Msg_InvalidatePath.GetString();
                return false;
            }
            try
            {
                if (!File.Exists(printFileName))
                {
                    if (IsValidPath(printFileName))
                    {
                        return true;
                    }
                    messageText = PreviewStringId.Msg_InvalidatePath.GetString();
                    return false;
                }
                else
                {
                    messageText = PreviewStringId.Msg_FileAlreadyExists.GetString();
                    flag = true;
                }
            }
            catch
            {
                messageText = PreviewStringId.Msg_InvalidatePath.GetString();
                flag = false;
            }
            return flag;
        }

        private System.Drawing.Printing.PrinterSettings PrinterSettings =>
            this.document.PrinterSettings;

        private System.Drawing.Printing.PageSettings PageSettings =>
            this.document.DefaultPageSettings;

        private bool IsValidPrinterSettings =>
            this.PrinterSettings.IsValid;
    }
}

