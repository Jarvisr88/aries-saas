namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class HypertextExportInfoContainer
    {
        public XlRichTextRun TextRun { get; set; }

        public string Link { get; set; }

        public System.Drawing.Image Image { get; set; }
    }
}

