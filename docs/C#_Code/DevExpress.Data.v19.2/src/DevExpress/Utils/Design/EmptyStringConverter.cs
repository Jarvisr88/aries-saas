namespace DevExpress.Utils.Design
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class EmptyStringConverter : StringConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            (sourceType != typeof(string)) ? base.CanConvertFrom(context, sourceType) : false;

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
            (destinationType != typeof(string)) ? base.ConvertTo(context, culture, value, destinationType) : (!string.IsNullOrEmpty(value as string) ? "(Not Empty)" : string.Empty);
    }
}

