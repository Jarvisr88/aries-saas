namespace DevExpress.Office.Export.Html
{
    using DevExpress.Office.Utils;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [ToolboxItem(false)]
    public class DXHtmlClipboardFragment : EmptyWebControl
    {
        protected int GetPosition(DXHtmlTextWriter writer)
        {
            ChunkedStringBuilderClipboardWriter innerWriter = writer.InnerWriter as ChunkedStringBuilderClipboardWriter;
            return ((innerWriter == null) ? -1 : innerWriter.ByteCount);
        }

        protected override void RenderBeginTag(DXHtmlTextWriter writer)
        {
            writer.Write("<!--StartFragment-->");
            this.StartFragment = this.GetPosition(writer);
        }

        protected override void RenderEndTag(DXHtmlTextWriter writer)
        {
            this.EndFragment = this.GetPosition(writer);
            writer.Write("<!--EndFragment-->");
        }

        public int StartFragment { get; private set; }

        public int EndFragment { get; private set; }
    }
}

