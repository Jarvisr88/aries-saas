namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;

    public interface IClipboardFormatted
    {
        int GetSelectedCellsCount(DataControlBase dataControl);
        IEnumerable<ColumnBase> GetSelectedColumns(DataControlBase dataControl);
        IEnumerable<int> GetSelectedRows(DataControlBase dataControl);
        bool IsSelect(int rowHanle, ColumnBase column, DataControlBase dataControl);
    }
}

