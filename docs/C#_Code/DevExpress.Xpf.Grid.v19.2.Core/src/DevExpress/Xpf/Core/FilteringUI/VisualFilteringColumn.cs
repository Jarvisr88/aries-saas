namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    internal class VisualFilteringColumn
    {
        public VisualFilteringColumn(string name, HeaderAppearanceAccessor getHeaderAppearance)
        {
            Guard.ArgumentNotNull(getHeaderAppearance, "getHeaderAppearance");
            this.<GetHeaderAppearance>k__BackingField = getHeaderAppearance;
            this.<Name>k__BackingField = name;
        }

        public string Name { get; }

        public HeaderAppearanceAccessor GetHeaderAppearance { get; }
    }
}

