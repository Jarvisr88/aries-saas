namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class SelectionIndicator : Control
    {
        public static readonly DependencyProperty IsHighlightedProperty;
        public static readonly DependencyProperty IsSelectedProperty;
        public static readonly DependencyProperty OrientationProperty;
        public static readonly DependencyProperty IsPressedProperty;
        private ScaleTransform scaleTransform;

        static SelectionIndicator();
        public SelectionIndicator();
        protected virtual void OnDecoratorChanged(SelectionIndicatorDecorator oldValue, SelectionIndicatorDecorator newValue);
        protected virtual void OnIsHighlightedChanged(bool oldValue, bool newValue);
        protected internal virtual void OnIsPressedChanged(bool oldValue, bool newValue);
        protected virtual void OnIsSelectedChanged(bool oldValue, bool newValue);
        protected internal virtual void OnOrientationChanged(System.Windows.Controls.Orientation? oldValue, System.Windows.Controls.Orientation? newValue);
        private void UpdateAnimation(bool extend);

        public System.Windows.Controls.Orientation? Orientation { get; set; }

        public bool IsSelected { get; set; }

        public bool IsPressed { get; set; }

        public bool IsHighlighted { get; set; }

        protected SelectionIndicatorDecorator Decorator { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SelectionIndicator.<>c <>9;

            static <>c();
            internal void <.cctor>b__4_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__4_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__4_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__4_3(DependencyObject d, DependencyPropertyChangedEventArgs v);
            internal void <.cctor>b__4_4(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

