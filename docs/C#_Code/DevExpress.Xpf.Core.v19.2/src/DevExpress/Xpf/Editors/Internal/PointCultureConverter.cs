namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.ReflectionExtensions;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Reflection;
    using System.Windows;

    public sealed class PointCultureConverter : TypeConverter
    {
        private static readonly Type TokenizerType = typeof(ModifierKeys).Assembly.GetType("MS.Internal.TokenizerHelper");

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            (destinationType == typeof(string)) || base.CanConvertTo(context, destinationType);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
            {
                throw base.GetConvertFromException(value);
            }
            string source = value as string;
            return ((source == null) ? base.ConvertFrom(context, culture, value) : this.ParsePoint(source, culture));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (!(value is Point))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            Point point = (Point) value;
            return ((destinationType == typeof(string)) ? point.ToString(CultureInfo.InvariantCulture) : base.ConvertTo(context, culture, value, destinationType));
        }

        private Point ParsePoint(string source, IFormatProvider provider)
        {
            Type[] types = new Type[] { typeof(string), typeof(IFormatProvider) };
            object[] parameters = new object[] { source, provider };
            ITokenizerHelper helper = TokenizerType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, types, null).Invoke(parameters).Wrap<ITokenizerHelper>();
            Point point = new Point(Convert.ToDouble(helper.NextTokenRequired(), provider), Convert.ToDouble(helper.NextTokenRequired(), provider));
            helper.LastTokenRequired();
            return point;
        }
    }
}

