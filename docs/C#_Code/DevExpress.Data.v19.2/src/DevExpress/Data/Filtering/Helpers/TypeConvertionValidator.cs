namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.Runtime.InteropServices;

    public class TypeConvertionValidator
    {
        public static bool CanConvert(object objValue, Type destinationType);
        public static bool CanConvertType(Type sourceType, Type destinationType);
        public static bool TryConvert(object objValue, Type destinationType, out object result);
    }
}

