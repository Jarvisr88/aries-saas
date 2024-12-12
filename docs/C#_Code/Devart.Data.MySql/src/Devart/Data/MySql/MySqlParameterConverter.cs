﻿namespace Devart.Data.MySql
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    internal class MySqlParameterConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType);
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType);
    }
}
