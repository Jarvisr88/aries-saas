namespace DevExpress.DirectX.NativeInterop.Direct3D
{
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D3D11DeviceChild : ComObject
    {
        public D3D11DeviceChild(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public D3D11Device GetDevice()
        {
            IntPtr ptr;
            ComObject.InvokeHelper.Calli(base.NativeObject, out ptr, 3);
            return new D3D11Device(ptr);
        }

        public void GetPrivateData(Guid guid, int pDataSize, IntPtr pData)
        {
            throw new NotImplementedException();
        }

        public void SetPrivateData(Guid guid, int DataSize, IntPtr pData)
        {
            throw new NotImplementedException();
        }

        public void SetPrivateDataInterface(Guid guid, object pData)
        {
            throw new NotImplementedException();
        }
    }
}

