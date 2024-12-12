namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.ComponentModel;
    using System.Text;

    [DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.TextExportOptions")]
    public class TextExportOptions : TextExportOptionsBase, IXtraSupportShouldSerialize
    {
        public const string DefaultSeparator = "\t";

        public TextExportOptions() : this("\t")
        {
        }

        protected TextExportOptions(TextExportOptions source) : base(source)
        {
        }

        public TextExportOptions(string separator)
        {
            this.Separator = separator;
        }

        public TextExportOptions(string separator, Encoding encoding) : base(separator, encoding)
        {
        }

        public TextExportOptions(string separator, Encoding encoding, TextExportMode textExportMode) : base(separator, encoding, textExportMode)
        {
        }

        protected internal override ExportOptionsBase CloneOptions() => 
            new TextExportOptions(this);

        bool IXtraSupportShouldSerialize.ShouldSerialize(string propertyName) => 
            (propertyName != "Separator") || base.ShouldSerializeSeparator();

        protected override bool GetDefaultQuoteStringsWithSeparators() => 
            false;

        protected override string GetDefaultSeparator() => 
            "\t";
    }
}

