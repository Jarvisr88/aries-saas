namespace DevExpress.Xpf.Printing.Native.Dialogs
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto, Pack=4), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    internal struct COMDLG_FILTERSPEC
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        internal string pszName;
        [MarshalAs(UnmanagedType.LPWStr)]
        internal string pszSpec;
    }
}

