namespace DevExpress.Data.Svg
{
    using System;
    using System.Runtime.CompilerServices;

    [FormatElement("path")]
    public class SvgPath : SvgVisualElement
    {
        private SvgCommandCollection commandCollection;

        public SvgPath();
        public override T CreatePlatformItem<T>(ISvgElementFactory<T> factory);
        protected override void ExportFields(SvgElementDataExportAgent dataAgent);
        protected override bool FillFields(SvgElementDataImportAgent dataAgent);
        public override SvgRect GetBoundaryPoints();

        private bool IsFilledCorrectly { get; }

        public SvgCommandCollection CommandCollection { get; }

        public SvgFillRule FillRule { get; set; }

        public SvgFillRule ClipRule { get; set; }
    }
}

