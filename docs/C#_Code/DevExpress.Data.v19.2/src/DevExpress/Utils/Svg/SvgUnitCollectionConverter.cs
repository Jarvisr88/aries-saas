namespace DevExpress.Utils.Svg
{
    using DevExpress.Utils.Filtering.Internal;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class SvgUnitCollectionConverter : TypeConverter
    {
        private static readonly SvgUnitConverter unitConverter = new SvgUnitConverter();
        private static StringComparison SVG_UNIT_STRINGCOMPARISON = StringComparison.InvariantCultureIgnoreCase;

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            !(destinationType == typeof(string)) ? base.CanConvertTo(context, destinationType) : true;

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string))
            {
                base.ConvertFrom(context, culture, value);
            }
            string strA = ((string) value).Trim();
            if (string.Compare(strA, SvgUnit.None, SVG_UNIT_STRINGCOMPARISON) == 0)
            {
                return null;
            }
            char[] separator = new char[] { ' ', '\r', '\n', '\t', ',' };
            string[] strArray = strA.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            SvgUnitCollection result = new SvgUnitCollection();
            foreach (string str2 in strArray)
            {
                Action<SvgUnit> <>9__0;
                SvgUnit @this = (SvgUnit) unitConverter.ConvertFrom(str2);
                Action<SvgUnit> @do = <>9__0;
                if (<>9__0 == null)
                {
                    Action<SvgUnit> local1 = <>9__0;
                    @do = <>9__0 = delegate (SvgUnit x) {
                        result.Add(x);
                    };
                }
                @this.Do<SvgUnit>(@do);
            }
            return result;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
            ((value == null) || !(destinationType == typeof(string))) ? base.ConvertTo(context, culture, value, destinationType) : ((SvgUnitCollection) value).ToString();
    }
}

