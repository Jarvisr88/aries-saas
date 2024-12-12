namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using System;
    using System.Windows.Input;

    internal static class ChangeCursorHelper
    {
        public static void ResetCursorToDefault()
        {
            SetCursor(null);
        }

        private static void SetCursor(Cursor cursor)
        {
            if (!ReferenceEquals(Mouse.OverrideCursor, cursor))
            {
                Mouse.OverrideCursor = cursor;
            }
        }

        public static void SetHandCursor()
        {
            SetCursor(Cursors.Hand);
        }

        public static void SetResizeCursor()
        {
            SetCursor(Cursors.SizeWE);
        }
    }
}

