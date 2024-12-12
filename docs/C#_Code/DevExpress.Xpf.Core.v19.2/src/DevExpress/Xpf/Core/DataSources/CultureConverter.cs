namespace DevExpress.Xpf.Core.DataSources
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class CultureConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            !(sourceType == typeof(string)) ? base.CanConvertFrom(context, sourceType) : true;

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string))
            {
                return base.ConvertFrom(context, culture, value);
            }
            try
            {
                return new CultureInfo((string) value);
            }
            catch (Exception)
            {
                return CultureInfo.CurrentCulture;
            }
        }
    }
}

