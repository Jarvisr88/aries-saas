namespace DevExpress.XtraExport.Helpers
{
    using System;

    public interface IGridOptionsView
    {
        bool ShowFooter { get; }

        bool ShowGroupFooter { get; }

        bool ColumnAutoWidth { get; }

        bool ShowGroupedColumns { get; }

        bool RowAutoHeight { get; }
    }
}

