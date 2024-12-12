namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Collections.Generic;

    public interface IGroupRow<out TRow> : IRowBase where TRow: IRowBase
    {
        IEnumerable<TRow> GetAllRows();
        string GetGroupRowHeader();

        bool IsCollapsed { get; }

        bool ShowGroupFooter { get; }
    }
}

