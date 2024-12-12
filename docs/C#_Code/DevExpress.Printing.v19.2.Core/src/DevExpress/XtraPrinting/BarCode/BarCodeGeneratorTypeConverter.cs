namespace DevExpress.XtraPrinting.BarCode
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;

    public class BarCodeGeneratorTypeConverter : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType);
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType);
        private static InstanceDescriptor DoConvertToInstanceDescriptor(object value);
        private static string DoConvertToString(object value, Type destinationType);
    }
}

