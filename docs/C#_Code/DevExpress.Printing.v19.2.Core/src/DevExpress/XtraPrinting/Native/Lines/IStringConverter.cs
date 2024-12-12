namespace DevExpress.XtraPrinting.Native.Lines
{
    using System;

    public interface IStringConverter
    {
        bool CanConvertFromString();
        object ConvertFromString(string str);
        string ConvertToString(object val);
    }
}

