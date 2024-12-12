namespace DevExpress.Utils.Svg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class SvgStyleConverter : TypeConverter
    {
        public static readonly SvgStyleConverter Instance = new SvgStyleConverter();

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string))
            {
                return base.ConvertFrom(context, culture, value);
            }
            SvgStyle style = SvgStyleParser.ReadStyleAttribute(value as string);
            return ((style.Attributes.Count != 0) ? style : null);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            SvgStyle style = value as SvgStyle;
            if ((style == null) || (style.Attributes.Count <= 0))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            Func<KeyValuePair<string, string>, string> selector = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<KeyValuePair<string, string>, string> local1 = <>c.<>9__2_0;
                selector = <>c.<>9__2_0 = x => x.Key + ":" + x.Value;
            }
            return string.Join(";", style.Attributes.Select<KeyValuePair<string, string>, string>(selector).ToArray<string>());
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgStyleConverter.<>c <>9 = new SvgStyleConverter.<>c();
            public static Func<KeyValuePair<string, string>, string> <>9__2_0;

            internal string <ConvertTo>b__2_0(KeyValuePair<string, string> x) => 
                x.Key + ":" + x.Value;
        }
    }
}

