namespace DevExpress.Export.Xl
{
    using System;
    using System.Collections.Generic;

    public interface IXlSheet : IDisposable
    {
        void BeginFiltering(XlCellRange autoFilterRange);
        int BeginGroup(bool collapsed);
        int BeginGroup(int outlineLevel, bool collapsed);
        IXlColumn CreateColumn();
        IXlColumn CreateColumn(int columnIndex);
        IXlPicture CreatePicture();
        IXlRow CreateRow();
        IXlRow CreateRow(int rowIndex);
        void EndFiltering();
        void EndGroup();
        void SkipColumns(int count);
        void SkipRows(int count);

        string Name { get; set; }

        IXlMergedCells MergedCells { get; }

        XlCellPosition SplitPosition { get; set; }

        XlCellRange AutoFilterRange { get; set; }

        IXlFilterColumns AutoFilterColumns { get; }

        IList<XlConditionalFormatting> ConditionalFormattings { get; }

        IList<XlDataValidation> DataValidations { get; }

        XlSheetVisibleState VisibleState { get; set; }

        XlPageMargins PageMargins { get; set; }

        XlPageSetup PageSetup { get; set; }

        XlHeaderFooter HeaderFooter { get; }

        XlPrintTitles PrintTitles { get; }

        XlCellRange PrintArea { get; set; }

        XlPrintOptions PrintOptions { get; set; }

        IXlPageBreaks ColumnPageBreaks { get; }

        IXlPageBreaks RowPageBreaks { get; }

        IList<XlHyperlink> Hyperlinks { get; }

        XlCellRange DataRange { get; }

        XlCellRange ColumnRange { get; }

        XlIgnoreErrors IgnoreErrors { get; set; }

        IXlOutlineProperties OutlineProperties { get; }

        IXlSheetViewOptions ViewOptions { get; }

        IList<XlSparklineGroup> SparklineGroups { get; }

        IXlTableCollection Tables { get; }

        IXlSheetSelection Selection { get; }

        int CurrentRowIndex { get; }

        int CurrentColumnIndex { get; }

        int CurrentOutlineLevel { get; }
    }
}

