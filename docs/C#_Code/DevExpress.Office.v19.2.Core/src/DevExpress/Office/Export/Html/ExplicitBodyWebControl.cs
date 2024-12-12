namespace DevExpress.Office.Export.Html
{
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;

    public class ExplicitBodyWebControl : DXHtmlGenericControl
    {
        private string bodyContent;

        public ExplicitBodyWebControl(string bodyContent) : base(DXHtmlTextWriterTag.Body)
        {
            this.bodyContent = bodyContent;
        }

        protected override void Render(DXHtmlTextWriter writer)
        {
            writer.Write(this.bodyContent);
        }
    }
}

