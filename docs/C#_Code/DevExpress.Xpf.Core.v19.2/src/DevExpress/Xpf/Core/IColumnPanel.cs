namespace DevExpress.Xpf.Core
{
    using System;

    public interface IColumnPanel
    {
        int ColumnCount { get; set; }

        int[] ColumnOffsets { get; set; }

        int[] RowOffsets { get; set; }
    }
}

