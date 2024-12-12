namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class NamePropertyChangeListener : DependencyObject
    {
        private static bool IsInDesignMode;
        public static readonly DependencyProperty NameProperty;
        private readonly Action nameChangedCallback;

        static NamePropertyChangeListener();
        private NamePropertyChangeListener(DependencyObject source, Action nameChangedCallback, bool designTimeOnly);
        public static NamePropertyChangeListener Create(DependencyObject source, Action nameChangedCallback);
        public static NamePropertyChangeListener CreateDesignTimeOnly(DependencyObject source, Action nameChangedCallback);
        private void OnNameChanged();

        public string Name { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NamePropertyChangeListener.<>c <>9;

            static <>c();
            internal void <.cctor>b__10_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

