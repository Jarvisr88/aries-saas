namespace DevExpress.Xpf.Core.Native
{
    using System;

    [Flags]
    internal enum WindowStyles : uint
    {
        public const WindowStyles OVERLAPPED = WindowStyles.OVERLAPPED;,
        public const WindowStyles POPUP = (WindowStyles.OVERLAPPED | WindowStyles.POPUP);,
        public const WindowStyles CHILD = WindowStyles.CHILD;,
        public const WindowStyles MINIMIZE = WindowStyles.ICONIC;,
        public const WindowStyles VISIBLE = (WindowStyles.OVERLAPPED | WindowStyles.VISIBLE);,
        public const WindowStyles DISABLED = WindowStyles.DISABLED;,
        public const WindowStyles CLIPSIBLINGS = WindowStyles.CLIPSIBLINGS;,
        public const WindowStyles CLIPCHILDREN = WindowStyles.CLIPCHILDREN;,
        public const WindowStyles MAXIMIZE = WindowStyles.MAXIMIZE;,
        public const WindowStyles BORDER = WindowStyles.BORDER;,
        public const WindowStyles DLGFRAME = WindowStyles.DLGFRAME;,
        public const WindowStyles VSCROLL = (WindowStyles.OVERLAPPED | WindowStyles.VSCROLL);,
        public const WindowStyles HSCROLL = WindowStyles.HSCROLL;,
        public const WindowStyles SYSMENU = (WindowStyles.OVERLAPPED | WindowStyles.SYSMENU);,
        public const WindowStyles THICKFRAME = (WindowStyles.OVERLAPPED | WindowStyles.SIZEBOX);,
        public const WindowStyles GROUP = WindowStyles.GROUP;,
        public const WindowStyles TABSTOP = WindowStyles.MAXIMIZEBOX;,
        public const WindowStyles MINIMIZEBOX = WindowStyles.GROUP;,
        public const WindowStyles MAXIMIZEBOX = WindowStyles.MAXIMIZEBOX;,
        public const WindowStyles CAPTION = (WindowStyles.BORDER | WindowStyles.DLGFRAME);,
        public const WindowStyles TILED = WindowStyles.OVERLAPPED;,
        public const WindowStyles ICONIC = WindowStyles.ICONIC;,
        public const WindowStyles SIZEBOX = (WindowStyles.OVERLAPPED | WindowStyles.SIZEBOX);,
        public const WindowStyles TILEDWINDOW = (WindowStyles.BORDER | WindowStyles.DLGFRAME | WindowStyles.GROUP | WindowStyles.MAXIMIZEBOX | WindowStyles.SIZEBOX | WindowStyles.SYSMENU);,
        public const WindowStyles OVERLAPPEDWINDOW = (WindowStyles.BORDER | WindowStyles.DLGFRAME | WindowStyles.GROUP | WindowStyles.MAXIMIZEBOX | WindowStyles.SIZEBOX | WindowStyles.SYSMENU);,
        public const WindowStyles POPUPWINDOW = (WindowStyles.BORDER | WindowStyles.POPUP | WindowStyles.SYSMENU);,
        public const WindowStyles CHILDWINDOW = WindowStyles.CHILD;
    }
}

