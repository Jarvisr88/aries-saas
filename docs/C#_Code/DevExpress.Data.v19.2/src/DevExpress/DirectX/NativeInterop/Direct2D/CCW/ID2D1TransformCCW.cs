namespace DevExpress.DirectX.NativeInterop.Direct2D.CCW
{
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop.CCW;
    using System;
    using System.Runtime.InteropServices;

    [Guid("ef1a287d-342a-4f76-8fdb-da0d6ea9f92b")]
    public interface ID2D1TransformCCW : ID2D1TransformNodeCCW, IUnknownCCW
    {
        [MethodOffset(0)]
        int MapOutputRectToInputRects(ref D2D1_RECT_L outputRect, IntPtr inputRects, int inputRectsCount);
        [MethodOffset(1)]
        int MapInputRectsToOutputRect(IntPtr inputRects, IntPtr inputOpaqueSubRects, int inputRectCount, out D2D1_RECT_L outputRect, out D2D1_RECT_L outputOpaqueSubRect);
        [MethodOffset(2)]
        int MapInvalidRect(int inputIndex, D2D1_RECT_L invalidInputRect, out D2D1_RECT_L invalidOutputRect);
    }
}

