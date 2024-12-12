namespace DevExpress.Xpf.Printing.Native.Dialogs
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    [ComImport, Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Guid("973510DB-7D7F-452B-8975-74A85828D354"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IFileDialogEvents
    {
        [PreserveSig]
        int OnFileOk([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);
        [PreserveSig]
        int OnFolderChanging([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd, [In, MarshalAs(UnmanagedType.Interface)] IShellItem psiFolder);
        void OnFolderChange([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);
        void OnSelectionChange([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);
        void OnShareViolation([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd, [In, MarshalAs(UnmanagedType.Interface)] IShellItem psi, out FDE_SHAREVIOLATION_RESPONSE pResponse);
        void OnTypeChange([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd);
        void OnOverwrite([In, MarshalAs(UnmanagedType.Interface)] IFileDialog pfd, [In, MarshalAs(UnmanagedType.Interface)] IShellItem psi, out FDE_OVERWRITE_RESPONSE pResponse);
    }
}

