namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export;
    using DevExpress.Export.Xl;
    using System;
    using System.Collections.Generic;

    internal interface IColumnExportProvider<in TRow> where TRow: class, IRowBase
    {
        bool CanExportValue(SheetAreaType area);
        void ClearCaches();
        void ExportValue(SheetAreaType area, IXlCell cell, TRow gridRow, int exportRowIndex);

        int Position { get; }

        string FieldName { get; }

        bool HasEmptyCells { get; }

        bool CanExecuteExport { get; }

        bool Used { get; set; }

        List<XlCellPosition> EmptyCellsPositions { get; }
    }
}

