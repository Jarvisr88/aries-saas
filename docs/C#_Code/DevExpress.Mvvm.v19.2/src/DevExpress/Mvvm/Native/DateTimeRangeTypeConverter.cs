namespace DevExpress.Mvvm.Native
{
    using DevExpress.Mvvm;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class DateTimeRangeTypeConverter : StringConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            DateTimeRange range;
            string str = value as string;
            return (!string.IsNullOrEmpty(str) ? (!DateTimeRange.TryParse(str, culture, out range) ? null : ((object) range)) : null);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
            (value is DateTimeRange) ? ((DateTimeRange) value).ToString(culture) : null;
    }
}

