namespace DevExpress.Export.Xl
{
    using System;
    using System.Globalization;

    public interface IXlExpressionContext
    {
        XlCellPosition CurrentCell { get; set; }

        string CurrentSheetName { get; }

        XlCellReferenceMode ReferenceMode { get; set; }

        XlCellReferenceStyle ReferenceStyle { get; }

        int MaxColumnCount { get; }

        int MaxRowCount { get; }

        XlExpressionStyle ExpressionStyle { get; }

        CultureInfo Culture { get; }
    }
}

