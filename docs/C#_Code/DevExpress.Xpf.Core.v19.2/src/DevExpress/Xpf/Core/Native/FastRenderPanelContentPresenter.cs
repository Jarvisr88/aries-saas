namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class FastRenderPanelContentPresenter : ContentHostContentPresenter
    {
        private WeakReference focusedElement;

        static FastRenderPanelContentPresenter();
        public FastRenderPanelContentPresenter();
        private void OnPreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e);
        private void OnVisibilityChanged(Visibility oldValue, Visibility newValue);
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved);

        internal FastRenderPanel Owner { get; set; }

        private DXTabControl TabControl { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FastRenderPanelContentPresenter.<>c <>9;

            static <>c();
            internal void <.cctor>b__0_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

