namespace DevExpress.Office.Export.Html
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.Export.Web;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    [ToolboxItem(false)]
    public class StyleWebControl : DXHtmlGenericControl, IScriptContainer
    {
        private readonly Dictionary<string, string> styles;
        private readonly Dictionary<string, string> tagStyles;

        public StyleWebControl() : base(DXHtmlTextWriterTag.Style)
        {
            this.styles = new Dictionary<string, string>(StringExtensions.ComparerInvariantCultureIgnoreCase);
            this.tagStyles = new Dictionary<string, string>(StringExtensions.ComparerInvariantCultureIgnoreCase);
            base.Attributes.Add("type", "text/css");
        }

        private void AddStyle(string style, string name)
        {
            if (!this.styles.ContainsKey(style))
            {
                this.styles.Add(style, name);
            }
        }

        private void AddTagStyle(string style, string name)
        {
            if (!this.tagStyles.ContainsKey(style))
            {
                this.tagStyles.Add(style, name);
            }
        }

        protected internal string GetClassName(string style) => 
            $"cs{style.GetHashCode():X}";

        public bool IsClientScriptBlockRegistered(string key) => 
            true;

        public void RegisterClientScriptBlock(string key, string script)
        {
            throw new NotSupportedException();
        }

        public void RegisterCommonCssStyle(string style, string tagName)
        {
            this.AddTagStyle(style, tagName);
        }

        public string RegisterCssClass(string style)
        {
            this.AddStyle(style, this.GetClassName(style));
            return this.styles[style];
        }

        protected override void Render(DXHtmlTextWriter writer)
        {
            this.RenderBeginTag(writer);
            this.RenderTagStyles(writer);
            this.RenderStyles(writer);
            this.RenderEndTag(writer);
        }

        protected internal virtual void RenderStyles(TextWriter writer)
        {
            foreach (string str in this.styles.Keys)
            {
                string str2 = this.styles[str];
                writer.WriteLine($".{str2}{{{str}}}");
            }
        }

        protected internal virtual void RenderTagStyles(TextWriter writer)
        {
            foreach (string str in this.tagStyles.Keys)
            {
                string str2 = this.tagStyles[str];
                writer.WriteLine($"{str2} {{{str}}}");
            }
        }

        public Dictionary<string, string> Styles =>
            this.styles;

        public Dictionary<string, string> TagStyles =>
            this.tagStyles;
    }
}

