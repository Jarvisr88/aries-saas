namespace DevExpress.Xpf.Docking.Platform
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public static class KeyHelper
    {
        public static bool IsCtrlPressed =>
            Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);

        public static bool IsShiftPressed =>
            Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);

        public static IInputElement FocusedElement =>
            Keyboard.FocusedElement;
    }
}

