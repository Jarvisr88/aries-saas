namespace DevExpress.DirectX.StandardInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("2cd9069f-12e2-11dc-9fed-001143a055f9"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID2D1GeometrySink
    {
        [PreserveSig]
        void SetFillMode(D2D1_FILL_MODE fillMode);
        void SetSegmentFlags();
        [PreserveSig]
        void BeginFigure(D2D_POINT_2F startPoint, D2D1_FIGURE_BEGIN figureBegin);
        void AddLines();
        void AddBeziers();
        [PreserveSig]
        void EndFigure(D2D1_FIGURE_END figureEnd);
        void Close();
        [PreserveSig]
        void AddLine(D2D_POINT_2F point);
        [PreserveSig]
        void AddBezier(ref D2D1_BEZIER_SEGMENT bezier);
        void AddQuadraticBezier();
        void AddQuadraticBeziers();
        void AddArc();
    }
}

