namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D2D1VertexBuffer : ComObject
    {
        internal D2D1VertexBuffer(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void Map(IntPtr data, int bufferSize)
        {
        }

        public void Unmap()
        {
        }
    }
}

