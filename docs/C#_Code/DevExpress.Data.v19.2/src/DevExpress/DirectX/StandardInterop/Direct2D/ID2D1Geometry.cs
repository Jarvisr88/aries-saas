namespace DevExpress.DirectX.StandardInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("2cd906a1-12e2-11dc-9fed-001143a055f9"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID2D1Geometry
    {
        void GetFactory();
        void GetBounds(ref D2D_MATRIX_3X2_F worldTransform, out D2D_RECT_F bounds);
        void GetWidenedBounds(float lineWidth, ID2D1StrokeStyle strokeStyke, ref D2D_MATRIX_3X2_F worldTransform, float flatteningTolerance, out D2D_RECT_F bounds);
        void StrokeContainsPoint();
        void FillContainsPoint();
        void CompareWithGeometry();
        void Simplify();
        void Tessellate();
        void CombineWithGeometry(ID2D1Geometry inputGeometry, D2D1_COMBINE_MODE combineMode, ref D2D_MATRIX_3X2_F inputGeometryTransform, float flatteningTolerance, ID2D1SimplifiedGeometrySink geometrySink);
        void Outline();
        void ComputeArea();
        void ComputeLength();
        void ComputePointAtLength();
        void Widen(float strokeWidth, ID2D1StrokeStyle strokeStyle, ref D2D_MATRIX_3X2_F transform, float flatteningTolerance, ID2D1SimplifiedGeometrySink geometrySink);
    }
}

