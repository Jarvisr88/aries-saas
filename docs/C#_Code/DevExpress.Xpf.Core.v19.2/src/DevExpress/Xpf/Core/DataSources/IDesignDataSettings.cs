namespace DevExpress.Xpf.Core.DataSources
{
    using System;

    public interface IDesignDataSettings
    {
        bool FlattenHierarchy { get; set; }

        int RowCount { get; set; }

        bool UseDistinctValues { get; set; }

        Type DataObjectType { get; set; }
    }
}

