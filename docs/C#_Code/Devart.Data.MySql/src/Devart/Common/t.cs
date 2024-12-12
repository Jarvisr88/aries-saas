namespace Devart.Common
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    internal class t : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType);
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType);
    }
}

