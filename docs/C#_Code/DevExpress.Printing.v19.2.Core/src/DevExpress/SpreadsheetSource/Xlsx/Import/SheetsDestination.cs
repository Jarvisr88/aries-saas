namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using System;
    using System.Xml;

    public class SheetsDestination : ElementDestination<XlsxSpreadsheetSourceImporter>
    {
        private static readonly ElementHandlerTable<XlsxSpreadsheetSourceImporter> handlerTable = CreateElementHandlerTable();

        public SheetsDestination(XlsxSpreadsheetSourceImporter importer) : base(importer)
        {
        }

        private static ElementHandlerTable<XlsxSpreadsheetSourceImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<XlsxSpreadsheetSourceImporter> table = new ElementHandlerTable<XlsxSpreadsheetSourceImporter>();
            table.Add("sheet", new ElementHandler<XlsxSpreadsheetSourceImporter>(SheetsDestination.OnSheet));
            return table;
        }

        private static Destination OnSheet(XlsxSpreadsheetSourceImporter importer, XmlReader reader) => 
            new SheetDestination(importer);

        public override void ProcessElementClose(XmlReader reader)
        {
            if (this.Importer.Source.Worksheets.Count == 0)
            {
                this.Importer.ThrowInvalidFile("Sheets count is zero");
            }
        }

        protected internal override ElementHandlerTable<XlsxSpreadsheetSourceImporter> ElementHandlerTable =>
            handlerTable;
    }
}

