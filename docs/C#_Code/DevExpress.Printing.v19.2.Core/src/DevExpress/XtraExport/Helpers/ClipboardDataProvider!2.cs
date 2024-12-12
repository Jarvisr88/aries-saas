namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export;
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.Windows.Forms;

    public abstract class ClipboardDataProvider<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        private ClipboardExportManagerBase<TCol, TRow> manager;

        public ClipboardDataProvider(ClipboardExportManagerBase<TCol, TRow> manager)
        {
            this.manager = manager;
        }

        protected virtual ClipboardOptions GetClipboardOptions(ClipboardDataFormat format, ClipboardDataProviderOptions options) => 
            new ClipboardOptions(true) { 
                AllowCsvFormat = format.HasFlag(ClipboardDataFormat.Csv) ? DefaultBoolean.True : DefaultBoolean.False,
                AllowExcelFormat = format.HasFlag(ClipboardDataFormat.Excel) ? DefaultBoolean.True : DefaultBoolean.False,
                AllowHtmlFormat = format.HasFlag(ClipboardDataFormat.Html) ? DefaultBoolean.True : DefaultBoolean.False,
                AllowRtfFormat = format.HasFlag(ClipboardDataFormat.RichText) ? DefaultBoolean.True : DefaultBoolean.False,
                AllowTxtFormat = (format.HasFlag(ClipboardDataFormat.Text) | format.HasFlag(ClipboardDataFormat.UnicodeText)) ? DefaultBoolean.True : DefaultBoolean.False,
                CopyCollapsedData = options.CopyCollapsedData ? DefaultBoolean.True : DefaultBoolean.False,
                CopyColumnHeaders = options.CopyColumnHeaders ? DefaultBoolean.True : DefaultBoolean.False
            };

        public string GetCsvData() => 
            this.GetCsvDataCore(this.GetDefaultOptions());

        protected string GetCsvDataCore(ClipboardDataProviderOptions options)
        {
            using (StreamReader reader = new StreamReader(this.GetDataCore(ClipboardDataFormat.Csv, options).GetData(DataFormats.CommaSeparatedValue) as MemoryStream))
            {
                reader.BaseStream.Seek(0L, SeekOrigin.Begin);
                return reader.ReadToEnd();
            }
        }

        public DataObject GetData() => 
            this.GetDataCore(ClipboardDataFormat.AllSupported, this.GetDefaultOptions());

        public DataObject GetData(ClipboardDataFormat format) => 
            this.GetDataCore(format, this.GetDefaultOptions());

        protected DataObject GetDataCore(ClipboardDataFormat format, ClipboardDataProviderOptions options)
        {
            ClipboardOptions clipboardOptions = this.GetClipboardOptions(format, options);
            this.manager.AssignOptions(clipboardOptions);
            DataObject dataObject = new DataObject();
            this.manager.SetClipboardData(dataObject);
            return dataObject;
        }

        protected abstract ClipboardDataProviderOptions GetDefaultOptions();
        public MemoryStream GetExcelData() => 
            this.GetExcelDataCore(this.GetDefaultOptions());

        protected MemoryStream GetExcelDataCore(ClipboardDataProviderOptions options) => 
            this.GetDataCore(ClipboardDataFormat.Excel, options).GetData("Biff8") as MemoryStream;

        public string GetHtmlData() => 
            this.GetHtmlDataCore(this.GetDefaultOptions());

        protected string GetHtmlDataCore(ClipboardDataProviderOptions options) => 
            this.GetDataCore(ClipboardDataFormat.Html, options).GetData(DataFormats.Html) as string;

        public string GetRichTextData() => 
            this.GetRichTextDataCore(this.GetDefaultOptions());

        protected string GetRichTextDataCore(ClipboardDataProviderOptions options) => 
            this.GetDataCore(ClipboardDataFormat.RichText, options).GetData(DataFormats.Rtf) as string;

        public string GetTextData() => 
            this.GetTextDataCore(this.GetDefaultOptions());

        protected string GetTextDataCore(ClipboardDataProviderOptions options) => 
            this.GetDataCore(ClipboardDataFormat.Text, options).GetData(DataFormats.Text) as string;

        public string GetUnicodeTextData() => 
            this.GetUnicodeTextDataCore(this.GetDefaultOptions());

        protected string GetUnicodeTextDataCore(ClipboardDataProviderOptions options) => 
            this.GetDataCore(ClipboardDataFormat.UnicodeText, options).GetData(DataFormats.Text) as string;
    }
}

