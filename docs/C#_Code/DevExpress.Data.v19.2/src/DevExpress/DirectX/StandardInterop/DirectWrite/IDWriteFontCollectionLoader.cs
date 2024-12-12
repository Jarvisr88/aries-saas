namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("cca920e4-52f0-492b-bfa8-29c72ee0a468"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteFontCollectionLoader
    {
        [PreserveSig]
        int CreateEnumeratorFromKey(IDWriteFactory factory, IntPtr collectionKey, int collectionKeySize, [MarshalAs(UnmanagedType.Interface)] out IDWriteFontFileEnumerator enumerator);
    }
}

