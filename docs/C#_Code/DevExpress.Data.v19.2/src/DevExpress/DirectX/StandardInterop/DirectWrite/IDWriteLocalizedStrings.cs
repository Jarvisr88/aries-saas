namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("08256209-099a-4b34-b86d-c22b110e7771"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteLocalizedStrings
    {
        [PreserveSig]
        int GetCount();
        void FindLocaleName([MarshalAs(UnmanagedType.LPWStr)] string localeName, out int index, [MarshalAs(UnmanagedType.Bool)] out bool exisit);
        void GetLocaleNameLength(int index, out int length);
        void GetLocaleName(int index, [MarshalAs(UnmanagedType.LPWStr)] string str, int length);
        void GetStringLength(int index, out int length);
        void GetString(int index, [MarshalAs(UnmanagedType.LPWStr)] string str, int length);
    }
}

