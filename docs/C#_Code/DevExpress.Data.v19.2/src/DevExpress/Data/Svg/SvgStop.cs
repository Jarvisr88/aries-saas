namespace DevExpress.Data.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    [FormatElement("stop")]
    public class SvgStop : SvgItem
    {
        private int offset;
        private System.Drawing.Color color;

        public SvgStop(int offset, System.Drawing.Color color);
        public override IEnumerable<SvgDefinition> ExportData(SvgElementDataExportAgent dataAgent, IDefinitionKeysGenerator keysGenerator);
        public override bool FillData(SvgElementDataImportAgent dataAgent);

        public int Offset { get; }

        public System.Drawing.Color Color { get; }
    }
}

