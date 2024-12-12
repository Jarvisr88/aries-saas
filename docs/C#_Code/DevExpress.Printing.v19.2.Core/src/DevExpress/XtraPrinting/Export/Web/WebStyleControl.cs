namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.Collections;

    public class WebStyleControl : DXHtmlContainerControl
    {
        private SortedList styles;
        private SortedList tagStyles;

        public WebStyleControl() : base(DXHtmlTextWriterTag.Style)
        {
            base.Attributes.Add("type", "text/css");
            this.styles = new SortedList(StringExtensions.ComparerInvariantCultureIgnoreCase);
            this.tagStyles = new SortedList();
        }

        public void AddStyle(string style, string name)
        {
            if (!this.styles.ContainsKey(style))
            {
                this.styles.Add(style, name);
            }
        }

        public void AddTagStyle(string style, string tagName)
        {
            if (!this.tagStyles.ContainsKey(style))
            {
                this.tagStyles.Add(style, tagName);
            }
        }

        public void ClearContent()
        {
            this.styles.Clear();
            this.tagStyles.Clear();
        }

        private string GetClassName(string style) => 
            $"cs{style.GetHashCode():X}";

        private string GetName(SortedList list, int index) => 
            (string) list.GetByIndex(index);

        private string GetValue(SortedList list, int index) => 
            (string) list.GetKey(index);

        public string RegisterStyle(string style)
        {
            this.AddStyle(style, this.GetClassName(style));
            return (string) this.styles[style];
        }

        protected internal override void Render(DXHtmlTextWriter writer)
        {
            if ((this.styles.Count > 0) || (this.tagStyles.Count > 0))
            {
                this.RenderBeginTag(writer);
                this.RenderContent(writer);
                this.RenderEndTag(writer);
            }
        }

        private void RenderContent(DXHtmlTextWriter writer)
        {
            for (int i = 0; i < this.styles.Count; i++)
            {
                string name = this.GetName(this.styles, i);
                string format = (name.IndexOf(".") < 0) ? ".{0} {{{1}}}" : "{0} {{{1}}}";
                writer.WriteLine(format, name, this.GetValue(this.styles, i));
            }
            for (int j = 0; j < this.tagStyles.Count; j++)
            {
                writer.WriteLine("{0} {{{1}}}", this.GetName(this.tagStyles, j), this.GetValue(this.tagStyles, j));
            }
        }

        public SortedList Styles =>
            this.styles;
    }
}

