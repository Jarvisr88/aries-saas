namespace DevExpress.Data.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [FormatElement("linearGradient")]
    public class SvgLinearGradientDefinition : SvgGradientDefinition
    {
        public override IEnumerable<SvgDefinition> ExportData(SvgElementDataExportAgent dataAgent, IDefinitionKeysGenerator keysGenerator);

        public SvgPoint Point1 { get; set; }

        public SvgPoint Point2 { get; set; }
    }
}

