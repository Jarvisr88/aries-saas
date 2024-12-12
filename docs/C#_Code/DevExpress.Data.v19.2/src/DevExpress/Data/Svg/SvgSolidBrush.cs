namespace DevExpress.Data.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class SvgSolidBrush : SvgBrush
    {
        private System.Drawing.Color color;

        public SvgSolidBrush(System.Drawing.Color color);
        private SvgSolidBrush(System.Drawing.Color color, bool isDefault);
        public static SvgSolidBrush CreateDefault(System.Drawing.Color color);
        public override IEnumerable<SvgDefinition> ExportData(SvgElementDataExportAgent dataAgent, IDefinitionKeysGenerator keysGenerator, string colorKey, string opacityKey);

        public System.Drawing.Color Color { get; }
    }
}

