namespace DevExpress.Internal.WinApi
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IPropertyStore
    {
        uint GetCount(out uint propertyCount);
        uint GetAt([In] uint propertyIndex, out PropertyKey key);
        uint GetValue([In] ref PropertyKey key, [Out] PropVariant pv);
        uint SetValue([In] ref PropertyKey key, [In] PropVariant pv);
        uint Commit();
    }
}

