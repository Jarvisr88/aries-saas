namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using System;
    using System.Xml;

    public class NumberFormatsDestination : ElementDestination<XlsxSpreadsheetSourceImporter>
    {
        private static readonly ElementHandlerTable<XlsxSpreadsheetSourceImporter> handlerTable = CreateElementHandlerTable();

        public NumberFormatsDestination(XlsxSpreadsheetSourceImporter importer) : base(importer)
        {
        }

        private static ElementHandlerTable<XlsxSpreadsheetSourceImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<XlsxSpreadsheetSourceImporter> table = new ElementHandlerTable<XlsxSpreadsheetSourceImporter>();
            table.Add("numFmt", new ElementHandler<XlsxSpreadsheetSourceImporter>(NumberFormatsDestination.OnNumberFormat));
            return table;
        }

        private static Destination OnNumberFormat(XlsxSpreadsheetSourceImporter importer, XmlReader reader) => 
            new NumberFormatDestination(importer);

        protected internal override ElementHandlerTable<XlsxSpreadsheetSourceImporter> ElementHandlerTable =>
            handlerTable;
    }
}

