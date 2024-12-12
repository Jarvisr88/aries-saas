namespace DevExpress.Data.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class SvgBrush
    {
        protected SvgBrush(bool isDefault = false);
        public abstract IEnumerable<SvgDefinition> ExportData(SvgElementDataExportAgent dataAgent, IDefinitionKeysGenerator keysGenerator, string colorKey, string opacityKey);

        public bool IsDefault { get; }
    }
}

