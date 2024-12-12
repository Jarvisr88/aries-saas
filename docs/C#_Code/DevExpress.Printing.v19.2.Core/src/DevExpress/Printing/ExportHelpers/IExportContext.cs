namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.Drawing;

    public interface IExportContext
    {
        void AddRow();
        void AddRow(CellObject[] values);
        void AddRow(object[] values);
        void InsertImage(Image image, XlCellRange range);
        void MergeCells(XlCellRange range);
    }
}

