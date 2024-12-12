namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class HtmlAnchor : HtmlTag
    {
        private string fontName;

        internal HtmlAnchor(HtmlDocument document) : base(document)
        {
            this.fontName = "Calibri";
            this.<FontSize>k__BackingField = 11.0;
            this.<TargetUrl>k__BackingField = string.Empty;
            this.<Caption>k__BackingField = string.Empty;
        }

        protected override string Compile(int level = 0)
        {
            base.WriteAttribute("href", this.TargetUrl);
            base.WriteOpenTag(false, level);
            base.WriteValue(this.Caption, 0);
            base.WriteCloseTag(level);
            return base.Compile(level);
        }

        protected override void PreCompile()
        {
            this.WriteFont();
            base.document.Head.Style.AddStyle(base.styleSheet);
        }

        private void WriteFont()
        {
            base.WriteStyle("font-family", this.FontName);
            base.WriteStyle("font-size", $"{this.FontSize}pt");
            this.WriteFontStyle();
        }

        private void WriteFontStyle()
        {
            if (this.FontStyle.HasFlag(System.Drawing.FontStyle.Italic))
            {
                base.WriteStyle("font-style", "italic");
            }
            if (this.FontStyle.HasFlag(System.Drawing.FontStyle.Bold))
            {
                base.WriteStyle("font-weight", "bold");
            }
            if (this.FontStyle.HasFlag(System.Drawing.FontStyle.Underline))
            {
                if (this.FontStyle.HasFlag(System.Drawing.FontStyle.Strikeout))
                {
                    base.WriteStyle("text-decoration", "line-through underline");
                }
                else
                {
                    base.WriteStyle("text-decoration", "underline");
                }
            }
        }

        protected override string Tag =>
            "a";

        public string FontName
        {
            get => 
                this.fontName;
            set
            {
                if (this.fontName != value)
                {
                    this.fontName = value;
                }
            }
        }

        public double FontSize { get; set; }

        public System.Drawing.FontStyle FontStyle { get; set; }

        public string TargetUrl { get; set; }

        public string Caption { get; set; }
    }
}

