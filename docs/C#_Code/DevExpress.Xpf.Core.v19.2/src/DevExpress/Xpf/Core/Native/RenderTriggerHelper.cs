namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public static class RenderTriggerHelper
    {
        [ThreadStatic]
        private static ReflectionHelper helper;
        private static readonly Type DefaultValueConverter;
        private static readonly Type SystemConvertConverter;

        static RenderTriggerHelper();
        private static bool CanConvertUsingSystemConverter(Type sourceType, Type targetType);
        private static object Convert(Type sourceType, Type targetType, object value);
        public static object GetConvertedValue(Type targetType, object value);
        public static object GetConvertedValue(object entity, string property, object value);
        private static TypeConverter GetConverter(Type targetType);
        public static Type GetPropertyType(object entity, string property, BindingFlags bindingFlags = 20);
        public static Type GetPropertyType(Type entityType, string property, BindingFlags bindingFlags = 20);
        public static object GetValue(object entity, string property, BindingFlags bindingFlags = 20);
        public static void SetConvertedValue(object entity, string property, object value);
        public static void SetValue(object entity, string property, object value, BindingFlags bindingFlags = 20);
        private static object ThrowNoProperty(object entity, string property, Exception ex);

        public static ReflectionHelper Helper { get; }
    }
}

