namespace DevExpress.Data.Svg
{
    using System;

    [FormatElement("text")]
    public class SvgText : SvgVisualElement
    {
        private SvgPoint location;
        private SvgSize offset;
        private double fontSize;
        private string fontFamily;
        private string text;
        private SvgRect bounds;
        private SvgTextAnchor textAnchor;

        public SvgText();
        public SvgText(SvgPoint location, SvgSize offset, SvgTextAnchor textAnchor, string text, SvgRect bounds);
        public SvgText(SvgPoint location, SvgSize offset, SvgTextAnchor textAnchor, string text, string fontFamily, double fontSize, SvgRect bounds);
        public override T CreatePlatformItem<T>(ISvgElementFactory<T> factory);
        protected override void ExportFields(SvgElementDataExportAgent dataAgent);
        protected override bool FillFields(SvgElementDataImportAgent dataAgent);
        public override SvgRect GetBoundaryPoints();

        public SvgPoint Location { get; }

        public SvgSize Offset { get; }

        public double FontSize { get; }

        public string FontFamily { get; }

        public string Text { get; }

        public SvgRect Bounds { get; }

        public SvgTextAnchor TextAnchor { get; }
    }
}

