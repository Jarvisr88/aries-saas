namespace DevExpress.Internal.WinApi.Windows.UI.Notifications
{
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct NOTIFICATION_USER_INPUT_DATA
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Key;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Value;
    }
}

