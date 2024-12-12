namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class PropertyChangeListener<T> : DependencyObject
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty FakeProperty;
        private readonly Action<T> changedCallback;

        static PropertyChangeListener();
        private PropertyChangeListener(Binding binding, Action<T> changedCallback);
        public static PropertyChangeListener<T> Create(Binding binding, Action<T> changedCallback);
        private void OnFakeChanged();

        public int ChangeCount { get; private set; }

        public T Fake { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PropertyChangeListener<T>.<>c <>9;

            static <>c();
            internal void <.cctor>b__12_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

