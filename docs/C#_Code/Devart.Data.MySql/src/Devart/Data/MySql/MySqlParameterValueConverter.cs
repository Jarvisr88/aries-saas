namespace Devart.Data.MySql
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    internal class MySqlParameterValueConverter : StringConverter
    {
        public override bool a(ITypeDescriptorContext A_0, Type A_1);
        public override object a(ITypeDescriptorContext A_0, CultureInfo A_1, object A_2);
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType);
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType);
    }
}

