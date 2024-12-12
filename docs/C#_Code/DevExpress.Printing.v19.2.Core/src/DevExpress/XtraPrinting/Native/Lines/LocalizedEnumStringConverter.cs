namespace DevExpress.XtraPrinting.Native.Lines
{
    using System;

    public class LocalizedEnumStringConverter : IStringConverter
    {
        private Type enumType;

        public LocalizedEnumStringConverter(Type enumType);
        bool IStringConverter.CanConvertFromString();
        object IStringConverter.ConvertFromString(string text);
        string IStringConverter.ConvertToString(object val);
    }
}

