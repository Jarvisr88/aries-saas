namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.InteropServices;

    internal static class TypeHelper
    {
        private static readonly Assembly DataAssembly;

        static TypeHelper();
        internal static bool AllowNull(Type type);
        internal static Type EnsureDynamicDescendantsAccessibility(Type type);
        internal static Type GetConversionType(Type type);
        internal static Type GetNullable(Type type, out Type underlyingType);
        internal static Type GetTypeOrNullable(Type type, bool? allowNull);
        internal static bool IsAccessibleForDynamicDescendants(Type type);
        private static bool IsComparableObject(Type type);
        internal static bool IsExpandableProperty(PropertyDescriptor pd);
        internal static bool IsExpandableType(Type type);
        internal static bool IsNullable(Type type);
        internal static bool IsNullable(Type type, out Type underlyingType);
        private static bool IsSimpleRefType(Type type);
        private static bool IsSimpleValueType(Type type);
        private static bool IsSpecialValueType(Type type);
    }
}

