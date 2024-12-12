namespace DevExpress.Utils.Serializing.Helpers
{
    using System;

    public class SimpleSerializer<T> : IOneTypeObjectConverter
    {
        object IOneTypeObjectConverter.FromString(string str) => 
            ObjectConverter.Instance.IStringToStructure(str, ((IOneTypeObjectConverter) this).Type);

        string IOneTypeObjectConverter.ToString(object obj) => 
            ObjectConverter.Instance.IStructureToString(obj);

        Type IOneTypeObjectConverter.Type =>
            typeof(T);
    }
}

