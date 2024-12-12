namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D2D1Effect : D2D1Properties
    {
        public D2D1Effect(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public D2D1Image GetInput(int index)
        {
            IntPtr ptr;
            ComObject.InvokeHelper.Calli(base.NativeObject, index, out ptr, 0x10);
            return new D2D1Image(ptr);
        }

        public int GetInputCount() => 
            ComObject.InvokeHelper.CalliInt(base.NativeObject, 0x11);

        public D2D1Image GetOutput()
        {
            IntPtr ptr;
            ComObject.InvokeHelper.Calli(base.NativeObject, out ptr, 0x12);
            return new D2D1Image(ptr);
        }

        public void SetInput(int index, D2D1Image input, bool invalidate)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, index, input.ToNativeObject(), MarshalBool(invalidate), 14);
        }

        public void SetInputCount(int inputCount)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, inputCount, 15));
        }
    }
}

