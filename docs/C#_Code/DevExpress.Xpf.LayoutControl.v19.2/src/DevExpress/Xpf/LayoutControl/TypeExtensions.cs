namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class TypeExtensions
    {
        internal static Type GetNonNullableType(this Type type) => 
            type.IsNullable() ? type.GetGenericArguments()[0] : type;

        internal static bool IsEditable(this Type type) => 
            type.IsSimple();

        internal static bool IsNullable(this Type type) => 
            type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>));

        internal static bool IsSimple(this Type type)
        {
            type = type.GetNonNullableType();
            return (type.IsPrimitive || (type.IsEnum || ((type == typeof(string)) || ((type == typeof(DateTime)) || ((type == typeof(decimal)) || (type == typeof(TimeSpan)))))));
        }
    }
}

