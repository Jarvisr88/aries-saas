namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class DependencyPropertyExtensions
    {
        public static object GetDefaultValue(this DependencyProperty dp) => 
            dp.DefaultMetadata.DefaultValue;

        public static string GetName(this DependencyProperty dp) => 
            dp.Name;

        public static Type GetOwnerType(this DependencyProperty dp) => 
            dp.OwnerType;
    }
}

