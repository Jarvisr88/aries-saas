namespace DevExpress.DirectX.StandardInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("47dd575d-ac05-4cdd-8049-9b02cd16f44c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID2D1Device
    {
        void GetFactory();
        ID2D1DeviceContext CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS options);
        ID2D1PrintControl CreatePrintControl(IntPtr wicFactory, IntPtr printTarget, ref D2D1_PRINT_CONTROL_PROPERTIES properties);
        void SetMaximumTextureMemory(long maximumInBytes);
        long GetMaximumTextureMemory();
        void ClearResources(int millisecondsSinceUse);
    }
}

