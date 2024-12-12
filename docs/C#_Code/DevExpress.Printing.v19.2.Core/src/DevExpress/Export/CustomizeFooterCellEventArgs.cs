namespace DevExpress.Export
{
    using DevExpress.Printing.ExportHelpers;
    using System;
    using System.Runtime.CompilerServices;

    public class CustomizeFooterCellEventArgs : CustomizeCellEventArgsBase
    {
        public ExportSummaryItem SummaryItem { get; set; }
    }
}

