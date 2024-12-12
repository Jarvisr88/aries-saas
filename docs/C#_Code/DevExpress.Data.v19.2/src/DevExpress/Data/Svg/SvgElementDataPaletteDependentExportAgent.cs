namespace DevExpress.Data.Svg
{
    using DevExpress.Utils.Design;
    using System;
    using System.Drawing;
    using System.Xml;

    public class SvgElementDataPaletteDependentExportAgent : SvgElementDataExportAgent
    {
        private readonly ISvgPaletteProvider paletteProvider;

        public SvgElementDataPaletteDependentExportAgent(XmlElement element, ISvgPaletteProvider paletteProvider);
        public override void SetColorValue(string key, Color value);
    }
}

