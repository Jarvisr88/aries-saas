namespace DevExpress.DirectX.StandardInterop.Direct2D
{
    using DevExpress.DirectX.Common.Direct2D;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("2cd9069e-12e2-11dc-9fed-001143a055f9"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ID2D1SimplifiedGeometrySink
    {
        [PreserveSig]
        void SetFillMode(D2D1_FILL_MODE fillMode);
        [PreserveSig]
        void SetSegmentFlags(D2D1_PATH_SEGMENT vertexFlags);
        [PreserveSig]
        void BeginFigure(D2D_POINT_2F startPoint, D2D1_FIGURE_BEGIN figureBegin);
        [PreserveSig]
        void AddLines([MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] D2D_POINT_2F[] points, int pointCount);
        [PreserveSig]
        void AddBeziers([MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] D2D1_BEZIER_SEGMENT[] segments, int segmentCount);
        [PreserveSig]
        void EndFigure(D2D1_FIGURE_END figureEnd);
        [PreserveSig]
        int Close();
    }
}

