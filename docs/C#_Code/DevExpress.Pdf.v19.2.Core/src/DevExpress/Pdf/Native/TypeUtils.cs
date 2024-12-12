namespace DevExpress.Pdf.Native
{
    using System;

    internal static class TypeUtils
    {
        public static bool IsAssignableFrom(Type sourceType, Type objType) => 
            sourceType.IsAssignableFrom(objType);
    }
}

