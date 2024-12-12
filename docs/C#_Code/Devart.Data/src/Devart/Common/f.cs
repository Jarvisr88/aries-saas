namespace Devart.Common
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Text;

    internal class f : TypeConverter
    {
        public override bool a(ITypeDescriptorContext A_0, Type A_1) => 
            !ReferenceEquals(A_1, typeof(string)) ? base.CanConvertFrom(A_0, A_1) : true;

        public override object a(ITypeDescriptorContext A_0, CultureInfo A_1, object A_2)
        {
            if ((A_2 == null) || ((A_2 as string) == ""))
            {
                return null;
            }
            if (!(A_2 is string))
            {
                return base.ConvertFrom(A_0, A_1, A_2);
            }
            char[] separator = new char[] { ';' };
            return ((string) A_2).Split(separator);
        }

        public override object a(ITypeDescriptorContext A_0, CultureInfo A_1, object A_2, Type A_3)
        {
            if (!ReferenceEquals(A_3, typeof(string)) || !(A_2 is string[]))
            {
                return base.ConvertTo(A_0, A_1, A_2, A_3);
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str in (string[]) A_2)
            {
                if (builder.Length != 0)
                {
                    builder.Append(";");
                }
                builder.Append(str);
            }
            return builder?.ToString();
        }

        public override bool b(ITypeDescriptorContext A_0, Type A_1) => 
            !ReferenceEquals(A_1, typeof(string[])) ? base.CanConvertTo(A_0, A_1) : true;
    }
}

