namespace DevExpress.Data.Browsing.Design
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public class DesignBindingConverterBase : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType);
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType);

        protected virtual string NoneString { get; }

        protected virtual string UntypedDataSourceName { get; }
    }
}

