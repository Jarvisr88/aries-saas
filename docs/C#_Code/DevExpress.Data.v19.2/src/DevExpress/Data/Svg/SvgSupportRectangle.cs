namespace DevExpress.Data.Svg
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class SvgSupportRectangle : SvgVisualElement
    {
        public SvgSupportRectangle();
        public SvgSupportRectangle(double x, double y, double width, double height);
        protected override void ExportFields(SvgElementDataExportAgent dataAgent);
        protected override bool FillFields(SvgElementDataImportAgent dataAgent);
        public override SvgRect GetBoundaryPoints();

        public double Width { get; protected set; }

        public double Height { get; protected set; }

        public SvgPoint Location { get; protected set; }
    }
}

