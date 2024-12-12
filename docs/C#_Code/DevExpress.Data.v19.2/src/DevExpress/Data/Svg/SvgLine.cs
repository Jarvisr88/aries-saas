namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("line")]
    public class SvgLine : SvgVisualElement
    {
        private SvgPoint point1;
        private SvgPoint point2;

        public SvgLine();
        public SvgLine(double x1, double y1, double x2, double y2);
        public override T CreatePlatformItem<T>(ISvgElementFactory<T> factory);
        protected override void ExportFields(SvgElementDataExportAgent dataAgent);
        protected override bool FillFields(SvgElementDataImportAgent dataAgent);
        public override SvgRect GetBoundaryPoints();

        public SvgPoint Point1 { get; }

        public SvgPoint Point2 { get; }
    }
}

