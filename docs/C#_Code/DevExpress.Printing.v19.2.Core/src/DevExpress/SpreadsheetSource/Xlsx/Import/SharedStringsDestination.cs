namespace DevExpress.SpreadsheetSource.Xlsx.Import
{
    using DevExpress.Office;
    using System;
    using System.Xml;

    public class SharedStringsDestination : ElementDestination<XlsxSpreadsheetSourceImporter>
    {
        private static readonly ElementHandlerTable<XlsxSpreadsheetSourceImporter> handlerTable = CreateElementHandlerTable();

        public SharedStringsDestination(XlsxSpreadsheetSourceImporter importer) : base(importer)
        {
        }

        private static ElementHandlerTable<XlsxSpreadsheetSourceImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<XlsxSpreadsheetSourceImporter> table = new ElementHandlerTable<XlsxSpreadsheetSourceImporter>();
            table.Add("si", new ElementHandler<XlsxSpreadsheetSourceImporter>(SharedStringsDestination.OnSharedString));
            return table;
        }

        private static Destination OnSharedString(XlsxSpreadsheetSourceImporter importer, XmlReader reader) => 
            SharedStringDestination.GetInstance(importer);

        public override void ProcessElementClose(XmlReader reader)
        {
            SharedStringDestination.ClearInstance();
        }

        protected internal override ElementHandlerTable<XlsxSpreadsheetSourceImporter> ElementHandlerTable =>
            handlerTable;
    }
}

