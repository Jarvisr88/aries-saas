namespace DevExpress.Data.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SvgGradientDefinition : SvgDefinition
    {
        public override IEnumerable<SvgDefinition> ExportData(SvgElementDataExportAgent dataAgent, IDefinitionKeysGenerator keysGenerator);

        public SvgTransformCollection TransformCollection { get; set; }

        public SvgGradientUnits Units { get; set; }
    }
}

