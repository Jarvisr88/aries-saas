namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.InteropServices;

    internal static class ValueParser<T>
    {
        private static T GetValue(object value);
        internal static bool TryGetValue(OperandValue value, out T result);
        internal static bool TryGetValue(object value, out T result);
    }
}

