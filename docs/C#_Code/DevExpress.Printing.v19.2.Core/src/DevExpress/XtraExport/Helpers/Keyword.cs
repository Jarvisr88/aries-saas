namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Drawing;

    internal class Keyword
    {
        private readonly string keyword;
        public static Keyword RTF = @"\rtf";
        public static Keyword Ansi = @"\ansi";
        public static Keyword DefaultFont = @"\deff";
        public static Keyword Plain = @"\plain";
        public static Keyword Field = @"\field";
        public static Keyword FieldInstruction = @"\*\fldinst";
        public static Keyword FieldResult = @"\fldrslt";
        public static Keyword FontTable = @"\fonttbl";
        public static Keyword Font = @"\f";
        public static Keyword FontFamilyNil = @"\fnil";
        public static Keyword ColorTable = @"\colortbl";
        public static Keyword ColorRed = @"\red";
        public static Keyword ColorGreen = @"\green";
        public static Keyword ColorBlue = @"\blue";
        public static Keyword RowStart = @"\trowd";
        public static Keyword RowEnd = @"\row";
        public static Keyword RowGap = @"\trgaph";
        public static Keyword RowLeft = @"\trleft";
        public static Keyword RowHeight = @"\trrh";
        public static Keyword ParagraphDefault = @"\pard";
        public static Keyword InTable = @"\intbl";
        public static Keyword CellLeftPadding = @"\trpaddl";
        public static Keyword CellLeftPaddingUnits = @"\trpaddfl";
        public static Keyword CellRightPadding = @"\trpaddr";
        public static Keyword CellRightPaddingUnits = @"\trpaddfr";
        public static Keyword CellRightBound = @"\cellx";
        public static Keyword CellLeftIndent = @"\li";
        public static Keyword CellEnd = @"\cell";
        public static Keyword CellVAlignmentTop = @"\clvertalt";
        public static Keyword CellVAlignmentCenter = @"\clvertalc";
        public static Keyword CellVAlignmentBottom = @"\clvertalb";
        public static Keyword CellHAlignmentLeft = @"\ql";
        public static Keyword CellHAlignmentCenter = @"\qc";
        public static Keyword CellHAlignmentRight = @"\qr";
        public static Keyword CellHAlignmentDistributed = @"\qd";
        public static Keyword CellHAlignmentJustify = @"\qj";
        public static Keyword CellFont = @"\f";
        public static Keyword CellFontColor = @"\cf";
        public static Keyword CellFontSize = @"\fs";
        public static Keyword CellFontUnderline = @"\ul";
        public static Keyword CellFontBold = @"\b";
        public static Keyword CellFontItalic = @"\i";
        public static Keyword CellFontStrikeThrough = @"\strike";
        public static Keyword CellPatternForeColor = @"\clcfpat";
        public static Keyword CellPatternBackColor = @"\clcbpat";
        public static Keyword CellShading = @"\clshdng";
        public static Keyword CellMergeFirstCell = @"\clmgf";
        public static Keyword CellMergeLastCell = @"\clmrg";
        public static Keyword CellVMergeFirstCell = @"\clvmgf";
        public static Keyword CellVMergeLastCell = @"\clvmrg";
        public static Keyword CellBorderLeft = @"\clbrdrl";
        public static Keyword CellBorderTop = @"\clbrdrt";
        public static Keyword CellBorderRight = @"\clbrdrr";
        public static Keyword CellBorderBottom = @"\clbrdrb";
        public static Keyword CellBorderWidth = @"\brdrw";
        public static Keyword CellBorderColor = @"\brdrcf";
        public static Keyword CellBorderStyleNone = @"\brdrnone";
        public static Keyword CellBorderStyleSingle = @"\brdrs";
        public static Keyword CellBorderStyleDashedSmall = @"\brdrdashsm";
        public static Keyword CellBorderStyleDotted = @"\brdrdot";
        public static Keyword CellBorderStyleDouble = @"\brdrdb";
        public static Keyword CellBorderStyleHair = @"\brdrhair";
        public static Keyword CellBorderStyleDashDotted = @"\brdrdashd";
        public static Keyword CellBorderStyleDashDotDotted = @"\brdrdashdd";

        protected Keyword(string keyword)
        {
            this.keyword = keyword;
        }

        public static implicit operator string(Keyword keyword) => 
            keyword.keyword;

        public static implicit operator Keyword(RtfCellBorderStyle style)
        {
            switch (style)
            {
                case RtfCellBorderStyle.None:
                    return CellBorderStyleNone;

                case RtfCellBorderStyle.Dashed:
                case RtfCellBorderStyle.MediumDashed:
                    return CellBorderStyleDashedSmall;

                case RtfCellBorderStyle.Dotted:
                case RtfCellBorderStyle.Hair:
                    return CellBorderStyleDotted;

                case RtfCellBorderStyle.Double:
                    return CellBorderStyleDouble;

                case RtfCellBorderStyle.DashDot:
                case RtfCellBorderStyle.MediumDashDot:
                    return CellBorderStyleDashDotted;

                case RtfCellBorderStyle.DashDotDot:
                case RtfCellBorderStyle.MediumDashDotDot:
                    return CellBorderStyleDashDotDotted;
            }
            return CellBorderStyleSingle;
        }

        public static implicit operator Keyword(RtfCellHAlignment alignmnent)
        {
            switch (alignmnent)
            {
                case RtfCellHAlignment.Center:
                    return CellHAlignmentCenter;

                case RtfCellHAlignment.Right:
                    return CellHAlignmentRight;

                case RtfCellHAlignment.Distributed:
                    return CellHAlignmentDistributed;

                case RtfCellHAlignment.Justify:
                    return CellHAlignmentJustify;
            }
            return CellHAlignmentLeft;
        }

        public static implicit operator Keyword(RtfCellVAlignment alignmnent)
        {
            switch (alignmnent)
            {
                case RtfCellVAlignment.Center:
                    return CellVAlignmentCenter;

                case RtfCellVAlignment.Bottom:
                    return CellVAlignmentBottom;
            }
            return CellVAlignmentTop;
        }

        public static implicit operator Keyword(FontStyle style)
        {
            string str = string.Empty;
            if (style != FontStyle.Regular)
            {
                if (style.HasFlag(FontStyle.Bold))
                {
                    str = str + ((string) CellFontBold);
                }
                if (style.HasFlag(FontStyle.Italic))
                {
                    str = str + ((string) CellFontItalic);
                }
                if (style.HasFlag(FontStyle.Underline))
                {
                    str = str + ((string) CellFontUnderline);
                }
                if (style.HasFlag(FontStyle.Strikeout))
                {
                    str = str + ((string) CellFontStrikeThrough);
                }
            }
            return str;
        }

        public static implicit operator Keyword(string keyword) => 
            new Keyword(keyword);
    }
}

