namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using DevExpress.Data.Utils;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class ModelExtensions
    {
        public static IModelProperty GetProperty(this IModelItem item, string propertyName, bool exceptionIfNotExist = true) => 
            exceptionIfNotExist ? item.Properties[propertyName] : item.Properties.Find(propertyName);

        public static IModelProperty GetProperty(this IModelItem item, DependencyProperty property, bool exceptionIfNotExist = true) => 
            item.GetProperty(property.Name, exceptionIfNotExist);

        public static IModelItem GetRoot(this IEditingContext context) => 
            context.Services.GetService<IModelService>().Root;

        public static void SetValue(this IModelItem item, string propertyName, object value, bool exceptionIfNotExist = true)
        {
            item.GetProperty(propertyName, exceptionIfNotExist).Do<IModelProperty>(x => x.SetValue(value));
        }

        public static void SetValue(this IModelItem item, DependencyProperty property, object value, bool exceptionIfNotExist = true)
        {
            item.GetProperty(property, exceptionIfNotExist).Do<IModelProperty>(x => x.SetValue(value));
        }

        public static void SetValueIfNotSet(this IModelProperty property, object value)
        {
            if (!property.IsSet && !Equals(value, property.ComputedValue))
            {
                property.SetValue(value);
            }
        }

        public static void SetValueIfNotSet(this IModelItem item, string propertyName, object value, bool exceptionIfNotExist = true)
        {
            item.GetProperty(propertyName, exceptionIfNotExist).Do<IModelProperty>(x => x.SetValueIfNotSet(value));
        }

        public static void SetValueIfNotSet(this IModelItem item, DependencyProperty property, object value, bool exceptionIfNotExist = true)
        {
            item.GetProperty(property, exceptionIfNotExist).Do<IModelProperty>(x => x.SetValueIfNotSet(value));
        }
    }
}

