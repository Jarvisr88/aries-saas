namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows;

    public class ClrSetterPropertyTypeConverter : TypeConverter
    {
        private static readonly object synchronized;
        private static readonly Dictionary<string, DependencyProperty> nameToPropertyMap;
        private static readonly Dictionary<string, ReflectionHelper> nameToPropertyFactoryMap;

        static ClrSetterPropertyTypeConverter();
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType);
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value);
        public static DependencyProperty FromString(string propertyName);
    }
}

