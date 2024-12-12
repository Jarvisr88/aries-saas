namespace DevExpress.DirectX.NativeInterop.Direct3D
{
    using DevExpress.DirectX.NativeInterop;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class D3DBlob : ComObject
    {
        internal D3DBlob(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public IntPtr GetBufferPointer() => 
            ComObject.InvokeHelper.CalliIntPtr(base.NativeObject, 3);

        public int GetBufferSize() => 
            ComObject.InvokeHelper.CalliIntPtr(base.NativeObject, 4).ToInt32();

        [SecuritySafeCritical]
        public byte[] GetData()
        {
            byte[] destination = new byte[this.GetBufferSize()];
            Marshal.Copy(this.GetBufferPointer(), destination, 0, destination.Length);
            return destination;
        }
    }
}

