namespace Dapper
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal static class TypeExtensions
    {
        public static MethodInfo GetPublicInstanceMethod(this Type type, string name, Type[] types) => 
            type.GetMethod(name, BindingFlags.Public | BindingFlags.Instance, null, types, null);
    }
}

