namespace DevExpress.SpreadsheetSource
{
    using DevExpress.Export.Xl;
    using System;

    public interface ISpreadsheetSource : IDisposable
    {
        ISpreadsheetDataReader GetDataReader(XlCellRange range);
        ISpreadsheetDataReader GetDataReader(IDefinedName definedName);
        ISpreadsheetDataReader GetDataReader(IWorksheet worksheet);
        ISpreadsheetDataReader GetDataReader(IWorksheet worksheet, XlCellRange range);
        ISpreadsheetDataReader GetDataReader(IWorksheet worksheet, IDefinedName definedName);

        IWorksheetCollection Worksheets { get; }

        IDefinedNamesCollection DefinedNames { get; }

        ITablesCollection Tables { get; }

        SpreadsheetDocumentFormat DocumentFormat { get; }

        int MaxColumnCount { get; }

        int MaxRowCount { get; }

        ISpreadsheetSourceOptions Options { get; }
    }
}

