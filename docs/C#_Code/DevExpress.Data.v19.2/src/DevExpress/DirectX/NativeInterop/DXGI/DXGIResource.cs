namespace DevExpress.DirectX.NativeInterop.DXGI
{
    using DevExpress.DirectX.NativeInterop;
    using System;
    using System.Runtime.InteropServices;

    [Guid("035f3ab4-482e-4e50-b41f-8a7f8bd8960b")]
    public class DXGIResource : DXGIDeviceSubObject
    {
        public DXGIResource(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void GetEvictionPriority()
        {
            throw new NotImplementedException();
        }

        public void GetSharedHandle(out IntPtr sharedHandle)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, out sharedHandle, 8);
        }

        public void GetUsage(int usageRef)
        {
            throw new NotImplementedException();
        }

        public void SetEvictionPriority(int evictionPriority)
        {
            throw new NotImplementedException();
        }

        public IntPtr SharedHandle
        {
            get
            {
                IntPtr ptr;
                this.GetSharedHandle(out ptr);
                return ptr;
            }
        }
    }
}

