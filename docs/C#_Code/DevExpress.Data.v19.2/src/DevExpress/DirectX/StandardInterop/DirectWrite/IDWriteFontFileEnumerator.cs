namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("72755049-5ff7-435d-8348-4be97cfa6c7c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteFontFileEnumerator
    {
        [PreserveSig]
        int MoveNext([MarshalAs(UnmanagedType.Bool)] out bool hasCurrentFile);
        [PreserveSig]
        int GetCurrentFontFile([MarshalAs(UnmanagedType.Interface)] out IDWriteFontFile fontFile);
    }
}

