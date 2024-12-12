namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Windows.Input;

    public static class CapsLockHelper
    {
        public static bool IsCapsLockToggled =>
            Keyboard.PrimaryDevice.IsKeyToggled(Key.Capital);
    }
}

