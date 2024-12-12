namespace DevExpress.Utils.Design
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class MarginsFConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            destinationType == typeof(string);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string str = value as string;
            return ((str == null) ? base.ConvertFrom(context, culture, value) : XmlMarginsFConverter.Instance.FromString(str));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            return ((!(value is MarginsF) || !(destinationType == typeof(string))) ? base.ConvertTo(context, culture, value, destinationType) : XmlMarginsFConverter.Instance.ToString(value));
        }

        private class XmlMarginsFConverter : StructFloatConverter
        {
            public static readonly MarginsFConverter.XmlMarginsFConverter Instance = new MarginsFConverter.XmlMarginsFConverter();

            private XmlMarginsFConverter()
            {
            }

            protected override object CreateObject(float[] values) => 
                new MarginsF(values[0], values[1], values[2], values[3]);

            protected override float[] GetValues(object obj)
            {
                MarginsF sf = (MarginsF) obj;
                return new float[] { sf.Left, sf.Right, sf.Top, sf.Bottom };
            }

            public override System.Type Type =>
                typeof(MarginsF);
        }
    }
}

