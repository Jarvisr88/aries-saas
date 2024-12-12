namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DXWindowHeaderItemsControl : ItemsControl
    {
        static DXWindowHeaderItemsControl();
        protected override DependencyObject GetContainerForItemOverride();
        protected virtual void OnWindowServiceChanged(object oldValue, object newValue);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXWindowHeaderItemsControl.<>c <>9;

            static <>c();
            internal void <.cctor>b__0_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

