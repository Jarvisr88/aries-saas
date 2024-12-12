namespace DevExpress.Utils
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows.Forms;

    public static class DragAndDropCursors
    {
        private const uint LOAD_LIBRARY_AS_DATAFILE = 2;
        private const uint noneEffectCursorIndex = 1;
        private const uint moveEffectCursorIndex = 2;
        private const uint copyEffectCursorIndex = 3;
        private const uint linkEffectCursorIndex = 4;
        public static readonly Cursor NoneEffectCursor;
        public static readonly Cursor MoveEffectCursor;
        public static readonly Cursor CopyEffectCursor;
        public static readonly Cursor LinkEffectCursor;

        [SecuritySafeCritical]
        static DragAndDropCursors()
        {
            IntPtr zero = IntPtr.Zero;
            try
            {
                zero = LoadLibraryEx("ole32.dll", IntPtr.Zero, 2);
                if (zero == IntPtr.Zero)
                {
                    NoneEffectCursor = Cursors.No;
                    MoveEffectCursor = Cursors.Default;
                    CopyEffectCursor = Cursors.Default;
                    LinkEffectCursor = Cursors.Default;
                }
                else
                {
                    NoneEffectCursor = LoadCursor(zero, 1, Cursors.No);
                    MoveEffectCursor = LoadCursor(zero, 2, Cursors.Default);
                    CopyEffectCursor = LoadCursor(zero, 3, Cursors.Default);
                    LinkEffectCursor = LoadCursor(zero, 4, Cursors.Default);
                }
            }
            catch (Exception)
            {
                NoneEffectCursor = Cursors.No;
                MoveEffectCursor = Cursors.Default;
                CopyEffectCursor = Cursors.Default;
                LinkEffectCursor = Cursors.Default;
            }
            finally
            {
                if (zero != IntPtr.Zero)
                {
                    FreeLibrary(zero);
                }
            }
        }

        [DllImport("kernel32.dll", CharSet=CharSet.Unicode)]
        private static extern IntPtr FreeLibrary(IntPtr hModule);
        public static Cursor GetCursor(DragDropEffects effect)
        {
            switch (effect)
            {
                case DragDropEffects.None:
                    return NoneEffectCursor;

                case DragDropEffects.Copy:
                    return CopyEffectCursor;

                case DragDropEffects.Move:
                    return MoveEffectCursor;

                case DragDropEffects.Link:
                    return LinkEffectCursor;
            }
            return Cursors.Default;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr LoadCursor(IntPtr hModule, uint cursorIndex);
        [SecuritySafeCritical]
        private static Cursor LoadCursor(IntPtr hModule, uint cursorIndex, Cursor defaultCursor)
        {
            IntPtr handle = LoadCursor(hModule, cursorIndex);
            return ((handle != IntPtr.Zero) ? new Cursor(handle) : defaultCursor);
        }

        [DllImport("kernel32.dll", CharSet=CharSet.Unicode)]
        private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, uint dwFlags);
    }
}

