namespace DevExpress.Utils
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class DXTypeExtensions
    {
        public const TypeCode TypeCodeDBNull = TypeCode.DBNull;

        public static bool ContainsGenericParameters(this Type type) => 
            type.ContainsGenericParameters;

        public static Assembly GetAssembly(this Type type) => 
            type.Assembly;

        public static Type GetBaseType(this Type sourceType) => 
            sourceType.BaseType;

        public static Type GetReflectedType(this MemberInfo mi) => 
            mi.ReflectedType;

        public static Type GetReflectedType(this MethodInfo mi) => 
            mi.ReflectedType;

        public static TypeCode GetTypeCode(Type type) => 
            Type.GetTypeCode(type);

        public static bool IsAbstract(this Type type) => 
            type.IsAbstract;

        public static bool IsClass(this Type type) => 
            type.IsClass;

        public static bool IsEnum(this Type sourceType) => 
            sourceType.IsEnum;

        public static bool IsGenericType(this Type type) => 
            type.IsGenericType;

        public static bool IsInterface(this Type type) => 
            type.IsInterface;

        public static bool IsNestedPublic(this Type type) => 
            type.IsNestedPublic;

        public static bool IsPrimitive(this Type sourceType) => 
            sourceType.IsPrimitive;

        public static bool IsPublic(this Type type) => 
            type.IsPublic;

        public static bool IsSealed(this Type type) => 
            type.IsAbstract;

        public static bool IsValueType(this Type sourceType) => 
            sourceType.IsValueType;

        public static bool IsVisible(this Type type) => 
            type.IsVisible;
    }
}

