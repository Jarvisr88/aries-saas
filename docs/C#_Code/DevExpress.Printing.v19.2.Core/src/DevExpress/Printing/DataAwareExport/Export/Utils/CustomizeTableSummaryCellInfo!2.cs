namespace DevExpress.Printing.DataAwareExport.Export.Utils
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    internal class CustomizeTableSummaryCellInfo<TCol, TRow> : CustomizeSummaryCellInfo<TCol, TRow> where TCol: class, IColumn where TRow: class, IRowBase
    {
        public IXlTableColumn TableColumn { get; set; }
    }
}

