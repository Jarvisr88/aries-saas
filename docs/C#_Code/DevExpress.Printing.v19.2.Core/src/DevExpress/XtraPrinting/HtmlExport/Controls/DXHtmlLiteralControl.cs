namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;

    public class DXHtmlLiteralControl : DXWebControlBase
    {
        internal string text;

        public DXHtmlLiteralControl()
        {
            base.PreventAutoID();
        }

        public DXHtmlLiteralControl(string text) : this()
        {
            string text1 = text;
            if (text == null)
            {
                string local1 = text;
                text1 = string.Empty;
            }
            this.text = text1;
        }

        protected override DXWebControlCollection CreateControlCollection() => 
            new DXWebEmptyControlCollection(this);

        internal override void InitRecursive(DXWebControlBase namingContainer)
        {
            this.OnInit(EventArgs.Empty);
        }

        internal override void LoadRecursive()
        {
            this.OnLoad(EventArgs.Empty);
        }

        internal override void PreRenderRecursiveInternal()
        {
            this.OnPreRender(EventArgs.Empty);
        }

        protected internal override void Render(DXHtmlTextWriter output)
        {
            output.Write(this.text);
        }

        internal override void UnloadRecursive(bool dispose)
        {
            this.OnUnload(EventArgs.Empty);
            if (dispose)
            {
                this.Dispose();
            }
        }

        public virtual string Text
        {
            get => 
                this.text;
            set
            {
                string text1 = value;
                if (value == null)
                {
                    string local1 = value;
                    text1 = string.Empty;
                }
                this.text = text1;
            }
        }
    }
}

