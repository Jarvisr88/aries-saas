namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.FilteringUI;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal static class ColumnChooserHelper
    {
        public static UnsubscribeAction AddPropertyChanged(this DependencyObject source, EventHandler action, params DependencyProperty[] properties)
        {
            UnsubscribeAction result = null;
            properties.ForEach<DependencyProperty>(delegate (DependencyProperty property) {
                PropertyChangeTracker tracker = new PropertyChangeTracker(source, property);
                tracker.Changed += action;
                result += delegate {
                    tracker.Changed -= action;
                };
            });
            return result;
        }
    }
}

