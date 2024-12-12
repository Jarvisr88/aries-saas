namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    public abstract class XlExportOptionsBase : PageByPageExportOptionsBase
    {
        private static readonly char[] illegalCharacters = new char[] { '\0', '\x0003', ':', '\\', '*', '?', '/', '[', ']' };
        private static readonly byte validLength = 0x1f;
        private const string DefaultSheetName = "Sheet";
        private const DevExpress.XtraPrinting.TextExportMode DefaultTextExportMode = DevExpress.XtraPrinting.TextExportMode.Value;
        private const XlIgnoreErrors DefaultIgnoreErrors = XlIgnoreErrors.None;
        private DefaultBoolean rightToLeftDocument;
        private bool fitToPrintedPageWidth;
        private bool fitToPrintedPageHeight;
        private bool exportHyperlinks;
        private bool showGridLines;
        private string sheetName;
        private DevExpress.XtraPrinting.TextExportMode textExportMode;
        private XlIgnoreErrors ignoreErrors;
        private bool rawDataMode;
        private XlDocumentOptions documentOptions;
        private XlEncryptionOptions encryptionOptions;
        private bool richTextRunsEnabled;

        public XlExportOptionsBase() : this(DevExpress.XtraPrinting.TextExportMode.Value)
        {
        }

        public XlExportOptionsBase(DevExpress.XtraPrinting.TextExportMode textExportMode)
        {
            this.rightToLeftDocument = DefaultBoolean.Default;
            this.exportHyperlinks = true;
            this.sheetName = "Sheet";
            this.documentOptions = new XlDocumentOptions();
            this.encryptionOptions = new XlEncryptionOptions();
            this.textExportMode = textExportMode;
        }

        protected XlExportOptionsBase(XlExportOptionsBase source) : base(source)
        {
            this.rightToLeftDocument = DefaultBoolean.Default;
            this.exportHyperlinks = true;
            this.sheetName = "Sheet";
            this.documentOptions = new XlDocumentOptions();
            this.encryptionOptions = new XlEncryptionOptions();
        }

        public XlExportOptionsBase(DevExpress.XtraPrinting.TextExportMode textExportMode, bool showGridLines) : this(textExportMode)
        {
            this.showGridLines = showGridLines;
        }

        public XlExportOptionsBase(DevExpress.XtraPrinting.TextExportMode textExportMode, bool showGridLines, bool exportHyperlinks) : this(textExportMode, showGridLines)
        {
            this.exportHyperlinks = exportHyperlinks;
        }

        public XlExportOptionsBase(DevExpress.XtraPrinting.TextExportMode textExportMode, bool showGridLines, bool exportHyperlinks, bool fitToPrintedPageWidth) : this(textExportMode, showGridLines, exportHyperlinks)
        {
            this.fitToPrintedPageWidth = fitToPrintedPageWidth;
        }

        public XlExportOptionsBase(DevExpress.XtraPrinting.TextExportMode textExportMode, bool showGridLines, bool exportHyperlinks, bool fitToPrintedPageWidth, DefaultBoolean rightToLeftDocument) : this(textExportMode, showGridLines, exportHyperlinks, fitToPrintedPageWidth)
        {
            this.rightToLeftDocument = rightToLeftDocument;
        }

        public override void Assign(ExportOptionsBase source)
        {
            base.Assign(source);
            XlExportOptionsBase base2 = (XlExportOptionsBase) source;
            this.textExportMode = base2.TextExportMode;
            this.showGridLines = base2.ShowGridLines;
            this.exportHyperlinks = base2.ExportHyperlinks;
            this.rawDataMode = base2.RawDataMode;
            this.sheetName = ValidateSheetName(base2.SheetName);
            this.fitToPrintedPageWidth = base2.FitToPrintedPageWidth;
            this.fitToPrintedPageHeight = base2.fitToPrintedPageHeight;
            this.rightToLeftDocument = base2.RightToLeftDocument;
            this.documentOptions = base2.DocumentOptions;
            this.encryptionOptions = base2.EncryptionOptions;
            this.ignoreErrors = base2.IgnoreErrors;
        }

        protected internal override bool ShouldSerialize() => 
            (this.textExportMode != DevExpress.XtraPrinting.TextExportMode.Value) || (!this.exportHyperlinks || (this.showGridLines || (this.rawDataMode || (this.fitToPrintedPageWidth || (this.fitToPrintedPageHeight || ((this.rightToLeftDocument != DefaultBoolean.Default) || ((this.sheetName != "Sheet") || ((this.ignoreErrors != XlIgnoreErrors.None) || (this.ShouldSerializeDocumentOptions() || (this.ShouldSerializeEncryptionOptions() || base.ShouldSerialize()))))))))));

        private bool ShouldSerializeDocumentOptions() => 
            this.DocumentOptions.ShouldSerialize();

        private bool ShouldSerializeEncryptionOptions() => 
            this.EncryptionOptions.ShouldSerialize();

        private static string ValidateSheetName(string sheetName)
        {
            string str = sheetName;
            if (string.IsNullOrEmpty(str))
            {
                return "Sheet";
            }
            if (str[0] == '\'')
            {
                str = str.Remove(0, 1);
            }
            if (str[str.Length - 1] == '\'')
            {
                str = str.Remove(str.Length - 1, 1);
            }
            foreach (char ch in illegalCharacters)
            {
                int index = str.IndexOf(ch);
                if (index > -1)
                {
                    str = str.Replace(ch.ToString(), "");
                }
            }
            if (str.Length > validLength)
            {
                str = str.Substring(0, validLength);
            }
            return str;
        }

        [Description("Enables the mode that produces simple tabular data without graphic elements, style and appearance settings."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlsExportOptions.RawDataMode"), DefaultValue(false), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty]
        public bool RawDataMode
        {
            get => 
                this.rawDataMode;
            set => 
                this.rawDataMode = value;
        }

        [Description("Gets or sets a value indicating whether the cells in the resulting XLS document should use the same formatting as the original document."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlExportOptionsBase.TextExportMode"), DefaultValue(0), XtraSerializableProperty]
        public DevExpress.XtraPrinting.TextExportMode TextExportMode
        {
            get => 
                this.textExportMode;
            set => 
                this.textExportMode = value;
        }

        [Description("Specifies whether or not hyperlinks should be exported to Excel."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlsExportOptions.ExportHyperlinks"), DefaultValue(true), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty]
        public bool ExportHyperlinks
        {
            get => 
                this.exportHyperlinks;
            set => 
                this.exportHyperlinks = value;
        }

        [Description("Gets or sets a value indicating whether the grid lines should be visible in the resulting XLS file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlsExportOptions.ShowGridLines"), DefaultValue(false), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty]
        public bool ShowGridLines
        {
            get => 
                this.showGridLines;
            set => 
                this.showGridLines = value;
        }

        [Description("Gets or sets whether the output document should be fit to the page width when printed."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlExportOptionsBase.FitToPrintedPageWidth"), DefaultValue(false), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty]
        public bool FitToPrintedPageWidth
        {
            get => 
                this.fitToPrintedPageWidth;
            set => 
                this.fitToPrintedPageWidth = value;
        }

        [Description("Gets or sets whether the output document should be fit to the page height when printed."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlExportOptionsBase.FitToPrintedPageHeight"), DefaultValue(false), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty]
        public bool FitToPrintedPageHeight
        {
            get => 
                this.fitToPrintedPageHeight;
            set => 
                this.fitToPrintedPageHeight = value;
        }

        [Description("Gets or sets whether the layout of the resulting XLS document should be aligned to support locales using right-to-left fonts."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlExportOptionsBase.RightToLeftDocument"), DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter)), XtraSerializableProperty]
        public DefaultBoolean RightToLeftDocument
        {
            get => 
                this.rightToLeftDocument;
            set => 
                this.rightToLeftDocument = value;
        }

        [Description("Gets or sets a name of the sheet in the created XLS file to which a document is exported."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlsExportOptions.SheetName"), DefaultValue("Sheet"), Localizable(true), XtraSerializableProperty]
        public string SheetName
        {
            get => 
                this.sheetName;
            set => 
                this.sheetName = ValidateSheetName(value);
        }

        [Description("Provides access to options to be embedded as the resulting XLS or XLSX file's Document Properties."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlsExportOptions.DocumentOptions"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public XlDocumentOptions DocumentOptions =>
            this.documentOptions;

        [Description("Provides access to the XLS and XLSX file encryption options."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlExportOptionsBase.EncryptionOptions"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content)]
        public XlEncryptionOptions EncryptionOptions =>
            this.encryptionOptions;

        [Description("Specifies the document errors to be ignored in a resulting Excel file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.XlExportOptionsBase.IgnoreErrors"), DefaultValue(0), XtraSerializableProperty]
        public XlIgnoreErrors IgnoreErrors
        {
            get => 
                this.ignoreErrors;
            set => 
                this.ignoreErrors = value;
        }

        internal bool RichTextRunsEnabled
        {
            get => 
                this.richTextRunsEnabled;
            set => 
                this.richTextRunsEnabled = value;
        }
    }
}

