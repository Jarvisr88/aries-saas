namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Reflection;
    using System.Windows;

    public static class ObjectPropertyHelper
    {
        public static DependencyProperty GetDependencyProperty(object obj, string propName)
        {
            if (string.IsNullOrWhiteSpace(propName) || (obj == null))
            {
                return null;
            }
            FieldInfo field ??= obj.GetType().GetField(propName + "Property", BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static);
            return ((field == null) ? null : ((DependencyProperty) field.GetValue(obj)));
        }

        public static PropertyInfo GetPropertyInfo(object obj, string propName, BindingFlags flags) => 
            (string.IsNullOrWhiteSpace(propName) || (obj == null)) ? null : obj.GetType().GetProperty(propName, flags);

        public static PropertyInfo GetPropertyInfoGetter(object obj, string propName) => 
            GetPropertyInfo(obj, propName, BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance);

        public static PropertyInfo GetPropertyInfoSetter(object obj, string propName) => 
            GetPropertyInfo(obj, propName, BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance);
    }
}

