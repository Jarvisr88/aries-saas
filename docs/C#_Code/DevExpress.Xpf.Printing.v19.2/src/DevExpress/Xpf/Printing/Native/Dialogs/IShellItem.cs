namespace DevExpress.Xpf.Printing.Native.Dialogs
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    [ComImport, Browsable(false), EditorBrowsable(EditorBrowsableState.Never), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe")]
    internal interface IShellItem
    {
        [PreserveSig]
        int BindToHandler(IntPtr pbc, ref Guid bhid, ref Guid riid, out IntPtr ppv);
        [PreserveSig]
        int GetParent(out IShellItem ppsi);
        [PreserveSig]
        int GetDisplayName(SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);
        [PreserveSig]
        int GetAttributes(SFGAO sfgaoMask, out SFGAO psfgaoAttribs);
        [PreserveSig]
        int Compare(IShellItem psi, SICHINTF hint, out int piOrder);
    }
}

