namespace DevExpress.XtraPrinting.HtmlExport.Controls
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.HtmlExport;
    using DevExpress.XtraPrinting.HtmlExport.Native;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public abstract class DXHtmlControl : DXWebControlBase
    {
        private DXWebAttributeCollection attributes;
        private DXSimpleBitVector32 webControlFlags;
        private DXWebStyle controlStyle;
        internal DXHtmlTextWriterTag tagKey;

        protected DXHtmlControl() : this(DXHtmlTextWriterTag.Span)
        {
        }

        protected DXHtmlControl(DXHtmlTextWriterTag tagKey)
        {
            this.tagKey = tagKey;
        }

        protected virtual void AddAttributesToRender(DXHtmlTextWriter writer)
        {
            if (this.ID != null)
            {
                writer.AddAttribute(DXHtmlTextWriterAttribute.Id, this.ClientID);
            }
            if (!this.Enabled)
            {
                writer.AddAttribute(DXHtmlTextWriterAttribute.Disabled, "disabled");
            }
            if (this.webControlFlags[8])
            {
                string toolTip = this.ToolTip;
                if (toolTip.Length > 0)
                {
                    writer.AddAttribute(DXHtmlTextWriterAttribute.Title, toolTip);
                }
            }
            if ((this.TagKey == DXHtmlTextWriterTag.Span) || (this.TagKey == DXHtmlTextWriterTag.A))
            {
                this.AddDisplayInlineBlockIfNeeded(writer);
            }
            if (this.ControlStyleCreated && !this.ControlStyle.IsEmpty)
            {
                this.ControlStyle.AddAttributesToRender(writer, this);
            }
            foreach (string str2 in this.Attributes.Keys)
            {
                if (StringExtensions.CompareInvariantCultureIgnoreCase(str2, "style") != 0)
                {
                    writer.AddAttribute(str2, this.Attributes[str2]);
                }
            }
            foreach (string str3 in this.Style.Keys)
            {
                writer.AddStyleAttribute(str3, this.Style[str3]);
            }
        }

        internal void AddDisplayInlineBlockIfNeeded(DXHtmlTextWriter writer)
        {
            if (!this.RequiresLegacyRendering && ((this.BorderStyle != DXWebBorderStyle.NotSet) || (!this.BorderWidth.IsEmpty || (!this.Height.IsEmpty || !this.Width.IsEmpty))))
            {
                writer.AddStyleAttribute(DXHtmlTextWriterStyle.Display, "inline-block");
            }
        }

        protected override DXWebControlCollection CreateControlCollection() => 
            new DXWebEmptyControlCollection(this);

        protected virtual DXWebStyle CreateControlStyle() => 
            new DXWebStyle(this.ViewState);

        protected virtual string GetAttribute(string name) => 
            this.Attributes[name];

        internal static string MapIntegerAttributeToString(int n) => 
            (n != -1) ? n.ToString(NumberFormatInfo.InvariantInfo) : null;

        internal static string MapStringAttributeToString(string s) => 
            ((s == null) || (s.Length != 0)) ? s : null;

        protected internal override void Render(DXHtmlTextWriter writer)
        {
            this.RenderBeginTag(writer);
        }

        protected virtual void RenderBeginTag(DXHtmlTextWriter writer)
        {
            this.AddAttributesToRender(writer);
            writer.RenderBeginTag(this.TagKey);
        }

        protected virtual void SetAttribute(string name, string value)
        {
            this.Attributes[name] = value;
        }

        public override string ToString() => 
            $"{base.GetType().Name} <{this.TagKey.ToString().ToLowerInvariant()}>";

        public DXWebAttributeCollection Attributes
        {
            get
            {
                this.attributes ??= new DXWebAttributeCollection(this.ViewState);
                return this.attributes;
            }
        }

        public virtual DXWebUnit BorderWidth
        {
            get => 
                this.ControlStyleCreated ? this.ControlStyle.BorderWidth : DXWebUnit.Empty;
            set => 
                this.ControlStyle.BorderWidth = value;
        }

        internal virtual bool RequiresLegacyRendering =>
            false;

        public bool Disabled
        {
            get
            {
                string str = this.Attributes["disabled"];
                return ((str != null) ? str.Equals("disabled") : false);
            }
            set
            {
                if (value)
                {
                    this.Attributes["disabled"] = "disabled";
                }
                else
                {
                    this.Attributes["disabled"] = null;
                }
            }
        }

        [Browsable(false)]
        public DXCssStyleCollection Style =>
            this.Attributes.CssStyle;

        public virtual DXWebUnit Width
        {
            get => 
                this.ControlStyleCreated ? this.ControlStyle.Width : DXWebUnit.Empty;
            set => 
                this.ControlStyle.Width = value;
        }

        public virtual DXWebUnit Height
        {
            get => 
                this.ControlStyleCreated ? this.ControlStyle.Height : DXWebUnit.Empty;
            set => 
                this.ControlStyle.Height = value;
        }

        public virtual DXWebFontInfo Font =>
            this.ControlStyle.Font;

        public virtual string CssClass
        {
            get => 
                this.ControlStyleCreated ? this.ControlStyle.CssClass : string.Empty;
            set => 
                this.ControlStyle.CssClass = value;
        }

        public bool ControlStyleCreated =>
            this.controlStyle != null;

        public DXWebStyle ControlStyle
        {
            get
            {
                if (this.controlStyle == null)
                {
                    this.controlStyle = this.CreateControlStyle();
                    if (base.IsTrackingViewState)
                    {
                        this.controlStyle.TrackViewState();
                    }
                }
                return this.controlStyle;
            }
        }

        public virtual DXWebBorderStyle BorderStyle
        {
            get => 
                this.ControlStyleCreated ? this.ControlStyle.BorderStyle : DXWebBorderStyle.NotSet;
            set => 
                this.ControlStyle.BorderStyle = value;
        }

        public virtual bool Enabled
        {
            get => 
                !base.flags[0x80000];
            set
            {
                if (!base.flags[0x80000] != value)
                {
                    if (!value)
                    {
                        base.flags.Set(0x80000);
                    }
                    else
                    {
                        base.flags.Clear(0x80000);
                    }
                    if (base.IsTrackingViewState)
                    {
                        this.webControlFlags.Set(2);
                    }
                }
            }
        }

        public virtual string ToolTip
        {
            get
            {
                if (this.webControlFlags[8])
                {
                    string str = this.ViewState["ToolTip"] as string;
                    if (str != null)
                    {
                        return str;
                    }
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["ToolTip"] = value;
                this.webControlFlags.Set(8);
            }
        }

        public string TagName =>
            this.tagKey.ToString().ToLowerInvariant();

        public DXHtmlTextWriterTag TagKey =>
            this.tagKey;
    }
}

