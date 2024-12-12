namespace DevExpress.Utils.Serializing.Helpers
{
    using System;

    public interface IOneTypeObjectConverter
    {
        object FromString(string str);
        string ToString(object obj);

        System.Type Type { get; }
    }
}

