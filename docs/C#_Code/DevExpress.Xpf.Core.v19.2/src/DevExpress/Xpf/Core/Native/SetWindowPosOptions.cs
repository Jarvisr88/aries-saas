namespace DevExpress.Xpf.Core.Native
{
    using System;

    [Flags]
    internal enum SetWindowPosOptions
    {
        public const SetWindowPosOptions ASYNCWINDOWPOS = SetWindowPosOptions.ASYNCWINDOWPOS;,
        public const SetWindowPosOptions DEFERERASE = SetWindowPosOptions.DEFERERASE;,
        public const SetWindowPosOptions DRAWFRAME = SetWindowPosOptions.DRAWFRAME;,
        public const SetWindowPosOptions FRAMECHANGED = SetWindowPosOptions.DRAWFRAME;,
        public const SetWindowPosOptions HIDEWINDOW = SetWindowPosOptions.HIDEWINDOW;,
        public const SetWindowPosOptions NOACTIVATE = SetWindowPosOptions.NOACTIVATE;,
        public const SetWindowPosOptions NOCOPYBITS = SetWindowPosOptions.NOCOPYBITS;,
        public const SetWindowPosOptions NOMOVE = SetWindowPosOptions.NOMOVE;,
        public const SetWindowPosOptions NOOWNERZORDER = SetWindowPosOptions.NOOWNERZORDER;,
        public const SetWindowPosOptions NOREDRAW = SetWindowPosOptions.NOREDRAW;,
        public const SetWindowPosOptions NOREPOSITION = SetWindowPosOptions.NOOWNERZORDER;,
        public const SetWindowPosOptions NOSENDCHANGING = SetWindowPosOptions.NOSENDCHANGING;,
        public const SetWindowPosOptions NOSIZE = SetWindowPosOptions.NOSIZE;,
        public const SetWindowPosOptions NOZORDER = SetWindowPosOptions.NOZORDER;,
        public const SetWindowPosOptions SHOWWINDOW = SetWindowPosOptions.SHOWWINDOW;
    }
}

