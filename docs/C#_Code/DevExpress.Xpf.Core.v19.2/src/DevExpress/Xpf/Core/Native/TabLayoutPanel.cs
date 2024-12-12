namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class TabLayoutPanel : RotatableLayoutPanel
    {
        public static readonly DependencyProperty ViewInfoProperty;

        static TabLayoutPanel();
        protected override void OnLoaded(object sender, RoutedEventArgs e);
        private void OnViewInfoChanged();

        public TabViewInfo ViewInfo { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Dock Location { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabLayoutPanel.<>c <>9;

            static <>c();
            internal void <.cctor>b__10_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

