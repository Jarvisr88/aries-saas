namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Text;

    public abstract class TextExportOptionsBase : ExportOptionsBase
    {
        private const BindingFlags PublicStaticFlags = (BindingFlags.Public | BindingFlags.Static);
        private const DevExpress.XtraPrinting.TextExportMode DefaultTextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
        private static readonly System.Text.Encoding defaultEncoding = DXEncoding.Default;
        private System.Text.Encoding encoding;
        private DevExpress.XtraPrinting.TextExportMode textExportMode;
        private string separator;
        private bool quoteStringsWithSeparators;

        protected TextExportOptionsBase() : this(defaultEncoding)
        {
        }

        protected TextExportOptionsBase(TextExportOptionsBase source) : base(source)
        {
            this.textExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
        }

        private TextExportOptionsBase(System.Text.Encoding encoding)
        {
            this.textExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
            this.Encoding = encoding;
            this.quoteStringsWithSeparators = this.GetDefaultQuoteStringsWithSeparators();
        }

        protected TextExportOptionsBase(string separator, System.Text.Encoding encoding) : this(encoding)
        {
            this.separator = separator;
        }

        protected TextExportOptionsBase(string separator, System.Text.Encoding encoding, DevExpress.XtraPrinting.TextExportMode textExportMode) : this(separator, encoding)
        {
            this.textExportMode = textExportMode;
        }

        public override void Assign(ExportOptionsBase source)
        {
            TextExportOptionsBase base2 = (TextExportOptionsBase) source;
            this.Encoding = base2.Encoding;
            this.separator = base2.Separator;
            this.quoteStringsWithSeparators = base2.QuoteStringsWithSeparators;
            this.textExportMode = base2.TextExportMode;
        }

        public virtual string GetActualSeparator() => 
            this.Separator;

        protected abstract bool GetDefaultQuoteStringsWithSeparators();
        protected abstract string GetDefaultSeparator();
        protected void ResetSeparator()
        {
            this.separator = this.GetDefaultSeparator();
        }

        protected internal override bool ShouldSerialize() => 
            this.ShouldSerializeEncoding() || (this.ShouldSerializeSeparator() || (this.ShouldSerializeQuoteStringsWithSeparators() || ((this.textExportMode != DevExpress.XtraPrinting.TextExportMode.Text) || (this.EncodingType != DevExpress.XtraPrinting.EncodingType.Default))));

        private bool ShouldSerializeEncoding() => 
            !Equals(this.encoding, defaultEncoding);

        private bool ShouldSerializeQuoteStringsWithSeparators() => 
            this.quoteStringsWithSeparators != this.GetDefaultQuoteStringsWithSeparators();

        protected bool ShouldSerializeSeparator() => 
            this.separator != this.GetDefaultSeparator();

        [Description("Gets or sets the encoding of the text-based file to which a report is exported."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.TextExportOptionsBase.Encoding"), TypeConverter(typeof(EncodingConverter)), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Text.Encoding Encoding
        {
            get => 
                this.encoding;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Encoding");
                }
                if (!ReferenceEquals(this.encoding, value))
                {
                    this.encoding = (System.Text.Encoding) value.Clone();
                }
            }
        }

        [DefaultValue(1), Description("Gets or sets a value indicating whether to use the formatting of the data fields in the bound dataset for the cells in the exported TXT, CSV, XLS or XLSX document."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.TextExportOptionsBase.TextExportMode"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), XtraSerializableProperty]
        public DevExpress.XtraPrinting.TextExportMode TextExportMode
        {
            get => 
                this.textExportMode;
            set => 
                this.textExportMode = value;
        }

        internal override DevExpress.XtraPrinting.ExportModeBase ExportModeBase =>
            DevExpress.XtraPrinting.ExportModeBase.SingleFile;

        [DefaultValue(0), Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Localizable(true), XtraSerializableProperty]
        public DevExpress.XtraPrinting.EncodingType EncodingType
        {
            get
            {
                foreach (PropertyInfo info in typeof(System.Text.Encoding).GetProperties(BindingFlags.Public | BindingFlags.Static))
                {
                    if (Equals(info.GetValue(null, null), this.encoding))
                    {
                        return (DevExpress.XtraPrinting.EncodingType) Enum.Parse(typeof(DevExpress.XtraPrinting.EncodingType), info.Name, false);
                    }
                }
                return DevExpress.XtraPrinting.EncodingType.Default;
            }
            set
            {
                PropertyInfo property = typeof(System.Text.Encoding).GetProperty(value.ToString(), BindingFlags.Public | BindingFlags.Static);
                if (property != null)
                {
                    this.Encoding = (System.Text.Encoding) property.GetValue(null, null);
                }
            }
        }

        [Description("Gets or sets the symbol(s) to separate text elements when a document is exported to a Text-based file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.TextExportOptionsBase.Separator"), TypeConverter(typeof(SeparatorConverter)), Localizable(true), RefreshProperties(RefreshProperties.All), XtraSerializableProperty]
        public virtual string Separator
        {
            get => 
                this.separator;
            set => 
                this.separator = value;
        }

        [Description("Gets or sets a value indicating whether a string with separators should be placed in quotation marks when a document is exported to a Text-based file."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.TextExportOptionsBase.QuoteStringsWithSeparators"), TypeConverter(typeof(QuoteStringsWithSeparatorsConverter)), XtraSerializableProperty]
        public bool QuoteStringsWithSeparators
        {
            get => 
                this.quoteStringsWithSeparators;
            set => 
                this.quoteStringsWithSeparators = value;
        }

        protected static System.Text.Encoding DefaultEncoding =>
            defaultEncoding;
    }
}

