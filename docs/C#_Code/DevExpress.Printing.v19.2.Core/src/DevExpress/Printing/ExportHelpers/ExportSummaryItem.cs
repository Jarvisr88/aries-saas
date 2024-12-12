namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    public class ExportSummaryItem
    {
        public XlSummary SummaryType { get; set; }

        public string FieldName { get; set; }

        public string DisplayFormat { get; set; }

        internal bool AlignByColumnInFooter { get; set; }
    }
}

