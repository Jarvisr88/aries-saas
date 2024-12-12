namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public static class KeyboardHelper
    {
        public static void Focus(IInputElement element)
        {
            Keyboard.Focus(element);
        }

        public static bool Focus(UIElement element) => 
            element.Focus();

        public static bool IsAltKey(Key key) => 
            KeyMapper.KeyToKeysValue(key) == 0x40000;

        public static bool IsAltModifier(ModifierKeys modifiers) => 
            (modifiers & ModifierKeys.Alt) != ModifierKeys.None;

        public static bool IsControlKey(Key key) => 
            KeyMapper.KeyToKeysValue(key) == 0x20000;

        public static bool IsControlModifier(ModifierKeys modifiers) => 
            (modifiers & ModifierKeys.Control) != ModifierKeys.None;

        public static bool IsShiftKey(Key key) => 
            KeyMapper.KeyToKeysValue(key) == 0x10000;

        public static bool IsShiftModifier(ModifierKeys modifiers) => 
            (modifiers & ModifierKeys.Shift) != ModifierKeys.None;

        public static bool IsAltPressed =>
            IsAltModifier(Keyboard.Modifiers);

        public static bool IsControlPressed =>
            IsControlModifier(Keyboard.Modifiers);

        public static bool IsShiftPressed =>
            IsShiftModifier(Keyboard.Modifiers);

        public static DependencyObject FocusedElement =>
            Keyboard.FocusedElement as DependencyObject;

        public static int KeySystemCodeEnter =>
            13;

        public static int KeySystemCodeEscape =>
            0x1b;

        public static int KeySystemCodeLineFeed =>
            10;
    }
}

