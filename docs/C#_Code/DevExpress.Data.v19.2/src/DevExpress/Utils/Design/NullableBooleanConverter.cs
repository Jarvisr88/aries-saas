namespace DevExpress.Utils.Design
{
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class NullableBooleanConverter : TypeConverter
    {
        private static readonly DefaultBooleanConverter defaultBooleanConverter = new DefaultBooleanConverter();
        private readonly NullableTypeConverterStandardPropertyGridAspect standardPropertyGridAspect = new NullableTypeConverterStandardPropertyGridAspect();

        private static DefaultBoolean BooleanToDefaultBoolean(bool? value)
        {
            bool? nullable = value;
            if (nullable == null)
            {
                return DefaultBoolean.Default;
            }
            bool valueOrDefault = nullable.GetValueOrDefault();
            if (!valueOrDefault)
            {
                return DefaultBoolean.False;
            }
            if (!valueOrDefault)
            {
                throw new InvalidOperationException();
            }
            return DefaultBoolean.True;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            defaultBooleanConverter.CanConvertFrom(context, sourceType);

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            defaultBooleanConverter.CanConvertTo(context, destinationType);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            this.standardPropertyGridAspect.OnConvertFrom(context);
            return DefaultBooleanToBoolean((DefaultBoolean) defaultBooleanConverter.ConvertFrom(context, culture, value));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            this.standardPropertyGridAspect.OnConvertTo(context);
            return defaultBooleanConverter.ConvertTo(context, culture, BooleanToDefaultBoolean((bool?) value), destinationType);
        }

        private static bool? DefaultBooleanToBoolean(DefaultBoolean value)
        {
            switch (value)
            {
                case DefaultBoolean.True:
                    return true;

                case DefaultBoolean.False:
                    return false;

                case DefaultBoolean.Default:
                    return null;
            }
            throw new InvalidOperationException();
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            bool?[] values = new bool?[3];
            values[1] = true;
            values[2] = false;
            return new TypeConverter.StandardValuesCollection(values);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => 
            true;

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => 
            true;
    }
}

