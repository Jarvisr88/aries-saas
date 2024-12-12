namespace DevExpress.Export.Xl
{
    using System;
    using System.Runtime.CompilerServices;

    public class XlDynamicFilter : IXlFilter, IXlFilterCriteria
    {
        public XlDynamicFilter(XlDynamicFilterType dynamicFilterType) : this(dynamicFilterType, XlVariantValue.Empty, XlVariantValue.Empty)
        {
            XlDynamicFilterCalculator.CalculateValues(this, DateTime.Today);
        }

        public XlDynamicFilter(XlDynamicFilterType dynamicFilterType, XlVariantValue value) : this(dynamicFilterType, value, XlVariantValue.Empty)
        {
        }

        public XlDynamicFilter(XlDynamicFilterType dynamicFilterType, XlVariantValue value, XlVariantValue maxValue)
        {
            this.DynamicFilterType = dynamicFilterType;
            this.Value = value;
            this.MaxValue = maxValue;
        }

        private int CalculateMonth(XlDynamicFilterType type) => 
            Math.Min(12, Math.Max(1, ((int) (type - XlDynamicFilterType.Month1)) + 1));

        private int CalculateQuarter(XlDynamicFilterType type) => 
            Math.Min(4, Math.Max(1, ((int) (type - XlDynamicFilterType.Quarter1)) + 1));

        private int CalculateQuarter(XlVariantValue value) => 
            ((value.DateTimeValue.Month - 1) / 3) + 1;

        bool IXlFilter.MeetCriteria(IXlCell cell, IXlCellFormatter cellFormatter)
        {
            if (cell == null)
            {
                return false;
            }
            XlVariantValue value2 = cell.Value;
            return (value2.IsNumeric ? ((this.DynamicFilterType != XlDynamicFilterType.BelowAverage) ? ((this.DynamicFilterType != XlDynamicFilterType.AboveAverage) ? (((cell.Formatting != null) && cell.Formatting.IsDateTimeNumberFormat()) && (((this.DynamicFilterType < XlDynamicFilterType.Tomorrow) || (this.DynamicFilterType > XlDynamicFilterType.YearToDate)) ? (((this.DynamicFilterType < XlDynamicFilterType.Quarter1) || (this.DynamicFilterType > XlDynamicFilterType.Quarter4)) ? (value2.DateTimeValue.Month == this.CalculateMonth(this.DynamicFilterType)) : (this.CalculateQuarter(value2) == this.CalculateQuarter(this.DynamicFilterType))) : ((value2.NumericValue >= this.Value.NumericValue) && (value2.NumericValue < this.MaxValue.NumericValue)))) : (!this.Value.IsNumeric || (value2.NumericValue > this.Value.NumericValue))) : (!this.Value.IsNumeric || (value2.NumericValue < this.Value.NumericValue))) : false);
        }

        internal bool DynamicFilterMaxValueRequired() => 
            (this.DynamicFilterType >= XlDynamicFilterType.Tomorrow) && (this.DynamicFilterType <= XlDynamicFilterType.YearToDate);

        internal bool DynamicFilterValueRequired() => 
            this.DynamicFilterType <= XlDynamicFilterType.YearToDate;

        internal int GetCriteriaCount()
        {
            int num = 0;
            if (this.DynamicFilterValueRequired() && this.Value.IsNumeric)
            {
                num++;
            }
            if (this.DynamicFilterMaxValueRequired() && this.MaxValue.IsNumeric)
            {
                num++;
            }
            return num;
        }

        internal XlFilterOperator GetFilterOperator(bool maxValue) => 
            (this.DynamicFilterType != XlDynamicFilterType.AboveAverage) ? ((this.DynamicFilterType != XlDynamicFilterType.BelowAverage) ? (maxValue ? XlFilterOperator.LessThan : XlFilterOperator.GreaterThanOrEqual) : XlFilterOperator.LessThan) : XlFilterOperator.GreaterThan;

        public XlDynamicFilterType DynamicFilterType { get; set; }

        public XlVariantValue Value { get; set; }

        public XlVariantValue MaxValue { get; set; }

        public XlFilterType FilterType =>
            XlFilterType.Dynamic;
    }
}

