namespace DevExpress.Utils.Design
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class LocalizableExpandableObjectTypeConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            (destinationType == typeof(string)) || base.CanConvertTo(context, destinationType);

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (context != null)
                {
                    return ((value != null) ? $"({DisplayTypeNameHelper.GetDisplayTypeName(value.GetType())})" : string.Empty);
                }
                if (value != null)
                {
                    return DisplayTypeNameHelper.GetDisplayTypeName(value.GetType());
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

