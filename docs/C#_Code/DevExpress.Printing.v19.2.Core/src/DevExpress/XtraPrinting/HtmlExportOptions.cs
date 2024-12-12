namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using System;
    using System.ComponentModel;

    [DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.HtmlExportOptions")]
    public class HtmlExportOptions : HtmlExportOptionsBase
    {
        public HtmlExportOptions() : this("utf-8")
        {
        }

        protected HtmlExportOptions(HtmlExportOptions source) : base(source)
        {
        }

        public HtmlExportOptions(string characterSet) : this(characterSet, "Document")
        {
        }

        public HtmlExportOptions(string characterSet, string title) : this(characterSet, title, false)
        {
        }

        public HtmlExportOptions(string characterSet, string title, bool removeSecondarySymbols)
        {
            base.CharacterSet = characterSet;
            base.Title = title;
            base.RemoveSecondarySymbols = removeSecondarySymbols;
        }

        protected internal override ExportOptionsBase CloneOptions() => 
            new HtmlExportOptions(this);
    }
}

