namespace DevExpress.Utils
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public static class MouseHelper
    {
        public static bool Capture(UIElement element) => 
            Mouse.Capture(element);

        public static MouseButtonState GetLeftButton() => 
            Mouse.LeftButton;

        public static Point GetPosition(FrameworkElement relativeTo) => 
            Mouse.GetPosition(relativeTo);

        public static MouseButtonState GetRightButton() => 
            Mouse.RightButton;

        public static bool IsMouseButtonPressed(MouseButtonEventArgs e) => 
            e.ButtonState == MouseButtonState.Pressed;

        public static bool IsMouseLeftButtonPressed(MouseEventArgs e) => 
            Mouse.LeftButton == MouseButtonState.Pressed;

        public static bool IsMouseLeftButtonReleased(MouseEventArgs e) => 
            Mouse.LeftButton == MouseButtonState.Released;

        public static bool IsMouseRightButtonPressed(MouseEventArgs e) => 
            Mouse.RightButton == MouseButtonState.Pressed;

        public static void ReleaseCapture(UIElement element)
        {
            if (ReferenceEquals(Mouse.Captured, element))
            {
                Mouse.Capture(null);
            }
        }

        public static void SubscribeLeftButtonDoubleClick(Control element, EventHandler handler)
        {
            element.AddHandler(Control.MouseDoubleClickEvent, delegate (object sender, MouseButtonEventArgs e) {
                if ((e.LeftButton == MouseButtonState.Pressed) && (handler != null))
                {
                    handler(sender, EventArgs.Empty);
                }
            });
        }

        public static IInputElement Captured =>
            Mouse.Captured;
    }
}

