namespace DevExpress.Data.Camera
{
    using DevExpress.Data.Camera.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Security;

    [SecuritySafeCritical]
    public static class DeviceHelper
    {
        public static List<CameraDeviceInfo> GetDevices();
        internal static IPin GetPin(this IBaseFilter filter, PinDirection dir, int num);

        public static CameraDeviceInfo[] DeviceMonikers { get; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static CameraDeviceInfo[] DeviceMonikes { get; }
    }
}

