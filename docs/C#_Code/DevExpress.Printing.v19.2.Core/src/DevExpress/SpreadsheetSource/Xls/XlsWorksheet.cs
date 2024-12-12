namespace DevExpress.SpreadsheetSource.Xls
{
    using DevExpress.Export.Xl;
    using DevExpress.SpreadsheetSource.Implementation;
    using System;
    using System.Runtime.CompilerServices;

    internal class XlsWorksheet : Worksheet
    {
        public XlsWorksheet(string name, XlSheetVisibleState visibleState, int startPosition) : base(name, visibleState)
        {
            this.StartPosition = startPosition;
        }

        public int StartPosition { get; private set; }
    }
}

