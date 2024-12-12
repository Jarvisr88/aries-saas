namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;

    public class D2D1StrokeStyle : D2D1Resource
    {
        public D2D1StrokeStyle(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public D2D1_CAP_STYLE GetDashCap() => 
            (D2D1_CAP_STYLE) ComObject.InvokeHelper.CalliInt(base.NativeObject, 6);

        public float[] GetDashes()
        {
            int dashesCount = this.GetDashesCount();
            float[] numArray = new float[dashesCount];
            if (dashesCount > 0)
            {
                using (ArrayMarshaler marshaler = new ArrayMarshaler(numArray))
                {
                    ComObject.InvokeHelper.Calli(base.NativeObject, marshaler.Pointer, dashesCount, 12);
                }
            }
            return numArray;
        }

        public int GetDashesCount() => 
            ComObject.InvokeHelper.CalliInt(base.NativeObject, 11);

        public float GetDashOffset() => 
            (float) ComObject.InvokeHelper.CalliInt(base.NativeObject, 9);

        public D2D1_DASH_STYLE GetDashStyle() => 
            (D2D1_DASH_STYLE) ComObject.InvokeHelper.CalliInt(base.NativeObject, 10);

        public D2D1_CAP_STYLE GetEndCap() => 
            (D2D1_CAP_STYLE) ComObject.InvokeHelper.CalliInt(base.NativeObject, 5);

        public D2D1_LINE_JOIN GetLineJoin() => 
            (D2D1_LINE_JOIN) ComObject.InvokeHelper.CalliInt(base.NativeObject, 8);

        public float GetMiterLimit() => 
            (float) ComObject.InvokeHelper.CalliInt(base.NativeObject, 7);

        public D2D1_CAP_STYLE GetStartCap() => 
            (D2D1_CAP_STYLE) ComObject.InvokeHelper.CalliInt(base.NativeObject, 4);
    }
}

