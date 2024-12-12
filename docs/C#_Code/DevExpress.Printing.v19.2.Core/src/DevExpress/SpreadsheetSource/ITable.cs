namespace DevExpress.SpreadsheetSource
{
    using DevExpress.Export.Xl;
    using System;

    public interface ITable
    {
        string Name { get; }

        XlCellRange Range { get; }

        XlCellRange DataRange { get; }

        string RefersTo { get; }

        bool HasHeaderRow { get; }

        bool HasTotalRow { get; }
    }
}

