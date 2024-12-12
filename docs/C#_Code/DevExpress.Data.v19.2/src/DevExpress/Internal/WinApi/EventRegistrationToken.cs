namespace DevExpress.Internal.WinApi
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), CLSCompliant(false)]
    public struct EventRegistrationToken
    {
        private ulong value;
    }
}

