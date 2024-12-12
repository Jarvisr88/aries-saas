namespace DevExpress.DirectX.NativeInterop.Direct2D.CCW
{
    using DevExpress.DirectX.NativeInterop.CCW;
    using System;
    using System.Runtime.InteropServices;

    [Guid("b2efe1e7-729f-4102-949f-505fa21bf666")]
    public interface ID2D1TransformNodeCCW : IUnknownCCW
    {
        [MethodOffset(0)]
        int GetInputCount();
    }
}

