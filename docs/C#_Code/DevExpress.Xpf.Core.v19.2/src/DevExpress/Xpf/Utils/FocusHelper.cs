namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public static class FocusHelper
    {
        public static bool CanBeFocused(UIElement element) => 
            element.Focusable && element.IsEnabled;

        public static IInputElement GetFocusedElement() => 
            Keyboard.FocusedElement;

        public static bool IsFocused(UIElement element) => 
            element.IsFocused;

        public static bool IsKeyboardFocused(UIElement element) => 
            element.IsKeyboardFocused;

        public static bool IsKeyboardFocusWithin(UIElement element) => 
            element.IsKeyboardFocusWithin;

        public static void SetFocusable(UIElement element, bool value)
        {
            element.Focusable = value;
        }
    }
}

