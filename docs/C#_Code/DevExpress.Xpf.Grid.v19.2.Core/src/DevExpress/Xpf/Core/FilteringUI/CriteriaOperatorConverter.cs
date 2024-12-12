namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class CriteriaOperatorConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string criteria = value as string;
            return ((criteria == null) ? base.ConvertFrom(context, culture, value) : CriteriaOperator.Parse(criteria, new object[0]));
        }
    }
}

