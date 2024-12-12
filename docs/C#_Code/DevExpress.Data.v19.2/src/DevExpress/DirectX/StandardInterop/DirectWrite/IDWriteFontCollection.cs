namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("a84cee02-3eea-4eee-a827-87c1a02a0fcc"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteFontCollection
    {
        [PreserveSig]
        int GetFontFamilyCount();
        IDWriteFontFamily GetFontFamily(int index);
        [return: MarshalAs(UnmanagedType.Bool)]
        bool FindFamilyName(string familyName, out int index);
        void GetFontFromFontFace();
    }
}

