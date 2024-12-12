namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.IO;

    public class CompressedHtmlTextWriter : DXHtmlTextWriter
    {
        public CompressedHtmlTextWriter(TextWriter writer) : base(writer)
        {
            this.NewLine = "";
        }

        protected override void OutputTabs()
        {
        }

        public override void WriteAttribute(string name, string value, bool fEncode)
        {
            if ((name != "colspan") || (value != "1"))
            {
                base.WriteAttribute(name, value, fEncode);
            }
        }
    }
}

