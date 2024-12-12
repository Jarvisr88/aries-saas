namespace DevExpress.Mvvm.Native
{
    using DevExpress.Mvvm.DataAnnotations;
    using System;
    using System.Runtime.CompilerServices;

    public static class NativeMetadataExtensions
    {
        public static ContextMenuLayoutBuilder<T> ContextMenuLayout<T>(this MetadataBuilder<T> builder) => 
            new ContextMenuLayoutBuilder<T>(builder);
    }
}

