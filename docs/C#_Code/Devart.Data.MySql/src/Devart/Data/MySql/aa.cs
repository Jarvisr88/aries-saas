﻿namespace Devart.Data.MySql
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    internal class aa : TypeConverter
    {
        protected readonly MySqlType a;

        protected aa(MySqlType A_0);
        public override bool a(ITypeDescriptorContext A_0, Type A_1);
        public override object a(ITypeDescriptorContext A_0, CultureInfo A_1, object A_2);
        public override object a(ITypeDescriptorContext A_0, CultureInfo A_1, object A_2, Type A_3);
        public override bool b(ITypeDescriptorContext A_0, Type A_1);
    }
}

