namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting.Native.LayoutAdjustment;
    using System;
    using System.Drawing;

    public interface ISubreportDocumentBand
    {
        void AddBottomSpan(float span);
        void AddTopSpan(float span);
        ILayoutData CreateLayoutData(LayoutDataContext layoutContext, RectangleF bounds);

        RectangleF ReportRect { get; set; }
    }
}

