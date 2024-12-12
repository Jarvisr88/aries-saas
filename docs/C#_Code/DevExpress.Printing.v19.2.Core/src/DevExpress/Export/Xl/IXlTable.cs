namespace DevExpress.Export.Xl
{
    using System;

    public interface IXlTable
    {
        XlTableReference GetReference(XlTablePart part);
        XlTableReference GetReference(XlTablePart part, string columnName);
        XlTableReference GetReference(XlTablePart part, string firstColumnName, string lastColumnName);
        XlTableReference GetRowReference(string columnName);
        XlTableReference GetRowReference(string firstColumnName, string lastColumnName);

        string Comment { get; set; }

        string Name { get; set; }

        bool HasHeaderRow { get; }

        bool HasTotalRow { get; }

        bool HasAutoFilter { get; set; }

        IXlCellRange Range { get; }

        IXlTableColumnCollection Columns { get; }

        IXlTableStyleInfo Style { get; }

        XlDifferentialFormatting DataFormatting { get; set; }

        XlDifferentialFormatting TotalRowFormatting { get; set; }

        XlDifferentialFormatting TableBorderFormatting { get; set; }
    }
}

