namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export;
    using System;
    using System.Runtime.CompilerServices;

    public class PasteCellValue<TCol> where TCol: class, IColumn
    {
        public TCol Column { get; set; }

        public object Value { get; set; }

        public RowPasteMode PasteMode { get; set; }
    }
}

