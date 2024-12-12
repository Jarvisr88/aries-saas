namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("circle")]
    public class SvgCircle : SvgVisualElement, ISvgEllipseAdapter
    {
        private double radius;
        private SvgPoint location;

        public SvgCircle();
        public SvgCircle(SvgPoint location, double radius);
        public SvgCircle(double cx, double cy, double radius);
        public override T CreatePlatformItem<T>(ISvgElementFactory<T> factory);
        protected override void ExportFields(SvgElementDataExportAgent dataAgent);
        protected override bool FillFields(SvgElementDataImportAgent dataAgent);
        public override SvgRect GetBoundaryPoints();

        public double Radius { get; }

        public SvgPoint Location { get; }

        SvgEllipse ISvgEllipseAdapter.Ellipse { get; }
    }
}

