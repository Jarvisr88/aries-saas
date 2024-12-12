namespace DevExpress.XtraPrinting.Export.Web
{
    using DevExpress.XtraPrinting.HtmlExport;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    [ToolboxItem(false)]
    public class ClipControl : DXWebControlBase
    {
        private Point offset;
        private Size clipSize;
        private Size originalSize;
        private DXHtmlControl parent;
        public string InnerControlCSSClass;
        private DXWebControlBase innerContainer;

        public ClipControl(DXHtmlControl parent, Point offset, Size clipSize)
        {
            this.offset = offset;
            this.clipSize = clipSize;
            this.parent = parent;
            this.originalSize = Size.Empty;
            this.innerContainer = new DXWebControlBase();
        }

        public void AddOffset(Point additionalOffset)
        {
            this.offset.Offset(additionalOffset);
        }

        private void ApplyParentVertAlignment(DXHtmlControl control)
        {
            control.Style.Add(DXHtmlTextWriterStyle.VerticalAlign, this.parent.Style["vertical-align"]);
        }

        private void ApplyStyle(DXHtmlControl control)
        {
            if (this.offset.Y != 0)
            {
                control.Style.Add("margin-top", HtmlConvert.ToHtml(this.offset.Y));
            }
            if (this.offset.X != 0)
            {
                control.Style.Add("margin-left", HtmlConvert.ToHtml(this.offset.X));
            }
        }

        private void CopyParentStyle(DXHtmlControl control)
        {
            if (!string.IsNullOrEmpty(this.InnerControlCSSClass))
            {
                control.Attributes["class"] = this.InnerControlCSSClass;
                control.Style.Add("text-align", this.parent.Style["text-align"]);
                control.Style.Add("vertical-align", this.parent.Style["vertical-align"]);
                control.Style.Add("line-height", this.parent.Style["line-height"]);
            }
        }

        protected internal override void CreateChildControls()
        {
            base.CreateChildControls();
            DXHtmlGenericControl child = new DXHtmlGenericControl(DXHtmlTextWriterTag.Div);
            child.Style.Add("overflow", "hidden");
            HtmlHelper.SetStyleSize(child.Style, this.clipSize);
            this.Controls.Add(child);
            DXHtmlControl control = child;
            if ((this.offset != Point.Empty) || (this.originalSize != Size.Empty))
            {
                if ((this.innerContainer.Controls.Count == 1) && (this.innerContainer.Controls[0] is DXHtmlImage))
                {
                    this.ApplyStyle(this.innerContainer.Controls[0] as DXHtmlImage);
                }
                else
                {
                    control = new DXHtmlGenericControl(DXHtmlTextWriterTag.Div);
                    this.ApplyStyle(control);
                    this.CopyParentStyle(control);
                    if (this.originalSize != Size.Empty)
                    {
                        HtmlHelper.SetStyleSize(control.Style, this.originalSize);
                    }
                    control.Style.Add("overflow", "hidden");
                    DXHtmlGenericControl control3 = new DXHtmlGenericControl(DXHtmlTextWriterTag.Div);
                    this.ApplyParentVertAlignment(control3);
                    control.Style.Add(DXHtmlTextWriterStyle.Display, "table");
                    control3.Style.Add(DXHtmlTextWriterStyle.Display, "table-cell");
                    child.Controls.Add(control);
                    control.Controls.Add(control3);
                    control = control3;
                }
            }
            control.Controls.Add(this.innerContainer);
        }

        public override void RenderControl(DXHtmlTextWriter writer)
        {
            this.CreateChildControls();
            base.Render(writer);
        }

        public void SetClipSize(Size size)
        {
            this.clipSize = size;
        }

        public void SetOriginalSize(Size size)
        {
            this.originalSize = size;
        }

        public DXWebControlBase InnerContainer =>
            this.innerContainer;
    }
}

