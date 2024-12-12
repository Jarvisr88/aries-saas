namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using DevExpress.DirectX.NativeInterop.PrintDocumentPackage;
    using DevExpress.DirectX.NativeInterop.WIC;
    using System;
    using System.Security;

    public class D2D1Device : D2D1Resource
    {
        public D2D1Device(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void ClearResources(int millisecondsSinceUse)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, millisecondsSinceUse, 8);
        }

        public D2D1DeviceContext CreateDeviceContext(D2D1_DEVICE_CONTEXT_OPTIONS options)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, (int) options, out ptr, 4));
            return new D2D1DeviceContext(ptr);
        }

        [SecuritySafeCritical]
        public D2D1PrintControl CreatePrintControl(WICImagingFactory wicFactory, PrintDocumentPackageTarget target, D2D1_PRINT_CONTROL_PROPERTIES properties)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, wicFactory.ToNativeObject(), target.ToNativeObject(), ref properties, out ptr, 5));
            return new D2D1PrintControl(ptr);
        }

        public long GetMaximumTextureMemory()
        {
            throw new NotImplementedException();
        }

        public void SetMaximumTextureMemory(long maximumInBytes)
        {
            throw new NotImplementedException();
        }
    }
}

