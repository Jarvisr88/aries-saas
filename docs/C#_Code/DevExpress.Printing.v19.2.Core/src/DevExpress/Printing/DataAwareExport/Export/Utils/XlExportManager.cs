namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using DevExpress.Office.Crypto;
    using DevExpress.XtraExport.Csv;
    using DevExpress.XtraExport.Xls;
    using DevExpress.XtraExport.Xlsx;
    using DevExpress.XtraPrinting;
    using System;

    public class XlExportManager
    {
        private IXlExport exporterCore;
        private IDataAwareExportOptions optionsCore;

        public XlExportManager(IDataAwareExportOptions options)
        {
            this.optionsCore = options;
        }

        protected void CreateExporter()
        {
            ExportTarget exportTarget = this.Options.ExportTarget;
            if (exportTarget == ExportTarget.Xls)
            {
                this.exporterCore = new XlsDataAwareExporter();
            }
            else if (exportTarget == ExportTarget.Xlsx)
            {
                this.exporterCore = new XlsxDataAwareExporter();
            }
            else
            {
                if (exportTarget != ExportTarget.Csv)
                {
                    throw new NotFiniteNumberException("only Xls, Xlsx and Csv allowed");
                }
                CsvDataAwareExporter exporter = new CsvDataAwareExporter {
                    Options = { 
                        Encoding = this.Options.CSVEncoding,
                        WritePreamble = this.Options.WritePreamble
                    }
                };
                CsvExportOptionsEx options = this.Options as CsvExportOptionsEx;
                if (options != null)
                {
                    exporter.Options.QuoteTextValues = options.QuoteStringsWithSeparators ? CsvQuotation.Auto : CsvQuotation.Never;
                    exporter.Options.PreventCsvInjection = options.RequireEncodeExecutableContent;
                }
                if (!string.IsNullOrEmpty(this.Options.CSVSeparator))
                {
                    exporter.Options.ValueSeparatorString = this.Options.CSVSeparator;
                }
                this.exporterCore = exporter;
            }
        }

        protected void SetDocumentOptions(IXlDocument document)
        {
            if (document.Options != null)
            {
                document.Options.Culture = this.Options.DocumentCulture;
            }
            IXlDocumentOptionsEx options = document.Options as IXlDocumentOptionsEx;
            if (options != null)
            {
                options.TruncateStringsToMaxLength = true;
                options.SuppressEmptyStrings = this.Options.SuppressEmptyStrings;
            }
        }

        protected void SetDocumentProperties(IXlDocument document)
        {
            XlExportOptionsBase base2 = this.Options as XlExportOptionsBase;
            if (base2 != null)
            {
                XlDocumentOptions documentOptions = base2.DocumentOptions;
                document.Properties.Application = documentOptions.Application;
                document.Properties.Author = documentOptions.Author;
                document.Properties.Category = documentOptions.Category;
                document.Properties.Company = documentOptions.Company;
                document.Properties.Description = documentOptions.Comments;
                document.Properties.Keywords = documentOptions.Tags;
                document.Properties.Subject = documentOptions.Subject;
                document.Properties.Title = documentOptions.Title;
                document.Properties.Custom["DXVersion"] = "19.2.9.0";
            }
        }

        public IDataAwareExportOptions Options =>
            this.optionsCore;

        public IXlExport Exporter
        {
            get
            {
                if (this.exporterCore == null)
                {
                    this.CreateExporter();
                }
                return this.exporterCore;
            }
            set => 
                this.exporterCore = value;
        }

        protected DevExpress.Office.Crypto.EncryptionOptions EncryptionOptions
        {
            get
            {
                XlExportOptionsBase options = this.Options as XlExportOptionsBase;
                if (options == null)
                {
                    return null;
                }
                if (string.IsNullOrEmpty(options.EncryptionOptions.Password))
                {
                    return null;
                }
                DevExpress.Office.Crypto.EncryptionOptions options1 = new DevExpress.Office.Crypto.EncryptionOptions();
                options1.Type = (ModelEncryptionType) options.EncryptionOptions.Type;
                options1.Password = options.EncryptionOptions.Password;
                return options1;
            }
        }
    }
}

