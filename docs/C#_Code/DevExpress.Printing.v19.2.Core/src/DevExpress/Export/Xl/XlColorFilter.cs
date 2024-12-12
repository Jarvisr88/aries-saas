namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlColorFilter : IXlFilter, IXlFilterCriteria
    {
        private XlPatternType patternType = XlPatternType.Solid;
        private XlColor color = XlColor.Auto;
        private XlColor patternColor = XlColor.Auto;

        public XlColorFilter()
        {
            this.FilterByCellColor = true;
        }

        bool IXlFilter.MeetCriteria(IXlCell cell, IXlCellFormatter cellFormatter) => 
            !this.FilterByCellColor ? this.MeetCellFontColor(cell) : this.MeetCellFillColor(cell);

        private bool MeetCellFillColor(IXlCell cell)
        {
            if (this.color.Rgb.IsEmpty && (this.PatternType == XlPatternType.Solid))
            {
                if ((cell == null) || ((cell.Formatting == null) || (cell.Formatting.Fill == null)))
                {
                    return true;
                }
                XlFill fill = cell.Formatting.Fill;
                return (((fill.PatternType == XlPatternType.Solid) || (fill.PatternType == XlPatternType.None)) ? fill.ForeColor.Rgb.IsEmpty : false);
            }
            if ((cell == null) || ((cell.Formatting == null) || (cell.Formatting.Fill == null)))
            {
                return false;
            }
            XlFill fill2 = cell.Formatting.Fill;
            return ((this.PatternType == fill2.PatternType) && ((fill2.PatternType != XlPatternType.None) && ((this.PatternType != XlPatternType.Solid) ? ((this.color.Rgb == fill2.BackColor.Rgb) && (this.patternColor.Rgb == fill2.ForeColor.Rgb)) : (this.color.Rgb == fill2.ForeColor.Rgb))));
        }

        private bool MeetCellFontColor(IXlCell cell)
        {
            XlColor rgb = this.color.Rgb;
            if (rgb.IsEmpty)
            {
                if ((cell == null) || ((cell.Formatting == null) || (cell.Formatting.Font == null)))
                {
                    return true;
                }
                XlFont font = cell.Formatting.Font;
                return (font.Color.Rgb.IsEmpty || ReferenceEquals(font.Color, XlColor.FromTheme(XlThemeColor.Dark1, 0.0)));
            }
            if ((cell == null) || ((cell.Formatting == null) || (cell.Formatting.Font == null)))
            {
                return false;
            }
            XlColor color3 = cell.Formatting.Font.Color.Rgb;
            return (!color3.IsEmpty ? (rgb.Rgb == color3.Rgb) : false);
        }

        public bool FilterByCellColor { get; set; }

        public XlPatternType PatternType
        {
            get => 
                this.patternType;
            set
            {
                if (value == XlPatternType.None)
                {
                    throw new ArgumentException("Wrong pattern type value!");
                }
                this.patternType = value;
            }
        }

        public XlColor Color
        {
            get => 
                this.color;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.color = empty;
            }
        }

        public XlColor PatternColor
        {
            get => 
                this.patternColor;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.patternColor = empty;
            }
        }

        public XlFilterType FilterType =>
            XlFilterType.Color;
    }
}

