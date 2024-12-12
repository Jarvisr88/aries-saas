namespace DevExpress.Data.Svg
{
    using System;

    public abstract class SvgPointContainer : SvgVisualElement
    {
        private SvgPointCollection pointCollection;

        protected SvgPointContainer();
        protected override void ExportFields(SvgElementDataExportAgent dataAgent);
        protected override bool FillFields(SvgElementDataImportAgent dataAgent);
        public override SvgRect GetBoundaryPoints();

        public SvgPointCollection PointCollection { get; }
    }
}

