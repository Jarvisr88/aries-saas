namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows.Input;

    public static class Keyboard2
    {
        public static bool IsAltPressed =>
            (Keyboard.Modifiers & ModifierKeys.Alt) != ModifierKeys.None;

        public static bool IsControlPressed =>
            (Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.None;

        public static bool IsShiftPressed =>
            (Keyboard.Modifiers & ModifierKeys.Shift) != ModifierKeys.None;
    }
}

