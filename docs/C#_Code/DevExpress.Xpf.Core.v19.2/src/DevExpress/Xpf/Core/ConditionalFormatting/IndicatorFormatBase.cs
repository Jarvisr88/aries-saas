namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Windows;
    using System.Windows.Media;

    public abstract class IndicatorFormatBase : Freezable
    {
        protected IndicatorFormatBase()
        {
        }

        public virtual Brush CoerceBackground(Brush value, FormatValueProvider provider, decimal? minValue, decimal? maxValue) => 
            value;

        public virtual DataBarFormatInfo CoerceDataBarFormatInfo(DataBarFormatInfo value, FormatValueProvider provider, decimal? minValue, decimal? maxValue) => 
            value;

        internal static decimal? GetDecimalValue(object value)
        {
            if (value == null)
            {
                return null;
            }
            TypeCode typeCode = Type.GetTypeCode(value.GetType());
            if (typeCode == TypeCode.DateTime)
            {
                return new decimal?(((DateTime) value).Ticks);
            }
            if (!IsNumericTypeCode(typeCode))
            {
                return null;
            }
            try
            {
                return new decimal?(Convert.ToDecimal(value));
            }
            catch (OverflowException)
            {
                return null;
            }
        }

        internal static T GetNormalizedValue<T>(T value, T min, T max) where T: IComparable => 
            (value.CompareTo(max) <= 0) ? ((value.CompareTo(min) >= 0) ? value : min) : max;

        internal static decimal? GetSummaryValue(FormatValueProvider provider, ConditionalFormatSummaryType summaryType) => 
            GetDecimalValue(provider.GetTotalSummaryValue(summaryType));

        internal static decimal? GetSummaryValue(FormatValueProvider provider, ConditionalFormatSummaryType summaryType, decimal? value)
        {
            decimal? nullable = value;
            return ((nullable != null) ? nullable : GetSummaryValue(provider, summaryType));
        }

        internal static bool IsNumericOrDateTimeTypeCode(TypeCode typeCode) => 
            IsNumericTypeCode(typeCode) || (typeCode == TypeCode.DateTime);

        internal static bool IsNumericTypeCode(TypeCode typeCode) => 
            (typeCode >= TypeCode.SByte) && (typeCode <= TypeCode.Decimal);
    }
}

