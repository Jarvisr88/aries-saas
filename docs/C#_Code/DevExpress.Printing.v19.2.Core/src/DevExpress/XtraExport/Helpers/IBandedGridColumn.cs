namespace DevExpress.XtraExport.Helpers
{
    using System;

    public interface IBandedGridColumn : IColumn
    {
        int ColVIndex { get; }

        int ColIndex { get; }

        int RowCount { get; }

        int RowIndex { get; }

        int Id { get; }

        bool AutoFillDown { get; }
    }
}

