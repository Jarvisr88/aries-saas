namespace DevExpress.SpreadsheetSource.Implementation
{
    using DevExpress.Export.Xl;
    using DevExpress.SpreadsheetSource;
    using System;
    using System.Runtime.CompilerServices;

    public class Worksheet : IWorksheet
    {
        public Worksheet(string name, XlSheetVisibleState visibleState)
        {
            this.Name = name;
            this.VisibleState = visibleState;
        }

        public string Name { get; private set; }

        public XlSheetVisibleState VisibleState { get; private set; }
    }
}

