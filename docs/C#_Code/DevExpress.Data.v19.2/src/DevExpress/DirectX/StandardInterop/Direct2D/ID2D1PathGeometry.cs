namespace DevExpress.DirectX.StandardInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("2cd906a5-12e2-11dc-9fed-001143a055f9"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID2D1PathGeometry
    {
        void GetFactory();
        void GetBounds();
        void GetWidenedBounds();
        void StrokeContainsPoint();
        void FillContainsPoint();
        void CompareWithGeometry();
        void Simplify();
        void Tessellate();
        void CombineWithGeometry(ID2D1Geometry inputGeometry, D2D1_COMBINE_MODE combineMode, ref D2D_MATRIX_3X2_F inputGeometryTransform, float flatteningTolerance, [In] ID2D1SimplifiedGeometrySink geometrySink);
        void Outline();
        void ComputeArea();
        void ComputeLength();
        void ComputePointAtLength();
        void Widen();
        ID2D1GeometrySink Open();
        void Stream();
        void GetSegmentCount();
        void GetFigureCount();
    }
}

