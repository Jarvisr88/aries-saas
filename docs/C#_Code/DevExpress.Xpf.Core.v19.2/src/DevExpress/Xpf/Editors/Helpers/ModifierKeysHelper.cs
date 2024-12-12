namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Windows.Input;

    public static class ModifierKeysHelper
    {
        public static bool ContainsModifiers(ModifierKeys modifiers) => 
            IsAltPressed(modifiers) || (IsShiftPressed(modifiers) || IsCtrlPressed(modifiers));

        public static ModifierKeys GetKeyboardModifiers() => 
            Keyboard.Modifiers;

        public static ModifierKeys GetKeyboardModifiers(KeyEventArgs e) => 
            e.KeyboardDevice.Modifiers;

        public static bool IsAltPressed(ModifierKeys modifiers) => 
            (modifiers & ModifierKeys.Alt) != ModifierKeys.None;

        public static bool IsCtrlPressed(ModifierKeys modifiers) => 
            (modifiers & ModifierKeys.Control) != ModifierKeys.None;

        public static bool IsOnlyCtrlPressed(ModifierKeys modifiers) => 
            modifiers == ModifierKeys.Control;

        public static bool IsShiftPressed(ModifierKeys modifiers) => 
            (modifiers & ModifierKeys.Shift) != ModifierKeys.None;

        public static bool IsWinPressed(ModifierKeys modifiers) => 
            (modifiers & ModifierKeys.Windows) != ModifierKeys.None;

        public static bool NoModifiers(ModifierKeys modifiers) => 
            modifiers == ModifierKeys.None;
    }
}

