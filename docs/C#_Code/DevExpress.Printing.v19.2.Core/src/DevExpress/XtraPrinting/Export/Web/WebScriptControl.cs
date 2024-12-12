namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class WebScriptControl : DXHtmlControl
    {
        private const string clientScriptStart1 = "<script type=\"text/javascript\"";
        private const string clientScriptStart2 = ">\r\n// <![CDATA[\r\n";
        private const string clientScriptEnd = "// ]]>\r\n</script>";
        protected SortedList<string, string> scriptHT;

        public WebScriptControl() : base(DXHtmlTextWriterTag.Script)
        {
            this.scriptHT = new SortedList<string, string>();
        }

        private string BuildScript()
        {
            StringBuilder builder = new StringBuilder();
            foreach (string str in this.scriptHT.Values)
            {
                builder.AppendLine(str);
            }
            return builder.ToString();
        }

        public virtual void ClearContent()
        {
            this.scriptHT.Clear();
        }

        protected virtual string CreateId(string script) => 
            string.Empty;

        public void RegisterClientScriptBlock(string key, string script)
        {
            if (!string.IsNullOrEmpty(script))
            {
                this.scriptHT.Add(key, script);
            }
        }

        protected internal override void Render(DXHtmlTextWriter writer)
        {
            this.RenderScripts(writer);
        }

        protected void RenderScripts(DXHtmlTextWriter writer)
        {
            if (this.scriptHT.Values.Count != 0)
            {
                string script = this.BuildScript();
                this.WriteScriptStart(writer, script);
                writer.Write(script);
                writer.WriteLine("// ]]>\r\n</script>");
            }
        }

        private void WriteScriptStart(DXHtmlTextWriter writer, string script)
        {
            writer.Write("<script type=\"text/javascript\"");
            string str = this.CreateId(script);
            if (!string.IsNullOrEmpty(str))
            {
                writer.Write(" id=\"" + str + "\"");
            }
            writer.WriteLine(">\r\n// <![CDATA[\r\n");
        }
    }
}

