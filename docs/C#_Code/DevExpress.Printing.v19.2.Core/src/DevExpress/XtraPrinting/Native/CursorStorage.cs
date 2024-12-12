namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class CursorStorage
    {
        private static Stack<Cursor> cursors;
        private static bool? isSafeSubWindowsGranted;

        static CursorStorage();
        public static void Clear();
        public static void RestoreCursor();
        private static void SetCurrentCursor(Cursor cursor);
        public static void SetCursor(Cursor cursor);

        private static object SyncRoot { get; }

        public static bool IsSafeSubWindowsGranted { get; }
    }
}

