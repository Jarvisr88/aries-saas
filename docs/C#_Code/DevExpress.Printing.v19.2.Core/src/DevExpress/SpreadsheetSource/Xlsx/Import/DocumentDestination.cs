namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using System;
    using System.Xml;

    public class DocumentDestination : ElementDestination<XlsxSpreadsheetSourceImporter>
    {
        private static readonly ElementHandlerTable<XlsxSpreadsheetSourceImporter> handlerTable = CreateElementHandlerTable();

        public DocumentDestination(XlsxSpreadsheetSourceImporter importer) : base(importer)
        {
        }

        private static ElementHandlerTable<XlsxSpreadsheetSourceImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<XlsxSpreadsheetSourceImporter> table = new ElementHandlerTable<XlsxSpreadsheetSourceImporter>();
            table.Add("sheets", new ElementHandler<XlsxSpreadsheetSourceImporter>(DocumentDestination.OnSheets));
            table.Add("definedNames", new ElementHandler<XlsxSpreadsheetSourceImporter>(DocumentDestination.OnDefinedNames));
            table.Add("workbookPr", new ElementHandler<XlsxSpreadsheetSourceImporter>(DocumentDestination.OnWorkbookProperties));
            return table;
        }

        private static Destination OnDefinedNames(XlsxSpreadsheetSourceImporter importer, XmlReader reader) => 
            new DefinedNamesDestination(importer);

        private static Destination OnSheets(XlsxSpreadsheetSourceImporter importer, XmlReader reader) => 
            new SheetsDestination(importer);

        private static Destination OnWorkbookProperties(XlsxSpreadsheetSourceImporter importer, XmlReader reader) => 
            new WorkbookPropertiesDestination(importer);

        protected internal override ElementHandlerTable<XlsxSpreadsheetSourceImporter> ElementHandlerTable =>
            handlerTable;
    }
}

