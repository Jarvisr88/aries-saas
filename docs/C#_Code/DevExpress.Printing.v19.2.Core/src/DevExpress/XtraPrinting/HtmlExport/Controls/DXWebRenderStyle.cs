namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using DevExpress.XtraPrinting.HtmlExport;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct DXWebRenderStyle
    {
        public string name;
        public string value;
        public DXHtmlTextWriterStyle key;
    }
}

