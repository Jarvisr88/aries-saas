namespace DevExpress.DirectX.NativeInterop.DXGI
{
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class DXGIObject : ComObject
    {
        public DXGIObject(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void GetParent(Guid riid)
        {
            throw new NotImplementedException();
        }

        public void GetPrivateData(Guid Name, int pDataSize, IntPtr pData)
        {
            throw new NotImplementedException();
        }

        public void SetPrivateData(Guid Name, int DataSize, IntPtr pData)
        {
            throw new NotImplementedException();
        }

        public void SetPrivateDataInterface(Guid Name, object pUnknown)
        {
            throw new NotImplementedException();
        }
    }
}

