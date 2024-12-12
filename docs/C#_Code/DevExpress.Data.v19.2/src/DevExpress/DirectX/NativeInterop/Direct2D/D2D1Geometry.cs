namespace DevExpress.DirectX.NativeInterop.Direct2D
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.Direct2D;
    using DevExpress.DirectX.NativeInterop;
    using System;
    using System.Runtime.InteropServices;

    public class D2D1Geometry : D2D1Resource
    {
        public D2D1Geometry(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void CombineWithGeometry(D2D1Geometry inputGeometry, D2D1_COMBINE_MODE combineMode, D2D_MATRIX_3X2_F inputGeometryTransform, float flatteningTolerance, D2D1SimplifiedGeometrySink geometrySink)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, inputGeometry.ToNativeObject(), (int) combineMode, ref inputGeometryTransform, flatteningTolerance, geometrySink.ToNativeObject(), 11));
        }

        public void CompareWithGeometry()
        {
            throw new NotImplementedException();
        }

        public float ComputeArea(D2D_MATRIX_3X2_F worldTransform, float flatteningTolerance)
        {
            float num;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, ref worldTransform, flatteningTolerance, out num, 13));
            return num;
        }

        public float ComputeLength(D2D_MATRIX_3X2_F worldTransform, float flatteningTolerance)
        {
            float num;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, ref worldTransform, flatteningTolerance, out num, 14));
            return num;
        }

        public void ComputePointAtLength(float lenght, D2D_MATRIX_3X2_F worldTransform, out D2D_POINT_2F point, out D2D_POINT_2F unitTangentVector)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, lenght, ref worldTransform, 0.25f, out point, out unitTangentVector, 15));
        }

        public void FillContainsPoint()
        {
            throw new NotImplementedException();
        }

        public D2D_RECT_F GetBounds(D2D_MATRIX_3X2_F worldTransform)
        {
            D2D_RECT_F infinite = D2D_RECT_F.Infinite;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, ref worldTransform, ref infinite, 4));
            return infinite;
        }

        public D2D_RECT_F GetWidenedBounds(float strokeWidth, D2D1StrokeStyle strokeStyle, D2D_MATRIX_3X2_F worldTransform)
        {
            D2D_RECT_F infinite = D2D_RECT_F.Infinite;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, strokeWidth, strokeStyle.ToNativeObject(), ref worldTransform, 0.25f, ref infinite, 5));
            return infinite;
        }

        public void Outline(D2D_MATRIX_3X2_F worldTransform, float flatteningTolerance, D2D1SimplifiedGeometrySink geometrySink)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, ref worldTransform, flatteningTolerance, geometrySink.ToNativeObject(), 12));
        }

        public void Simplify()
        {
            throw new NotImplementedException();
        }

        public void StrokeContainsPoint()
        {
            throw new NotImplementedException();
        }

        public void Tessellate()
        {
            throw new NotImplementedException();
        }

        public void Widen(float strokeWidth, D2D1StrokeStyle strokeStyle, D2D_MATRIX_3X2_F worldTransform, float flatteningTolerance, D2D1SimplifiedGeometrySink geometrySink)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, strokeWidth, strokeStyle.ToNativeObject(), ref worldTransform, flatteningTolerance, geometrySink.ToNativeObject(), 0x10));
        }
    }
}

