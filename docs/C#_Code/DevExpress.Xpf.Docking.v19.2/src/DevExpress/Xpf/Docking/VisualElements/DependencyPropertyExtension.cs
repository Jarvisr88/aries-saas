namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class DependencyPropertyExtension
    {
        public static object GetDefaultValue(this DependencyProperty property, Type ownerType = null) => 
            property.DefaultMetadata.DefaultValue;
    }
}

