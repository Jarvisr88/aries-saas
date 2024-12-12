namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    public static class RightToLeftHelper
    {
        private static Key GetLeftKey(bool isRightToLeft) => 
            isRightToLeft ? Key.Right : Key.Left;

        public static bool IsLeftKey(Key key, bool isRightToLeft) => 
            key == GetLeftKey(isRightToLeft);

        public static bool IsRightKey(Key key, bool isRightToLeft) => 
            key == GetLeftKey(!isRightToLeft);

        public static Key TransposeKey(Key key, bool isRightToLeft = true) => 
            isRightToLeft ? ((key != Key.Left) ? ((key != Key.Right) ? key : Key.Left) : Key.Right) : key;

        public static Key TransposeKey(Key key, FrameworkElement control) => 
            (control != null) ? TransposeKey(key, control.FlowDirection == FlowDirection.RightToLeft) : key;
    }
}

