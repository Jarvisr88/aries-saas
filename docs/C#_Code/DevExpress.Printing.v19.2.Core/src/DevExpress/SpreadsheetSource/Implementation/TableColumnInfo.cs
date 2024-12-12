namespace DevExpress.SpreadsheetSource.Implementation
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    public class TableColumnInfo
    {
        public string Name { get; set; }

        public XlNumberFormat NumberFormat { get; set; }

        public XlNumberFormat TotalRowNumberFormat { get; set; }
    }
}

