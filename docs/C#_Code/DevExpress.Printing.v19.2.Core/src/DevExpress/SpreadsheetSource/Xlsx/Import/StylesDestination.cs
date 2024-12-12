namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using System;
    using System.Xml;

    public class StylesDestination : ElementDestination<XlsxSpreadsheetSourceImporter>
    {
        private static readonly ElementHandlerTable<XlsxSpreadsheetSourceImporter> handlerTable = CreateElementHandlerTable();

        public StylesDestination(XlsxSpreadsheetSourceImporter importer) : base(importer)
        {
        }

        private static ElementHandlerTable<XlsxSpreadsheetSourceImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<XlsxSpreadsheetSourceImporter> table = new ElementHandlerTable<XlsxSpreadsheetSourceImporter>();
            table.Add("numFmts", new ElementHandler<XlsxSpreadsheetSourceImporter>(StylesDestination.OnNumberFormats));
            table.Add("cellXfs", new ElementHandler<XlsxSpreadsheetSourceImporter>(StylesDestination.OnCellFormats));
            return table;
        }

        private static Destination OnCellFormats(XlsxSpreadsheetSourceImporter importer, XmlReader reader) => 
            new CellFormatsDestination(importer);

        private static Destination OnNumberFormats(XlsxSpreadsheetSourceImporter importer, XmlReader reader) => 
            new NumberFormatsDestination(importer);

        protected internal override ElementHandlerTable<XlsxSpreadsheetSourceImporter> ElementHandlerTable =>
            handlerTable;
    }
}

