namespace DevExpress.Export
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    public class CustomizeDocumentColumnEventArgs
    {
        public IXlColumn DocumentColumn { get; set; }

        public string ColumnFieldName { get; set; }
    }
}

