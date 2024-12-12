namespace DevExpress.Utils.Serializing.Helpers
{
    using System;

    public interface ICustomObjectConverter
    {
        bool CanConvert(Type type);
        object FromString(Type type, string str);
        Type GetType(string typeName);
        string ToString(Type type, object obj);
    }
}

