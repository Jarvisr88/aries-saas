namespace DevExpress.XtraPrinting
{
    using DevExpress.Export;
    using DevExpress.Printing;
    using DevExpress.Utils;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Text;

    [DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.CsvExportOptions")]
    public class CsvExportOptions : TextExportOptionsBase, IXtraSupportShouldSerialize
    {
        private const bool DefaultFollowReportLayout = true;
        private const bool DefaultSkipEmptyRows = true;
        private const bool DefaultSkipEmptyColumns = true;
        private static bool followReportLayout = true;
        private bool skipEmptyRows;
        private bool skipEmptyColumns;
        private bool useCustomSeparator;
        private DefaultBoolean encodeExecutableContent;

        public CsvExportOptions() : this(DefaultCsvSeparator, TextExportOptionsBase.DefaultEncoding)
        {
        }

        private CsvExportOptions(CsvExportOptions source) : base(source)
        {
            this.skipEmptyRows = true;
            this.skipEmptyColumns = true;
            this.encodeExecutableContent = DefaultBoolean.Default;
        }

        public CsvExportOptions(string separator, Encoding encoding) : this(separator, encoding, TextExportMode.Text)
        {
        }

        public CsvExportOptions(string separator, Encoding encoding, TextExportMode textExportMode) : this(separator, encoding, textExportMode, true, true)
        {
        }

        public CsvExportOptions(string separator, Encoding encoding, TextExportMode textExportMode, bool skipEmptyRows, bool skipEmptyColumns) : base(separator, encoding, textExportMode)
        {
            this.skipEmptyRows = true;
            this.skipEmptyColumns = true;
            this.encodeExecutableContent = DefaultBoolean.Default;
            this.skipEmptyRows = skipEmptyRows;
            this.skipEmptyColumns = skipEmptyColumns;
        }

        public override void Assign(ExportOptionsBase source)
        {
            base.Assign(source);
            CsvExportOptions options = (CsvExportOptions) source;
            this.skipEmptyColumns = options.SkipEmptyColumns;
            this.skipEmptyRows = options.SkipEmptyRows;
        }

        protected internal override ExportOptionsBase CloneOptions() => 
            new CsvExportOptions(this);

        bool IXtraSupportShouldSerialize.ShouldSerialize(string propertyName) => 
            (propertyName == "UseCustomSeparator") ? this.ShouldSerializeUseCustomSeparator() : ((propertyName == "Separator") ? base.ShouldSerializeSeparator() : true);

        public override string GetActualSeparator() => 
            !string.IsNullOrEmpty(this.Separator) ? this.Separator : CultureInfo.CurrentCulture.GetListSeparator().ToString();

        protected override bool GetDefaultQuoteStringsWithSeparators() => 
            true;

        protected override string GetDefaultSeparator() => 
            DefaultCsvSeparator;

        protected internal override bool ShouldSerialize() => 
            base.ShouldSerialize() || (!this.skipEmptyColumns || !this.skipEmptyRows);

        private bool ShouldSerializeUseCustomSeparator() => 
            false;

        [Browsable(false), DefaultValue(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static bool FollowReportLayout
        {
            get => 
                followReportLayout;
            set => 
                followReportLayout = value;
        }

        [Description("Specifies whether or not to include the empty rows into the resulting CSV file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.CsvExportOptions.SkipEmptyRows"), DefaultValue(true), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty]
        public bool SkipEmptyRows
        {
            get => 
                this.skipEmptyRows;
            set => 
                this.skipEmptyRows = value;
        }

        [Description("Specifies whether or not to include the empty columns into the resulting CSV file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.CsvExportOptions.SkipEmptyColumns"), DefaultValue(true), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty]
        public bool SkipEmptyColumns
        {
            get => 
                this.skipEmptyColumns;
            set => 
                this.skipEmptyColumns = value;
        }

        [Description("Gets or sets the symbol(s) to separate text elements when a document is exported to a Text-based file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.TextExportOptionsBase.Separator"), TypeConverter(typeof(CsvSeparatorConverter)), Localizable(true), RefreshProperties(RefreshProperties.All), XtraSerializableProperty]
        public override string Separator
        {
            get => 
                base.Separator;
            set
            {
                base.Separator = value;
                if (value != DefaultCsvSeparator)
                {
                    this.UseCustomSeparator = true;
                }
            }
        }

        [Description("Specifies whether to use the default system or a custom separator character for CSV export."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.CsvExportOptions.UseCustomSeparator"), RefreshProperties(RefreshProperties.All), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty(XtraSerializationVisibility.Hidden), TypeConverter(typeof(BooleanTypeConverter)), EditorBrowsable(EditorBrowsableState.Never)]
        public bool UseCustomSeparator
        {
            get => 
                this.useCustomSeparator;
            set
            {
                this.useCustomSeparator = value;
                if (!value)
                {
                    base.ResetSeparator();
                }
            }
        }

        [Description("Gets or sets whether to encode the potentially dangerous content from a control or document when it is exported to a CSV file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.CsvExportOptions.EncodeExecutableContent"), DefaultValue(2), TypeConverter(typeof(DefaultBooleanConverter)), Browsable(false), XtraSerializableProperty]
        public DefaultBoolean EncodeExecutableContent
        {
            get => 
                this.encodeExecutableContent;
            set => 
                this.encodeExecutableContent = value;
        }

        internal bool RequireEncodeExecutableContent
        {
            get
            {
                DefaultBoolean encodeExecutableContent = this.EncodeExecutableContent;
                if (encodeExecutableContent == DefaultBoolean.Default)
                {
                    encodeExecutableContent = ExportSettings.EncodeCsvExecutableContent;
                }
                if (encodeExecutableContent == DefaultBoolean.Default)
                {
                    encodeExecutableContent = DefaultBoolean.False;
                }
                return (encodeExecutableContent != DefaultBoolean.False);
            }
        }

        private static string DefaultCsvSeparator =>
            string.Empty;
    }
}

