namespace DevExpress.XtraPrinting.Native.DrillDown
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;

    internal class DrillDownKeyConverter : ICustomObjectConverter
    {
        bool ICustomObjectConverter.CanConvert(Type type);
        object ICustomObjectConverter.FromString(Type type, string str);
        Type ICustomObjectConverter.GetType(string typeName);
        string ICustomObjectConverter.ToString(Type type, object obj);
        private static bool IsValidType(Type type);
    }
}

