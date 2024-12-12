namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export.Xl;
    using System;

    public interface ISheetCustomizationContext
    {
        void AddAutoFilter(XlCellRange range);
        void SetFixedHeader(int documentRow);
    }
}

