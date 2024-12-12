namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class BarManagerFocusHelper
    {
        public BarManagerFocusHelper.IsLostFocusCheckDelegate IsLostFocusCheck;
        protected UIElement lastFocusedElement;
        protected bool isLostFocus;

        public BarManagerFocusHelper(FrameworkElement owner);
        public BarManagerFocusHelper(FrameworkElement owner, BarManagerFocusHelper.IsLostFocusCheckDelegate isLostFocusCheck);
        public virtual void CaptureFocus(bool makeOwnerFocused = true);
        public static bool IsKeyboardFocusedWithin(DependencyObject dObj);
        protected virtual void OnLostFocus(object sender, EventArgs e);
        public virtual void ReleaseFocus();
        public void SubscribeOwner();
        public void UnsubscribeOwner();

        protected FrameworkElement Owner { get; private set; }

        public delegate bool IsLostFocusCheckDelegate();
    }
}

