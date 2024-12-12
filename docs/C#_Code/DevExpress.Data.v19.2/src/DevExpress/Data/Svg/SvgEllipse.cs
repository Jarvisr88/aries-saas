namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("ellipse")]
    public class SvgEllipse : SvgSupportRectangle, ISvgEllipseAdapter
    {
        public SvgEllipse();
        public SvgEllipse(double cx, double cy, double rx, double ry);
        public override T CreatePlatformItem<T>(ISvgElementFactory<T> factory);
        protected override void ExportFields(SvgElementDataExportAgent dataAgent);
        protected override bool FillFields(SvgElementDataImportAgent dataAgent);
        public override SvgRect GetBoundaryPoints();

        SvgEllipse ISvgEllipseAdapter.Ellipse { get; }
    }
}

