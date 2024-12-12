namespace ActiproSoftware.ComponentModel
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class GenericExpandableNullableObjectConverter : ExpandableNullableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destType);
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType);
    }
}

