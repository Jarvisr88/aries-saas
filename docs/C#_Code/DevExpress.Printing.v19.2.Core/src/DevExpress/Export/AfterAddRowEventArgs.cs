namespace DevExpress.Export
{
    using DevExpress.Printing.ExportHelpers;
    using System;
    using System.Runtime.CompilerServices;

    public class AfterAddRowEventArgs : DataAwareEventArgsBase
    {
        public IExportContext ExportContext { get; set; }
    }
}

