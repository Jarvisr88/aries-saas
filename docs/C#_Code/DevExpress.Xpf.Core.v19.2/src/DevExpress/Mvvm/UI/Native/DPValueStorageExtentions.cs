namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class DPValueStorageExtentions
    {
        public static T GetAttachedValue<T>(this DPValueStorage<T> storage, DependencyObject o, DependencyProperty p) => 
            (storage == null) ? ((T) o.GetValue(p)) : storage.GetValue(o, p);
    }
}

