namespace DevExpress.Office.Export.Html
{
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.ComponentModel;

    [ToolboxItem(false)]
    public class EmptyWebControl : DXHtmlGenericControl
    {
        protected override void RenderBeginTag(DXHtmlTextWriter writer)
        {
        }

        protected override void RenderEndTag(DXHtmlTextWriter writer)
        {
        }
    }
}

