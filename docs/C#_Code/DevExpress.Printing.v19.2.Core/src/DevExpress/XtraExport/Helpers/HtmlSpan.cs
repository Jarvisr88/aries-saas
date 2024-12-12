namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class HtmlSpan : HtmlTag
    {
        private string fontName;
        private Color foreColor;
        private string valueCore;

        internal HtmlSpan(HtmlDocument document) : base(document)
        {
            this.fontName = "Calibri";
            this.foreColor = Color.Black;
            this.valueCore = string.Empty;
            this.<FontSize>k__BackingField = 11.0;
        }

        protected override string Compile(int level = 0)
        {
            base.WriteOpenTag(false, level);
            base.WriteValue(this.Value, level);
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
            base.WriteStyle("color", this.ForeColor.ToHex());
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
            "span";

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

        public Color ForeColor
        {
            get => 
                this.foreColor;
            set
            {
                if (this.foreColor != value)
                {
                    this.foreColor = value;
                }
            }
        }

        public string Value
        {
            get => 
                this.valueCore;
            set
            {
                if (this.valueCore != value)
                {
                    this.valueCore = value;
                }
            }
        }
    }
}

