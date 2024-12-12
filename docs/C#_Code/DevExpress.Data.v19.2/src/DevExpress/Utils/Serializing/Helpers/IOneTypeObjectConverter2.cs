namespace DevExpress.Utils.Serializing.Helpers
{
    using System;

    public interface IOneTypeObjectConverter2 : IOneTypeObjectConverter
    {
        bool CanConvertFromString(string str);
    }
}

