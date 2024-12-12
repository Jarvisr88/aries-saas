namespace DevExpress.SpreadsheetSource.Xlsx
{
    using DevExpress.Export.Xl;
    using DevExpress.SpreadsheetSource.Implementation;
    using System;
    using System.Runtime.CompilerServices;

    public class XlsxWorksheet : Worksheet
    {
        public XlsxWorksheet(string name, XlSheetVisibleState visibleState, string relationId) : base(name, visibleState)
        {
            this.RelationId = relationId;
        }

        public string RelationId { get; private set; }
    }
}

