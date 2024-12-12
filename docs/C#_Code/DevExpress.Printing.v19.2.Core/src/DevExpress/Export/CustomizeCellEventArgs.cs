namespace DevExpress.Export
{
    using DevExpress.Printing.ExportHelpers;
    using System;
    using System.Runtime.CompilerServices;

    public class CustomizeCellEventArgs : CustomizeCellEventArgsBase
    {
        public object Value { get; set; }

        public string Hyperlink { get; set; }

        public ExportSummaryItem SummaryItem { get; set; }
    }
}

