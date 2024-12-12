namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class TabOrientablePanel : OrientableLayoutPanel
    {
        public static readonly DependencyProperty ChildMarginProperty;
        public static readonly DependencyProperty IndentProperty;
        public static readonly DependencyProperty ViewInfoProperty;

        static TabOrientablePanel();
        public static Thickness GetChildMargin(DependencyObject obj);
        private static void OnChildMarginChanged(DependencyObject obj, Thickness value);
        private void OnIndentChanged();
        protected override void OnLoaded(object sender, RoutedEventArgs e);
        protected virtual void OnViewInfoChanged();
        public static void SetChildMargin(DependencyObject obj, Thickness value);

        public Thickness Indent { get; set; }

        public TabViewInfo ViewInfo { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public System.Windows.Controls.Orientation Orientation { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabOrientablePanel.<>c <>9;
            public static Func<TabViewInfo, Orientation> <>9__15_0;
            public static Func<Orientation> <>9__15_1;
            public static Action<FrameworkElement> <>9__15_2;
            public static Func<TabViewInfo, bool> <>9__17_0;

            static <>c();
            internal void <.cctor>b__19_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__19_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__19_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal bool <OnIndentChanged>b__17_0(TabViewInfo x);
            internal Orientation <OnViewInfoChanged>b__15_0(TabViewInfo x);
            internal Orientation <OnViewInfoChanged>b__15_1();
            internal void <OnViewInfoChanged>b__15_2(FrameworkElement x);
        }
    }
}

