namespace DevExpress.Xpf.Core.DataSources
{
    using System;
    using System.Runtime.CompilerServices;

    public class DesignDataSettings : IDesignDataSettings
    {
        public bool FlattenHierarchy { get; set; }

        public int RowCount { get; set; }

        public bool UseDistinctValues { get; set; }

        public Type DataObjectType { get; set; }
    }
}

