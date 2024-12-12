namespace DevExpress.Utils.Design
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class CollectionTypeConverter : TypeConverter
    {
        private static DXDisplayNameAttribute attr = new DXDisplayNameAttribute(typeof(ResFinder), "PropertyNamesRes", "System.Collections.CollectionBase", "(Collection)");

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            (destinationType == typeof(string)) || base.CanConvertTo(context, destinationType);

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
            !(destinationType == typeof(string)) ? base.ConvertTo(context, culture, value, destinationType) : DisplayName;

        public static string DisplayName =>
            attr.DisplayName;
    }
}

