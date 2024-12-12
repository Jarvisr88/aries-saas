namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using System;
    using System.Xml;

    public class DefinedNamesDestination : ElementDestination<XlsxSpreadsheetSourceImporter>
    {
        private static readonly ElementHandlerTable<XlsxSpreadsheetSourceImporter> handlerTable = CreateElementHandlerTable();

        public DefinedNamesDestination(XlsxSpreadsheetSourceImporter importer) : base(importer)
        {
        }

        private static ElementHandlerTable<XlsxSpreadsheetSourceImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<XlsxSpreadsheetSourceImporter> table = new ElementHandlerTable<XlsxSpreadsheetSourceImporter>();
            table.Add("definedName", new ElementHandler<XlsxSpreadsheetSourceImporter>(DefinedNamesDestination.OnDefinedName));
            return table;
        }

        private static Destination OnDefinedName(XlsxSpreadsheetSourceImporter importer, XmlReader reader) => 
            new DefinedNameDestination(importer);

        protected internal override ElementHandlerTable<XlsxSpreadsheetSourceImporter> ElementHandlerTable =>
            handlerTable;
    }
}

