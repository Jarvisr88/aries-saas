namespace DevExpress.Data.Svg
{
    using DevExpress.Utils.Design;
    using System;
    using System.Xml;

    public class SvgPaletteDependentExporter : SvgExporter
    {
        private readonly ISvgPaletteProvider paletteProvider;

        public SvgPaletteDependentExporter(SvgSize size, ISvgPaletteProvider paletteProvider);
        protected override SvgElementDataExportAgent CreateExportAgent(XmlElement xmlElement);
    }
}

