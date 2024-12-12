namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core.Platform;
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Input;

    public static class EventArgsHelper
    {
        [DebuggerStepThrough]
        public static DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs Convert(UIElement element, MouseButtonEventArgs e)
        {
            MouseButtons mouseButtons = GetMouseButtons(e);
            DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs args1 = new DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs(e.GetPosition(element), mouseButtons, GetChangedButtons(e));
            args1.OriginalEvent = e;
            return args1;
        }

        [DebuggerStepThrough]
        public static DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs Convert(UIElement element, System.Windows.Input.MouseEventArgs e)
        {
            MouseButtons mouseButtons = GetMouseButtons(e);
            DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs args1 = new DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs(e.GetPosition(element), mouseButtons);
            args1.OriginalEvent = e;
            return args1;
        }

        [DebuggerStepThrough]
        public static MouseButtons GetChangedButtons(MouseButtonEventArgs e)
        {
            MouseButtons none = MouseButtons.None;
            if (e.ChangedButton == MouseButton.Left)
            {
                none |= MouseButtons.Left;
            }
            if (e.ChangedButton == MouseButton.Middle)
            {
                none |= MouseButtons.Middle;
            }
            if (e.ChangedButton == MouseButton.Right)
            {
                none |= MouseButtons.None | MouseButtons.Right;
            }
            return none;
        }

        [DebuggerStepThrough]
        private static MouseButtons GetMouseButtons(System.Windows.Input.MouseEventArgs e)
        {
            MouseButtons none = MouseButtons.None;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                none |= MouseButtons.Left;
            }
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                none |= MouseButtons.Middle;
            }
            if (e.RightButton == MouseButtonState.Pressed)
            {
                none |= MouseButtons.None | MouseButtons.Right;
            }
            if (e.XButton1 == MouseButtonState.Pressed)
            {
                none |= MouseButtons.None | MouseButtons.XButton1;
            }
            if (e.XButton2 == MouseButtonState.Pressed)
            {
                none |= MouseButtons.None | MouseButtons.XButton2;
            }
            return none;
        }
    }
}

