namespace DevExpress.SpreadsheetSource
{
    using DevExpress.Export.Xl;
    using System;

    public interface IWorksheet
    {
        string Name { get; }

        XlSheetVisibleState VisibleState { get; }
    }
}

