namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlTop10Filter : IXlFilter, IXlFilterCriteria
    {
        private double value;

        public XlTop10Filter(double filterValue) : this(10.0, filterValue, true, false)
        {
        }

        public XlTop10Filter(double value, double filterValue, bool top, bool percent)
        {
            this.Value = value;
            this.FilterValue = filterValue;
            this.Top = top;
            this.Percent = percent;
        }

        bool IXlFilter.MeetCriteria(IXlCell cell, IXlCellFormatter cellFormatter)
        {
            if (cell == null)
            {
                return false;
            }
            XlVariantValue value2 = cell.Value;
            return (value2.IsNumeric ? (!double.IsNaN(this.FilterValue) ? (!this.Top ? (value2.NumericValue <= this.FilterValue) : (value2.NumericValue >= this.FilterValue)) : true) : false);
        }

        public bool Top { get; set; }

        public bool Percent { get; set; }

        public double Value
        {
            get => 
                this.value;
            set
            {
                if ((value < 1.0) || (value > 500.0))
                {
                    throw new ArgumentOutOfRangeException("Value out of range 1...500!");
                }
                this.value = value;
            }
        }

        public double FilterValue { get; set; }

        public XlFilterType FilterType =>
            XlFilterType.Top10;
    }
}

