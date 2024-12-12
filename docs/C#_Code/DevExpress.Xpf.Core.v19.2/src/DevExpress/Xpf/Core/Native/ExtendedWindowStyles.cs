namespace DevExpress.Xpf.Core.Native
{
    using System;

    [Flags]
    internal enum ExtendedWindowStyles : uint
    {
        public const ExtendedWindowStyles None = ExtendedWindowStyles.LEFT;,
        public const ExtendedWindowStyles DLGMODALFRAME = ExtendedWindowStyles.DLGMODALFRAME;,
        public const ExtendedWindowStyles NOPARENTNOTIFY = (ExtendedWindowStyles.LEFT | ExtendedWindowStyles.NOPARENTNOTIFY);,
        public const ExtendedWindowStyles TOPMOST = (ExtendedWindowStyles.LEFT | ExtendedWindowStyles.TOPMOST);,
        public const ExtendedWindowStyles ACCEPTFILES = ExtendedWindowStyles.ACCEPTFILES;,
        public const ExtendedWindowStyles TRANSPARENT = (ExtendedWindowStyles.LEFT | ExtendedWindowStyles.TRANSPARENT);,
        public const ExtendedWindowStyles MDICHILD = (ExtendedWindowStyles.LEFT | ExtendedWindowStyles.MDICHILD);,
        public const ExtendedWindowStyles TOOLWINDOW = (ExtendedWindowStyles.LEFT | ExtendedWindowStyles.TOOLWINDOW);,
        public const ExtendedWindowStyles WINDOWEDGE = (ExtendedWindowStyles.LEFT | ExtendedWindowStyles.WINDOWEDGE);,
        public const ExtendedWindowStyles CLIENTEDGE = ExtendedWindowStyles.CLIENTEDGE;,
        public const ExtendedWindowStyles CONTEXTHELP = ExtendedWindowStyles.CONTEXTHELP;,
        public const ExtendedWindowStyles RIGHT = (ExtendedWindowStyles.LEFT | ExtendedWindowStyles.RIGHT);,
        public const ExtendedWindowStyles LEFT = ExtendedWindowStyles.LEFT;,
        public const ExtendedWindowStyles RTLREADING = (ExtendedWindowStyles.LEFT | ExtendedWindowStyles.RTLREADING);,
        public const ExtendedWindowStyles LTRREADING = ExtendedWindowStyles.LEFT;,
        public const ExtendedWindowStyles LEFTSCROLLBAR = (ExtendedWindowStyles.LEFT | ExtendedWindowStyles.LEFTSCROLLBAR);,
        public const ExtendedWindowStyles RIGHTSCROLLBAR = ExtendedWindowStyles.LEFT;,
        public const ExtendedWindowStyles CONTROLPARENT = ExtendedWindowStyles.CONTROLPARENT;,
        public const ExtendedWindowStyles STATICEDGE = (ExtendedWindowStyles.LEFT | ExtendedWindowStyles.STATICEDGE);,
        public const ExtendedWindowStyles APPWINDOW = ExtendedWindowStyles.APPWINDOW;,
        public const ExtendedWindowStyles LAYERED = ExtendedWindowStyles.LAYERED;,
        public const ExtendedWindowStyles NOINHERITLAYOUT = (ExtendedWindowStyles.LEFT | ExtendedWindowStyles.NOINHERITLAYOUT);,
        public const ExtendedWindowStyles LAYOUTRTL = ExtendedWindowStyles.LAYOUTRTL;,
        public const ExtendedWindowStyles COMPOSITED = ExtendedWindowStyles.COMPOSITED;,
        public const ExtendedWindowStyles NOACTIVATE = (ExtendedWindowStyles.LEFT | ExtendedWindowStyles.NOACTIVATE);,
        public const ExtendedWindowStyles OVERLAPPEDWINDOW = (ExtendedWindowStyles.CLIENTEDGE | ExtendedWindowStyles.WINDOWEDGE);,
        public const ExtendedWindowStyles PALETTEWINDOW = (ExtendedWindowStyles.LEFT | ExtendedWindowStyles.PALETTEWINDOW);
    }
}

