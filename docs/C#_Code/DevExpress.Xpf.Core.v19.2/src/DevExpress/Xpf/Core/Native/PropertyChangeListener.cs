namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class PropertyChangeListener : DependencyObject
    {
        public static readonly DependencyProperty FakeProperty;
        private readonly Action<object> changedCallback;

        static PropertyChangeListener();
        private PropertyChangeListener(BindingBase binding, Action<object> changedCallback);
        public static PropertyChangeListener Create(BindingBase binding, Action<object> changedCallback);
        private void OnFakeChanged();

        public object Fake { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PropertyChangeListener.<>c <>9;

            static <>c();
            internal void <.cctor>b__8_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

