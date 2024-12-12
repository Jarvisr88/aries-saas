namespace DevExpress.DirectX.NativeInterop.Direct3D
{
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D3D11Resource : D3D11DeviceChild
    {
        public D3D11Resource(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public int GetEvictionPriority() => 
            ComObject.InvokeHelper.CalliInt(base.NativeObject, 9);

        public void GetNativeType()
        {
            throw new NotImplementedException();
        }

        public void SetEvictionPriority(int EvictionPriority)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, EvictionPriority, 8);
        }
    }
}

