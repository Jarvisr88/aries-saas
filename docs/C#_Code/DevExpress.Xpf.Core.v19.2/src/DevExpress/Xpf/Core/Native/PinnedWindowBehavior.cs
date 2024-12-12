namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PinnedWindowBehavior : Behavior<Window>
    {
        public static readonly DependencyProperty PinnedProperty;
        public static readonly DependencyProperty PaddingProperty;
        public static readonly DependencyProperty BoundObjectProperty;
        private Window boundWindow;
        private readonly Locker pinLocker;

        static PinnedWindowBehavior();
        public PinnedWindowBehavior();
        private void AssociatedObjectOnClosing(object sender, CancelEventArgs cancelEventArgs);
        private void AssociatedObjectOnSourceInitialized(object sender, EventArgs e);
        private void OnAssociatedObjectLocationChanged(object sender, EventArgs e);
        private void OnAssociatedObjectSizeChanged(object sender, SizeChangedEventArgs e);
        protected override void OnAttached();
        private void OnBoundElementSizeChanged(object sender, SizeChangedEventArgs e);
        protected virtual void OnBoundObjectChanged(FrameworkElement oldValue, FrameworkElement newValue);
        protected virtual void OnBoundWindowChanged(Window oldValue, Window newValue);
        private void OnBoundWindowLocationChanged(object sender, EventArgs e);
        protected override void OnDetaching();
        protected internal virtual void OnIsPopupContentInvisibleChanged(bool oldValue, bool newValue);
        private void OnPaddingChanged(Thickness oldValue, Thickness newValue);
        private void OnPinnedChanged(bool oldValue, bool newValue);
        private void RecalcAssociatedObjectSize();
        private void SubscribeEvent(FrameworkElement newValue);
        private void UnsubscribeEvent(FrameworkElement oldValue);
        private void UpdateAssociatedObjectProperties();

        public Thickness Padding { get; set; }

        public bool Pinned { get; set; }

        public FrameworkElement BoundObject { get; set; }

        private bool IsActuallyAttached { get; }

        public Window BoundWindow { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PinnedWindowBehavior.<>c <>9;
            public static Func<FrameworkElement, PresentationSource> <>9__26_0;

            static <>c();
            internal void <.cctor>b__37_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__37_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__37_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal PresentationSource <UpdateAssociatedObjectProperties>b__26_0(FrameworkElement x);
        }
    }
}

