namespace DevExpress.Export
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    public class BeforeExportTableEventArgs
    {
        public IXlTable Table { get; internal set; }

        public bool UseTableTotalFooter { get; set; }
    }
}

