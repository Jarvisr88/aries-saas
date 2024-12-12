namespace DevExpress.Office.PInvoke
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("965FC360-16FF-11d0-91CB-00AA00BBB723")]
    public interface ISecurityInformation
    {
        void GetObjectInformation(ref Win32.SI_OBJECT_INFO object_info);
        void GetSecurity(int requestInformation, IntPtr securityDescriptor, bool fDefault);
        void SetSecurity(int requestInformation, IntPtr securityDescriptor);
        void GetAccessRight(IntPtr guidObject, int dwFlags, [MarshalAs(UnmanagedType.LPArray)] out Win32.SI_ACCESS[] access, ref int accessCount, ref int defaultAccess);
        void MapGeneric(IntPtr guidObjectType, IntPtr aceFlags, IntPtr mask);
        void GetInheritTypes(ref Win32.SI_INHERIT_TYPE inheritType, IntPtr inheritTypesCount);
        void PropertySheetPageCallback(IntPtr hwnd, int uMsg, int uPage);
    }
}

