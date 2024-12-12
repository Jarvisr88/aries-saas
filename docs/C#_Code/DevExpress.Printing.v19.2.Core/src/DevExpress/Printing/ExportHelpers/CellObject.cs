namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export;
    using System;
    using System.Runtime.CompilerServices;

    public class CellObject
    {
        public object Value { get; set; }

        public XlFormattingObject Formatting { get; set; }

        public string Hyperlink { get; set; }
    }
}

