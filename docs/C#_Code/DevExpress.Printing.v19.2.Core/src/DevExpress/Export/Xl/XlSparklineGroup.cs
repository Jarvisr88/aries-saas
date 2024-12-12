namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class XlSparklineGroup
    {
        private readonly List<XlSparkline> sparklines;
        private XlColor colorSeries;
        private XlColor colorNegative;
        private XlColor colorAxis;
        private XlColor colorMarker;
        private XlColor colorFirst;
        private XlColor colorLast;
        private XlColor colorHigh;
        private XlColor colorLow;
        private double lineWeight;

        public XlSparklineGroup()
        {
            this.sparklines = new List<XlSparkline>();
            this.colorSeries = XlColor.FromTheme(XlThemeColor.Accent1, -0.5);
            this.colorNegative = XlColor.FromTheme(XlThemeColor.Accent2, 0.0);
            this.colorAxis = XlColor.FromArgb(0, 0, 0);
            this.colorMarker = XlColor.FromTheme(XlThemeColor.Accent1, -0.5);
            this.colorFirst = XlColor.FromTheme(XlThemeColor.Accent1, 0.4);
            this.colorLast = XlColor.FromTheme(XlThemeColor.Accent1, 0.4);
            this.colorHigh = XlColor.FromTheme(XlThemeColor.Accent1, 0.0);
            this.colorLow = XlColor.FromTheme(XlThemeColor.Accent1, 0.0);
            this.lineWeight = 0.75;
        }

        public XlSparklineGroup(XlCellRange dataRange, XlCellRange location)
        {
            this.sparklines = new List<XlSparkline>();
            this.colorSeries = XlColor.FromTheme(XlThemeColor.Accent1, -0.5);
            this.colorNegative = XlColor.FromTheme(XlThemeColor.Accent2, 0.0);
            this.colorAxis = XlColor.FromArgb(0, 0, 0);
            this.colorMarker = XlColor.FromTheme(XlThemeColor.Accent1, -0.5);
            this.colorFirst = XlColor.FromTheme(XlThemeColor.Accent1, 0.4);
            this.colorLast = XlColor.FromTheme(XlThemeColor.Accent1, 0.4);
            this.colorHigh = XlColor.FromTheme(XlThemeColor.Accent1, 0.0);
            this.colorLow = XlColor.FromTheme(XlThemeColor.Accent1, 0.0);
            this.lineWeight = 0.75;
            Guard.ArgumentNotNull(dataRange, "dataRange");
            Guard.ArgumentNotNull(location, "location");
            if ((location.RowCount > 1) && (location.ColumnCount > 1))
            {
                throw new ArgumentException("Location must be one column wide or one row tall");
            }
            if ((location.RowCount == 1) && (location.ColumnCount == 1))
            {
                if ((dataRange.RowCount > 1) && (dataRange.ColumnCount > 1))
                {
                    throw new ArgumentException("Data range must be one column wide or one row tall");
                }
                this.sparklines.Add(new XlSparkline(dataRange, location));
            }
            else if (location.RowCount == 1)
            {
                if (dataRange.ColumnCount != location.ColumnCount)
                {
                    throw new ArgumentException("Data range must have same width as location");
                }
                for (int i = 0; i < location.ColumnCount; i++)
                {
                    this.sparklines.Add(new XlSparkline(this.GetColumnSlice(dataRange, i), this.GetColumnSlice(location, i)));
                }
            }
            else if (location.ColumnCount == 1)
            {
                if (dataRange.RowCount != location.RowCount)
                {
                    throw new ArgumentException("Data range must have same height as location");
                }
                for (int i = 0; i < location.RowCount; i++)
                {
                    this.sparklines.Add(new XlSparkline(this.GetRowSlice(dataRange, i), this.GetRowSlice(location, i)));
                }
            }
        }

        private XlCellRange GetColumnSlice(XlCellRange range, int index)
        {
            XlCellPosition topLeft = new XlCellPosition(range.FirstColumn + index, range.FirstRow, range.TopLeft.ColumnType, range.TopLeft.RowType);
            return new XlCellRange(range.SheetName, topLeft, new XlCellPosition(range.FirstColumn + index, range.LastRow, range.TopLeft.ColumnType, range.TopLeft.RowType));
        }

        private XlCellRange GetRowSlice(XlCellRange range, int index)
        {
            XlCellPosition topLeft = new XlCellPosition(range.FirstColumn, range.FirstRow + index, range.TopLeft.ColumnType, range.TopLeft.RowType);
            return new XlCellRange(range.SheetName, topLeft, new XlCellPosition(range.LastColumn, range.FirstRow + index, range.TopLeft.ColumnType, range.TopLeft.RowType));
        }

        public XlSparklineType SparklineType { get; set; }

        public IList<XlSparkline> Sparklines =>
            this.sparklines;

        public XlColor ColorSeries
        {
            get => 
                this.colorSeries;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.colorSeries = empty;
            }
        }

        public XlColor ColorNegative
        {
            get => 
                this.colorNegative;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.colorNegative = empty;
            }
        }

        public XlColor ColorAxis
        {
            get => 
                this.colorAxis;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.colorAxis = empty;
            }
        }

        public XlColor ColorMarker
        {
            get => 
                this.colorMarker;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.colorMarker = empty;
            }
        }

        public XlColor ColorFirst
        {
            get => 
                this.colorFirst;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.colorFirst = empty;
            }
        }

        public XlColor ColorLast
        {
            get => 
                this.colorLast;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.colorLast = empty;
            }
        }

        public XlColor ColorHigh
        {
            get => 
                this.colorHigh;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.colorHigh = empty;
            }
        }

        public XlColor ColorLow
        {
            get => 
                this.colorLow;
            set
            {
                XlColor empty = value;
                if (value == null)
                {
                    XlColor local1 = value;
                    empty = XlColor.Empty;
                }
                this.colorLow = empty;
            }
        }

        public XlDisplayBlanksAs DisplayBlanksAs { get; set; }

        public double LineWeight
        {
            get => 
                this.lineWeight;
            set
            {
                if ((value < 0.0) || (value > 1584.0))
                {
                    throw new ArgumentOutOfRangeException("LineWeight out of range 0...1584");
                }
                this.lineWeight = value;
            }
        }

        public XlCellRange DateRange { get; set; }

        public XlSparklineAxisScaling MinScaling { get; set; }

        public XlSparklineAxisScaling MaxScaling { get; set; }

        public double ManualMax { get; set; }

        public double ManualMin { get; set; }

        public bool HighlightNegative { get; set; }

        public bool HighlightFirst { get; set; }

        public bool HighlightLast { get; set; }

        public bool HighlightHighest { get; set; }

        public bool HighlightLowest { get; set; }

        public bool DisplayXAxis { get; set; }

        public bool DisplayMarkers { get; set; }

        public bool DisplayHidden { get; set; }

        public bool RightToLeft { get; set; }
    }
}

