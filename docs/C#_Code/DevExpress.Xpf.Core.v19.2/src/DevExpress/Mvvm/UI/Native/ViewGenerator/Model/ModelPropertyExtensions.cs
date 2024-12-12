namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using System;
    using System.Runtime.CompilerServices;

    public static class ModelPropertyExtensions
    {
        public static IModelProperty FindProperty(this IModelPropertyCollection properties, string propertyName, Type propertyOwnerType) => 
            (propertyOwnerType != null) ? properties.Find(propertyOwnerType, propertyName) : properties[propertyName];
    }
}

