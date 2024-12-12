namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.ObjectModel;

    public interface IClipboardData
    {
        ReadOnlyCollection<IClipboardRow> Rows { get; }

        int RowCount { get; }

        int ColumnCount { get; }
    }
}

