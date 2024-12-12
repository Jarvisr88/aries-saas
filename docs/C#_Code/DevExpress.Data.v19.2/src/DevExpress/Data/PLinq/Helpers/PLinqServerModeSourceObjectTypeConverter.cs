namespace DevExpress.Data.PLinq.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;

    public class PLinqServerModeSourceObjectTypeConverter : TypeListConverter
    {
        private SortedList<string, Type> typesCache;
        public const string None = "(none)";

        public PLinqServerModeSourceObjectTypeConverter();
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType);
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object val);
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object val, Type destType);
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context);
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context);
    }
}

