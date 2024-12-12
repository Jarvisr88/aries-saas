namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using DevExpress.XtraPrinting.HtmlExport;
    using System;

    public class DXHtmlGenericControl : DXHtmlContainerControl
    {
        public DXHtmlGenericControl() : this(DXHtmlTextWriterTag.Span)
        {
        }

        public DXHtmlGenericControl(DXHtmlTextWriterTag tag) : base(tag)
        {
        }
    }
}

