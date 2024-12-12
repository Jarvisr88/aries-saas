namespace DevExpress.Data.Linq.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;

    public class LinqServerModeSourceObjectTypeConverter : TypeListConverter
    {
        protected SortedList<string, Type> typesCache;
        public const string None = "(none)";

        public LinqServerModeSourceObjectTypeConverter();
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType);
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object val);
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object val, Type destType);
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context);
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context);
    }
}

