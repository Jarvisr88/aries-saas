namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Text;

    internal class RtfTableRowCell : RtfControl
    {
        private readonly int DefaultPadding = 0x6c;
        private readonly int DefaultPaddingUnit = 3;
        private readonly int MaxStandardAsciiCode = 0x7f;
        private Color backColor = Color.White;
        private int cellIndent;
        private string fontName = RtfDocument.DefaultFont;
        private Color foreColor = Color.Black;
        private XlRichTextString formattedValueCore;
        private string valueCore = string.Empty;
        internal bool HMergeFirstCell;
        internal bool HMergeLastCell;
        internal bool VMergeFirstCell;
        internal bool VMergeLastCell;

        public RtfTableRowCell(RtfDocument document, RtfTableRow row)
        {
            this.<Document>k__BackingField = document;
            this.<Row>k__BackingField = row;
            this.<LeftBorder>k__BackingField = new RtfTableRowCellBorder(this.Document);
            this.<RightBorder>k__BackingField = new RtfTableRowCellBorder(this.Document);
            this.<TopBorder>k__BackingField = new RtfTableRowCellBorder(this.Document);
            this.<BottomBorder>k__BackingField = new RtfTableRowCellBorder(this.Document);
        }

        public void AssignAppearance(RtfTableRowCell other)
        {
            this.BackColor = other.BackColor;
            this.HAlignment = other.HAlignment;
            this.VAlignment = other.VAlignment;
        }

        private void CompileBorder(RtfTableRowCellBorder border, BorderSide side)
        {
            if (border.Style != RtfCellBorderStyle.None)
            {
                int? nullable;
                switch (side)
                {
                    case BorderSide.Left:
                        nullable = null;
                        base.WriteKeyword(Keyword.CellBorderLeft, nullable, false, false);
                        break;

                    case BorderSide.Top:
                        nullable = null;
                        base.WriteKeyword(Keyword.CellBorderTop, nullable, false, false);
                        break;

                    case BorderSide.Right:
                        nullable = null;
                        base.WriteKeyword(Keyword.CellBorderRight, nullable, false, false);
                        break;

                    case BorderSide.Bottom:
                        nullable = null;
                        base.WriteKeyword(Keyword.CellBorderBottom, nullable, false, false);
                        break;

                    default:
                        break;
                }
                base.WriteKeyword(Keyword.CellLeftPadding, new int?(this.DefaultPadding), false, false);
                base.WriteKeyword(Keyword.CellLeftPaddingUnits, new int?(this.DefaultPaddingUnit), false, false);
                base.WriteKeyword(Keyword.CellRightPadding, new int?(this.DefaultPadding), false, false);
                base.WriteKeyword(Keyword.CellRightPaddingUnits, new int?(this.DefaultPaddingUnit), false, false);
                nullable = null;
                base.WriteKeyword(border.Style, nullable, false, false);
                if (border.Width > 0)
                {
                    base.WriteKeyword(Keyword.CellBorderWidth, new int?(border.Width), false, false);
                }
                base.WriteKeyword(Keyword.CellBorderColor, new int?(this.Document.ColorTable.GetColorIndex(border.Color)), false, false);
            }
        }

        public string CompileCell()
        {
            int? nullable;
            base.builder.Clear();
            if (this.VMergeFirstCell)
            {
                nullable = null;
                base.WriteKeyword(Keyword.CellVMergeFirstCell, nullable, false, false);
            }
            if (this.VMergeLastCell)
            {
                nullable = null;
                base.WriteKeyword(Keyword.CellVMergeLastCell, nullable, false, false);
            }
            if (this.HMergeFirstCell)
            {
                nullable = null;
                base.WriteKeyword(Keyword.CellMergeFirstCell, nullable, false, false);
            }
            if (this.HMergeLastCell)
            {
                nullable = null;
                base.WriteKeyword(Keyword.CellMergeLastCell, nullable, false, false);
            }
            if (this.VAlignment != RtfCellVAlignment.Top)
            {
                nullable = null;
                base.WriteKeyword(this.VAlignment, nullable, false, false);
            }
            this.CompileBorder(this.LeftBorder, BorderSide.Left);
            this.CompileBorder(this.TopBorder, BorderSide.Top);
            this.CompileBorder(this.RightBorder, BorderSide.Right);
            this.CompileBorder(this.BottomBorder, BorderSide.Bottom);
            base.WriteKeyword(Keyword.CellPatternBackColor, new int?(this.Document.ColorTable.GetColorIndex(Color.White)), false, false);
            base.WriteKeyword(Keyword.CellPatternForeColor, new int?(this.Document.ColorTable.GetColorIndex(this.BackColor)), false, false);
            base.WriteKeyword(Keyword.CellShading, 0x2710, false, false);
            base.WriteKeyword(Keyword.CellRightBound, new int?(this.CellRightBound), false, false);
            return base.builder.ToString();
        }

        public string CompileCellData()
        {
            base.builder.Clear();
            if (string.IsNullOrEmpty(this.Value))
            {
                this.WriteEmptyCell();
            }
            else
            {
                if (this.CellIndent > 0)
                {
                    base.WriteKeyword(Keyword.CellLeftIndent, new int?(this.CellIndent), false, false);
                }
                base.WriteOpenBrace();
                if (this.IsHyperlink)
                {
                    this.WriteHyperlink();
                }
                else
                {
                    if (this.FormattedValue == null)
                    {
                        this.WriteCellFormatting();
                    }
                    base.WriteValue(this.Value, false);
                }
                base.WriteCloseBrace();
                int? nullable = null;
                base.WriteKeyword(Keyword.CellEnd, nullable, false, false);
            }
            return base.builder.ToString();
        }

        private void CreateFormattedValue()
        {
            StringBuilder builder = new StringBuilder();
            foreach (XlRichTextRun run in this.formattedValueCore.Runs)
            {
                base.WriteOpenBrace(builder);
                if (this.CellIndent > 0)
                {
                    base.WriteKeyword(builder, Keyword.CellLeftIndent, new int?(this.CellIndent), false, false);
                }
                int? nullable = null;
                base.WriteKeyword(builder, this.HAlignment, nullable, false, false);
                base.WriteKeyword(builder, Keyword.CellFont, new int?(this.Document.FontTable.GetFontIndex(run.Font.Name)), false, false);
                base.WriteKeyword(builder, Keyword.CellFontSize, new int?((int) (2.0 * run.Font.Size)), false, false);
                base.WriteKeyword(builder, Keyword.CellFontColor, new int?(this.Document.ColorTable.GetColorIndex((Color) run.Font.Color)), false, false);
                System.Drawing.FontStyle fontStyle = FontHelper.GetFontStyle(run.Font);
                if (fontStyle != System.Drawing.FontStyle.Regular)
                {
                    nullable = null;
                    base.WriteKeyword(builder, fontStyle, nullable, false, false);
                }
                base.WriteKeyword(builder, Keyword.CellFontColor, new int?(this.Document.ColorTable.GetColorIndex((Color) run.Font.Color)), false, false);
                base.WriteSpace(builder);
                base.WriteValue(builder, this.GetRtfUnicode(run.Text), false);
                base.WriteCloseBrace(builder);
            }
            this.valueCore = builder.ToString();
        }

        private string GetRtfUnicode(string text)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char ch in text)
            {
                if (ch > this.MaxStandardAsciiCode)
                {
                    builder.AppendFormat(@"\u{0}?", Convert.ToInt32(ch));
                }
                else
                {
                    builder.Append(ch);
                }
            }
            return builder.ToString();
        }

        private void WriteCellFormatting()
        {
            int? nullable = null;
            base.WriteKeyword(this.HAlignment, nullable, false, false);
            base.WriteKeyword(Keyword.CellFont, new int?(this.Document.FontTable.GetFontIndex(this.FontName)), false, false);
            base.WriteKeyword(Keyword.CellFontSize, new int?(this.FontSize), false, false);
            if (this.FontStyle != System.Drawing.FontStyle.Regular)
            {
                nullable = null;
                base.WriteKeyword(this.FontStyle, nullable, false, false);
            }
            base.WriteKeyword(Keyword.CellFontColor, new int?(this.Document.ColorTable.GetColorIndex(this.ForeColor)), false, false);
            base.WriteSpace();
        }

        private void WriteEmptyCell()
        {
            base.WriteSpace();
            int? nullable = null;
            base.WriteKeyword(Keyword.CellHAlignmentLeft, nullable, false, false);
            base.WriteSpace();
            nullable = null;
            base.WriteKeyword(Keyword.CellEnd, nullable, false, false);
        }

        private void WriteHyperlink()
        {
            int? nullable = null;
            base.WriteKeyword(Keyword.Field, nullable, false, false);
            base.WriteOpenBrace();
            nullable = null;
            base.WriteKeyword(Keyword.FieldInstruction, nullable, false, false);
            base.WriteSpace();
            base.WriteValue("HYPERLINK", false);
            base.WriteSpace();
            base.WriteValue($""{this.TargetUrl}"", false);
            base.WriteCloseBrace();
            base.WriteOpenBrace();
            nullable = null;
            base.WriteKeyword(Keyword.FieldResult, nullable, false, false);
            base.WriteOpenBrace();
            this.WriteCellFormatting();
            base.WriteValue(this.Value, false);
            base.WriteCloseBrace();
            base.WriteCloseBrace();
        }

        public Color BackColor
        {
            get => 
                this.backColor;
            set
            {
                if (this.backColor != value)
                {
                    this.backColor = value;
                    this.Document.ColorTable.CheckColor(this.backColor);
                }
            }
        }

        public RtfTableRowCellBorder BottomBorder { get; }

        public int CellIndent
        {
            get => 
                220 * this.cellIndent;
            set
            {
                if (this.cellIndent != value)
                {
                    this.cellIndent = value;
                }
            }
        }

        public int CellRightBound { get; set; }

        public RtfDocument Document { get; }

        public string FontName
        {
            get => 
                this.fontName;
            set
            {
                if (this.fontName != value)
                {
                    this.fontName = value;
                    this.Document.FontTable.CheckFont(this.fontName);
                }
            }
        }

        public int FontSize { get; set; }

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
                    this.Document.ColorTable.CheckColor(this.foreColor);
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

        public RtfCellHAlignment HAlignment { get; set; }

        public RtfTableRowCellBorder LeftBorder { get; }

        public RtfTableRowCellBorder RightBorder { get; }

        public RtfTableRow Row { get; }

        public RtfTableRowCellBorder TopBorder { get; }

        public RtfCellVAlignment VAlignment { get; set; }

        public bool IsHyperlink =>
            !string.IsNullOrWhiteSpace(this.TargetUrl);

        public string TargetUrl { get; set; }

        public string Value
        {
            get => 
                this.valueCore;
            set
            {
                string rtfUnicode = this.GetRtfUnicode(value);
                if (this.valueCore != rtfUnicode)
                {
                    this.valueCore = rtfUnicode;
                }
            }
        }

        private enum BorderSide
        {
            Left,
            Top,
            Right,
            Bottom
        }
    }
}

