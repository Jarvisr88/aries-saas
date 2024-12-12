namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class HtmlDocumentBuilderBase
    {
        protected SortedList<string, string> startupList = new SortedList<string, string>();

        public virtual void CreateDocumentCore(DXHtmlTextWriter writer, DXWebControlBase webControl, IImageRepository imageRepository)
        {
            this.RenderControl(webControl, writer);
            this.WriteStartupScript(writer);
            writer.Flush();
        }

        public void RegisterStartupScript(string key, string script)
        {
            if ((key.Length > 0) && (script.Length > 0))
            {
                this.startupList.Add(key, script);
            }
        }

        protected virtual void RenderControl(DXWebControlBase control, DXHtmlTextWriter writer)
        {
            control.RenderControl(writer);
        }

        protected virtual void WriteStartupScript(TextWriter writer)
        {
            foreach (string str in this.startupList.Values)
            {
                writer.WriteLine(str);
            }
        }
    }
}

