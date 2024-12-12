namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Export.Xl;
    using System;
    using System.Runtime.CompilerServices;

    internal class XlsDefinedNameInfo
    {
        public bool IsHidden { get; set; }

        public string Name { get; set; }

        public int SheetIndex { get; set; }

        public int ScopeSheetIndex { get; set; }

        public XlCellRange Range { get; set; }
    }
}

