namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.InteropServices;

    internal static class ValueParser
    {
        private static object GetDefaultValue(Type valueType);
        private static object GetValue(object value, Type valueType);
        internal static bool TryGetValue(OperandValue value, Type valueType, out object result);
        internal static bool TryGetValue(object value, Type valueType, out object result);
        internal static bool TryParse(object value, Type type, out object result);
    }
}

