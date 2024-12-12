namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Reflection;

    public static class ReflectorHelper
    {
        public static MethodInfo GetMethod(Type type, string methodName) => 
            type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);

        public static PropertyInfo GetProperty(Type type, string propertyName) => 
            GetProperty(type, propertyName, BindingFlags.Public | BindingFlags.Instance);

        public static PropertyInfo GetProperty(Type type, string propertyName, BindingFlags flags) => 
            type.GetProperty(propertyName, flags);

        public static MethodInfo GetStaticMethod(Type type, string methodName) => 
            type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);

        public static PropertyInfo GetStaticProperty(Type type, string propertyName) => 
            type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Static);

        public static object GetValue(object obj, string propertyName) => 
            GetProperty(obj.GetType(), propertyName).GetValue(obj, null);

        public static object GetValue(object obj, string propertyName, BindingFlags flags) => 
            GetProperty(obj.GetType(), propertyName, flags).GetValue(obj, null);

        public static void SetValue(object obj, string propertyName, object value)
        {
            GetProperty(obj.GetType(), propertyName).SetValue(obj, value, null);
        }
    }
}

