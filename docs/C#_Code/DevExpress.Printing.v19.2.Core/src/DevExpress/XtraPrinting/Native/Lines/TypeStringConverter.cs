namespace DevExpress.XtraPrinting.Native.Lines
{
    using System;
    using System.ComponentModel;

    public class TypeStringConverter : IStringConverter
    {
        private readonly TypeConverter converter;
        private readonly ITypeDescriptorContext context;

        public TypeStringConverter(TypeConverter converter, ITypeDescriptorContext context);
        bool IStringConverter.CanConvertFromString();
        object IStringConverter.ConvertFromString(string text);
        string IStringConverter.ConvertToString(object val);
    }
}

