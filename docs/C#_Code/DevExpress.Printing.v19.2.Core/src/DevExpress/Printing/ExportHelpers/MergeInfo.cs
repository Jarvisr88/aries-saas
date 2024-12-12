namespace DevExpress.Printing.ExportHelpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    internal class MergeInfo
    {
        public int RowHandle { get; set; }

        public int ExportRowIndex { get; set; }

        public XlVariantValue Value { get; set; }
    }
}

