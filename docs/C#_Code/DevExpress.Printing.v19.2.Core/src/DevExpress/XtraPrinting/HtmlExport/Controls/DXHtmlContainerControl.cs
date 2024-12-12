namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using DevExpress.XtraPrinting.HtmlExport;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.ComponentModel;
    using System.Net;

    public abstract class DXHtmlContainerControl : DXHtmlControl
    {
        protected DXHtmlContainerControl() : this(DXHtmlTextWriterTag.Span)
        {
        }

        public DXHtmlContainerControl(DXHtmlTextWriterTag tag) : base(tag)
        {
        }

        protected override void AddAttributesToRender(DXHtmlTextWriter writer)
        {
            this.ViewState.Remove("innerhtml");
            base.AddAttributesToRender(writer);
        }

        protected override DXWebControlCollection CreateControlCollection() => 
            new DXWebControlCollection(this);

        protected internal override void Render(DXHtmlTextWriter writer)
        {
            this.RenderBeginTag(writer);
            this.RenderChildren(writer);
            this.RenderEndTag(writer);
        }

        protected internal virtual void RenderContents(DXHtmlTextWriter writer)
        {
            this.RenderChildren(writer);
        }

        protected virtual void RenderEndTag(DXHtmlTextWriter writer)
        {
            writer.RenderEndTag();
        }

        [Browsable(false)]
        public virtual string InnerHtml
        {
            get
            {
                if (base.IsLiteralContent())
                {
                    return ((DXHtmlLiteralControl) this.Controls[0]).Text;
                }
                if (this.Controls.Count != 0)
                {
                    throw new Exception("Inner_Content_not_literal");
                }
                return string.Empty;
            }
            set
            {
                this.Controls.Clear();
                this.Controls.Add(new DXHtmlLiteralControl(value));
                this.ViewState["innerhtml"] = value;
            }
        }

        [Browsable(false)]
        public virtual string InnerText
        {
            get => 
                WebUtility.HtmlDecode(this.InnerHtml);
            set => 
                this.InnerHtml = WebUtility.HtmlEncode(value);
        }

        public bool HasChildren =>
            this.Controls.Count == 0;
    }
}

