namespace DevExpress.Utils.Svg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class SvgTransformConverter : TypeConverter
    {
        public static readonly SvgTransformConverter Instance = new SvgTransformConverter();

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            !(sourceType == typeof(string)) ? base.CanConvertFrom(context, sourceType) : true;

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            !(destinationType == typeof(string)) ? base.CanConvertTo(context, destinationType) : true;

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string))
            {
                return base.ConvertFrom(context, culture, value);
            }
            SvgTransformCollection transforms = new SvgTransformCollection();
            foreach (string str2 in this.SplitTransforms((string) value))
            {
                if (!string.IsNullOrEmpty(str2))
                {
                    char[] separator = new char[] { '(' };
                    string[] strArray = str2.Split(separator);
                    double[] numbers = CoordinateParser.GetNumbers(strArray[1].Trim());
                    SvgTransform transform = SvgElementCreator.CreateTransform(strArray[0].Trim().ToLower(), numbers);
                    if (transform != null)
                    {
                        transforms.Add(transform);
                    }
                }
            }
            return transforms;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                SvgTransformCollection source = value as SvgTransformCollection;
                if (source != null)
                {
                    Func<SvgTransform, string> selector = <>c.<>9__5_0;
                    if (<>c.<>9__5_0 == null)
                    {
                        Func<SvgTransform, string> local1 = <>c.<>9__5_0;
                        selector = <>c.<>9__5_0 = t => t.ToString();
                    }
                    return string.Join(" ", source.Select<SvgTransform, string>(selector).ToArray<string>());
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        private IEnumerable<string> SplitTransforms(string transforms)
        {
            char[] separator = new char[] { ')' };
            return transforms.Trim().Split(separator);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgTransformConverter.<>c <>9 = new SvgTransformConverter.<>c();
            public static Func<SvgTransform, string> <>9__5_0;

            internal string <ConvertTo>b__5_0(SvgTransform t) => 
                t.ToString();
        }
    }
}

