namespace DevExpress.Data.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [FormatElement("radialGradient")]
    public class SvgRadialGradientDefinition : SvgGradientDefinition
    {
        public override IEnumerable<SvgDefinition> ExportData(SvgElementDataExportAgent dataAgent, IDefinitionKeysGenerator keysGenerator);

        public SvgPoint Location { get; set; }

        public double Radius { get; set; }
    }
}

