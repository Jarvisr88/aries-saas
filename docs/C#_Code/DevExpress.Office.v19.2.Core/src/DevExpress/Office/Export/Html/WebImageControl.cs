namespace DevExpress.Office.Export.Html
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.HtmlExport;
    using DevExpress.XtraPrinting.HtmlExport.Controls;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.ComponentModel;

    [ToolboxItem(false)]
    public class WebImageControl : DXHtmlGenericControl
    {
        private bool urlResolved;

        public WebImageControl() : base(DXHtmlTextWriterTag.Img)
        {
        }

        protected override void AddAttributesToRender(DXHtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            string imageUrl = this.ImageUrl;
            if (!this.UrlResolved)
            {
                imageUrl = base.ResolveClientUrl(imageUrl);
            }
            if (imageUrl.Length > 0)
            {
                if (imageUrl.StartsWithInvariantCultureIgnoreCase("file://"))
                {
                    writer.AddAttribute("src", imageUrl, false);
                }
                else
                {
                    writer.AddAttribute(DXHtmlTextWriterAttribute.Src, imageUrl);
                }
            }
            imageUrl = this.DescriptionUrl;
            if (imageUrl.Length != 0)
            {
                writer.AddAttribute(DXHtmlTextWriterAttribute.Longdesc, base.ResolveClientUrl(imageUrl));
            }
            imageUrl = this.AlternateText;
            if ((imageUrl.Length > 0) || this.GenerateEmptyAlternateText)
            {
                writer.AddAttribute(DXHtmlTextWriterAttribute.Alt, imageUrl);
            }
            string alignValueString = this.GetAlignValueString();
            if (!string.IsNullOrEmpty(alignValueString))
            {
                writer.AddAttribute(DXHtmlTextWriterAttribute.Align, alignValueString);
            }
            if (this.BorderWidth.IsEmpty)
            {
                writer.AddStyleAttribute(DXHtmlTextWriterStyle.BorderWidth, "0px");
            }
        }

        protected internal string GetAlignValueString()
        {
            switch (this.ImageAlign)
            {
                case DXWebImageAlign.NotSet:
                    return string.Empty;

                case DXWebImageAlign.Left:
                    return "left";

                case DXWebImageAlign.Right:
                    return "right";

                case DXWebImageAlign.Baseline:
                    return "baseline";

                case DXWebImageAlign.Top:
                    return "top";

                case DXWebImageAlign.Middle:
                    return "middle";

                case DXWebImageAlign.Bottom:
                    return "bottom";

                case DXWebImageAlign.AbsBottom:
                    return "absbottom";

                case DXWebImageAlign.AbsMiddle:
                    return "absmiddle";
            }
            return "texttop";
        }

        [Localizable(true), Bindable(true), DefaultValue("")]
        public virtual string AlternateText
        {
            get
            {
                string str = (string) this.ViewState["alt"];
                return ((str == null) ? string.Empty : str);
            }
            set => 
                this.ViewState["alt"] = value;
        }

        [DefaultValue("")]
        public virtual string DescriptionUrl
        {
            get
            {
                string str = (string) this.ViewState["longdesc"];
                return ((str == null) ? string.Empty : str);
            }
            set => 
                this.ViewState["longdesc"] = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Enabled
        {
            get => 
                base.Enabled;
            set => 
                base.Enabled = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DXWebFontInfo Font =>
            base.Font;

        [DefaultValue(false)]
        public virtual bool GenerateEmptyAlternateText
        {
            get => 
                this.ViewState["alt"] is string;
            set
            {
                if (!value)
                {
                    this.ViewState.Remove("alt");
                }
                else if (!(this.ViewState["alt"] is string))
                {
                    this.ViewState["alt"] = string.Empty;
                }
            }
        }

        [DefaultValue(0)]
        public virtual DXWebImageAlign ImageAlign
        {
            get
            {
                object obj2 = this.ViewState["align"];
                return ((obj2 == null) ? DXWebImageAlign.NotSet : ((DXWebImageAlign) obj2));
            }
            set
            {
                if ((value < DXWebImageAlign.NotSet) || (value > DXWebImageAlign.TextTop))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.ViewState["align"] = value;
            }
        }

        [DefaultValue(""), Bindable(true)]
        public virtual string ImageUrl
        {
            get
            {
                string str = (string) this.ViewState["src"];
                return ((str == null) ? string.Empty : str);
            }
            set => 
                this.ViewState["src"] = value;
        }

        internal bool UrlResolved
        {
            get => 
                this.urlResolved;
            set => 
                this.urlResolved = value;
        }
    }
}

