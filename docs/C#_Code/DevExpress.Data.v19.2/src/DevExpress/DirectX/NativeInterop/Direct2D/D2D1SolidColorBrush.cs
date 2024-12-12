namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D2D1SolidColorBrush : D2D1Brush
    {
        public D2D1SolidColorBrush(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public D2D1_COLOR_F GetColor()
        {
            throw new NotImplementedException();
        }

        public void SetColor(D2D1_COLOR_F color)
        {
            ComObject.InvokeHelper.Calli(base.NativeObject, ref color, 8);
        }
    }
}

