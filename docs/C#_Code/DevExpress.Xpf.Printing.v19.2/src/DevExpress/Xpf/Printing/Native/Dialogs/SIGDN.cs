﻿namespace DevExpress.Xpf.Printing.Native.Dialogs
{
    using System;
    using System.ComponentModel;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    internal enum SIGDN : uint
    {
        NORMALDISPLAY = 0,
        PARENTRELATIVEPARSING = 0x80018001,
        DESKTOPABSOLUTEPARSING = 0x80028000,
        PARENTRELATIVEEDITING = 0x80031001,
        DESKTOPABSOLUTEEDITING = 0x8004c000,
        FILESYSPATH = 0x80058000,
        URL = 0x80068000,
        PARENTRELATIVEFORADDRESSBAR = 0x8007c001,
        PARENTRELATIVE = 0x80080001,
        PARENTRELATIVEFORUI = 0x80094001
    }
}

