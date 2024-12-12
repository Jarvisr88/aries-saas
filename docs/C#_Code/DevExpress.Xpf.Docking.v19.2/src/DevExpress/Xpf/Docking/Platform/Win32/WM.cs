namespace DevExpress.Xpf.Docking.Platform.Win32
{
    using System;

    internal enum WM
    {
        WM_MOVE = 3,
        WM_SIZE = 5,
        WM_ACTIVATE = 6,
        WM_SETFOCUS = 7,
        WM_KILLFOCUS = 8,
        WM_SHOWWINDOW = 0x18,
        WM_GETMINMAXINFO = 0x24,
        WM_WINDOWPOSCHANGING = 70,
        WM_WINDOWPOSCHANGED = 0x47,
        WM_NCCALCSIZE = 0x83,
        WM_NCHITTEST = 0x84,
        WM_NCACTIVATE = 0x86,
        WM_NCMOUSEMOVE = 160,
        WM_KEYDOWN = 0x100,
        WM_KEYUP = 0x101,
        WM_SYSCOMMAND = 0x112,
        WM_INITMENUPOPUP = 0x117,
        WM_LBUTTONUP = 0x202,
        WM_SIZING = 0x214,
        WM_CAPTURECHANGED = 0x215,
        WM_MOVING = 0x216,
        WM_ENTERSIZEMOVE = 0x231,
        WM_EXITSIZEMOVE = 0x232,
        WM_DWMCOMPOSITIONCHANGED = 0x31e
    }
}

