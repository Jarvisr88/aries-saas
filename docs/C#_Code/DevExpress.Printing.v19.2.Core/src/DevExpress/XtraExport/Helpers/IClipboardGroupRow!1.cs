namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.Generic;

    public interface IClipboardGroupRow<out TRow> : IGroupRow<TRow>, IRowBase where TRow: IRowBase
    {
        string GetGroupedColumnFieldName();
        IEnumerable<TRow> GetSelectedRows();
        bool IsTreeListGroupRow();
    }
}

