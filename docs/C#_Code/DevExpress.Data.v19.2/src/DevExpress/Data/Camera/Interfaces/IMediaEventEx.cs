namespace DevExpress.Data.Camera.Interfaces
{
    using DevExpress.Data.Camera;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [ComImport, Guid("56a868c0-0ad4-11ce-b03a-0020af0ba770"), InterfaceType(ComInterfaceType.InterfaceIsDual), SuppressUnmanagedCodeSecurity]
    public interface IMediaEventEx
    {
        [PreserveSig]
        int GetEventHandle(out IntPtr hEvent);
        [PreserveSig]
        int GetEvent(out EventCode lEventCode, out IntPtr lParam1, out IntPtr lParam2, [In] int msTimeout);
        [PreserveSig]
        int WaitForCompletion([In] int msTimeout, out EventCode pEvCode);
        [PreserveSig]
        int CancelDefaultHandling([In] EventCode lEvCode);
        [PreserveSig]
        int RestoreDefaultHandling([In] EventCode lEvCode);
        [PreserveSig]
        int FreeEventParams([In] EventCode lEvCode, [In] IntPtr lParam1, [In] IntPtr lParam2);
        [PreserveSig]
        int SetNotifyWindow([In] IntPtr hwnd, [In] int lMsg, [In] IntPtr lInstanceData);
        [PreserveSig]
        int SetNotifyFlags([In] NotifyFlags lNoNotifyFlags);
        [PreserveSig]
        int GetNotifyFlags(out NotifyFlags lplNoNotifyFlags);
    }
}

