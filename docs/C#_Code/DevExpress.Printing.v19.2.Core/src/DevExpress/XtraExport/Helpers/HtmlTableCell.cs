namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class HtmlTableCell : HtmlTag
    {
        private const int MaxStandardAsciiCode = 0x7f;
        private Color backColor;
        private int cellIndent;
        private readonly float DefaultPadding;
        private readonly string DefaultPaddingUnit;
        private string fontName;
        private Color foreColor;
        private XlRichTextString formattedValueCore;
        private readonly float IndentToPixel;
        private string rawValueCore;
        private List<HtmlSpan> textSpans;
        private string valueCore;
        private HtmlAnchor hyperlink;

        internal HtmlTableCell(HtmlTableRow row) : base(row.document)
        {
            this.backColor = Color.White;
            this.DefaultPadding = 7.2f;
            this.DefaultPaddingUnit = "px";
            this.fontName = "Calibri";
            this.foreColor = Color.Black;
            this.IndentToPixel = 14.66667f;
            this.rawValueCore = string.Empty;
            this.textSpans = new List<HtmlSpan>();
            this.valueCore = string.Empty;
            this.<ColSpan>k__BackingField = 1;
            this.<FontSize>k__BackingField = 11.0;
            this.<RowSpan>k__BackingField = 1;
            this.<WidthInPercent>k__BackingField = -1.0;
            this.<TargetUrl>k__BackingField = string.Empty;
            this.<Row>k__BackingField = row;
            this.<LeftBorder>k__BackingField = new HtmlTableCellBorder();
            this.<TopBorder>k__BackingField = new HtmlTableCellBorder();
            this.<RightBorder>k__BackingField = new HtmlTableCellBorder();
            this.<BottomBorder>k__BackingField = new HtmlTableCellBorder();
        }

        protected override string Compile(int level = 0)
        {
            base.WriteOpenTag(false, level);
            if (this.IsHyperlink)
            {
                base.WriteChild(this.hyperlink, level);
            }
            else
            {
                foreach (HtmlSpan span in this.textSpans)
                {
                    base.WriteChild(span, level);
                }
            }
            base.WriteCloseTag(level);
            return base.Compile(level);
        }

        private void CreateFormattedValue()
        {
            this.textSpans.Clear();
            foreach (XlRichTextRun run in this.formattedValueCore.Runs)
            {
                HtmlSpan item = new HtmlSpan(this.Row.document);
                if (run.Font != null)
                {
                    item.FontName = run.Font.Name;
                    item.FontSize = run.Font.Size;
                    item.FontStyle = FontHelper.GetFontStyle(run.Font);
                    item.ForeColor = (Color) run.Font.Color;
                }
                item.Value = this.GetHtmlUnicode(run.Text);
                this.textSpans.Add(item);
            }
        }

        private string GetHtmlUnicode(string value)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char ch in WebUtility.HtmlEncode(value).ToCharArray())
            {
                int num2 = Convert.ToInt32(ch);
                if (num2 > 0x7f)
                {
                    builder.AppendFormat("&#{0};", num2);
                }
                else
                {
                    builder.Append(ch);
                }
            }
            return Encoding.Default.GetString(Encoding.UTF8.GetBytes(builder.ToString()));
        }

        protected override void PreCompile()
        {
            this.WriteAttributes();
            base.document.Head.Style.AddStyle(base.styleSheet);
            if (this.IsHyperlink)
            {
                this.hyperlink = new HtmlAnchor(base.document);
                this.hyperlink.Caption = this.valueCore;
                this.hyperlink.TargetUrl = this.TargetUrl;
                this.hyperlink.FontName = this.FontName;
                this.hyperlink.FontSize = this.FontSize;
                this.hyperlink.FontStyle = this.FontStyle;
                base.PreCompile(this.hyperlink);
            }
            else
            {
                this.WriteText();
                foreach (HtmlSpan span in this.textSpans)
                {
                    base.PreCompile(span);
                }
            }
        }

        private void WriteAlignment()
        {
            base.WriteStyle("vertical-align", this.VAlignment.ToHtmlValue());
            base.WriteStyle("text-align", this.HAlignment.ToHtmlValue());
        }

        private void WriteAttributes()
        {
            if (this.ColSpan > 1)
            {
                base.WriteAttribute("colspan", this.ColSpan.ToString());
            }
            if (this.RowSpan > 1)
            {
                base.WriteAttribute("rowspan", this.RowSpan.ToString());
            }
            this.WriteBorders();
            this.WritePadding();
            this.WriteAlignment();
            base.WriteStyle("background-color", this.BackColor.ToHex());
            if (this.WidthInPercent > 0.0)
            {
                base.WriteStyle("width", $"{this.WidthInPercent}%");
            }
        }

        private void WriteBorderColor()
        {
            this.WriteBorderPropOptimized("border-color", this.TopBorder.Color.ToHex(), this.RightBorder.Color.ToHex(), this.BottomBorder.Color.ToHex(), this.LeftBorder.Color.ToHex());
        }

        private void WriteBorderPropOptimized(string prop, string topBorder, string rightBorder, string bottomBorder, string leftBorder)
        {
            if (!leftBorder.Equals(rightBorder))
            {
                string[] textArray3 = new string[] { topBorder, rightBorder, bottomBorder, leftBorder };
                base.WriteStyle(prop, string.Join(" ", textArray3));
            }
            else if (!topBorder.Equals(bottomBorder))
            {
                string[] textArray2 = new string[] { topBorder, leftBorder, bottomBorder };
                base.WriteStyle(prop, string.Join(" ", textArray2));
            }
            else if (leftBorder.Equals(topBorder))
            {
                base.WriteStyle(prop, leftBorder);
            }
            else
            {
                string[] textArray1 = new string[] { topBorder, leftBorder };
                base.WriteStyle(prop, string.Join(" ", textArray1));
            }
        }

        private void WriteBorders()
        {
            this.WriteBorderStyle();
            this.WriteBorderWidth();
            this.WriteBorderColor();
        }

        private void WriteBorderStyle()
        {
            string borderStyle = StyleHelper.GetBorderStyle(this.TopBorder.Style);
            this.WriteBorderPropOptimized("border-style", borderStyle, StyleHelper.GetBorderStyle(this.RightBorder.Style), StyleHelper.GetBorderStyle(this.BottomBorder.Style), StyleHelper.GetBorderStyle(this.LeftBorder.Style));
        }

        private void WriteBorderWidth()
        {
            string borderWidth = StyleHelper.GetBorderWidth(this.TopBorder.Style, this.TopBorder.Width);
            this.WriteBorderPropOptimized("border-width", borderWidth, StyleHelper.GetBorderWidth(this.RightBorder.Style, this.RightBorder.Width), StyleHelper.GetBorderWidth(this.BottomBorder.Style, this.BottomBorder.Width), StyleHelper.GetBorderWidth(this.LeftBorder.Style, this.LeftBorder.Width));
        }

        private void WritePadding()
        {
            string leftBorder = (this.CellIndent <= 0) ? $"{this.DefaultPadding}{this.DefaultPaddingUnit}" : $"{(this.DefaultPadding + (this.IndentToPixel * this.CellIndent))}{this.DefaultPaddingUnit}";
            string rightBorder = $"{this.DefaultPadding}{this.DefaultPaddingUnit}";
            this.WriteBorderPropOptimized("padding", "0px", rightBorder, "0px", leftBorder);
        }

        private void WriteText()
        {
            if ((this.FormattedValue == null) && (this.textSpans.Count > 0))
            {
                this.textSpans[0].FontName = this.FontName;
                this.textSpans[0].FontSize = this.FontSize;
                this.textSpans[0].FontStyle = this.FontStyle;
                this.textSpans[0].ForeColor = this.ForeColor;
            }
        }

        protected override string Tag =>
            "td";

        public Color BackColor
        {
            get => 
                this.backColor;
            set
            {
                if (this.backColor != value)
                {
                    this.backColor = value;
                }
            }
        }

        public HtmlTableCellBorder BottomBorder { get; }

        public int CellIndent
        {
            get => 
                this.cellIndent;
            set
            {
                if (this.cellIndent != value)
                {
                    this.cellIndent = value;
                }
            }
        }

        public int ColSpan { get; set; }

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

        public XlRichTextString FormattedValue
        {
            get => 
                this.formattedValueCore;
            set
            {
                if (!ReferenceEquals(this.formattedValueCore, value))
                {
                    this.formattedValueCore = value;
                    this.CreateFormattedValue();
                }
            }
        }

        public HtmlCellHAlignment HAlignment { get; set; }

        public HtmlTableCellBorder LeftBorder { get; }

        public HtmlTableCellBorder RightBorder { get; }

        public HtmlTableRow Row { get; }

        public int RowSpan { get; set; }

        public HtmlTableCellBorder TopBorder { get; }

        public HtmlCellVAlignment VAlignment { get; set; }

        public string Value
        {
            get => 
                this.valueCore;
            set
            {
                if (this.rawValueCore != value)
                {
                    this.rawValueCore = value;
                    this.textSpans.Clear();
                    this.valueCore = this.GetHtmlUnicode(this.rawValueCore);
                    HtmlSpan item = new HtmlSpan(this.Row.document);
                    item.Value = this.valueCore;
                    this.textSpans.Add(item);
                }
            }
        }

        public double WidthInPercent { get; set; }

        public string TargetUrl { get; set; }

        public bool IsHyperlink =>
            !string.IsNullOrEmpty(this.TargetUrl);
    }
}

