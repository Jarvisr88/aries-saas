namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class OfficeThemeLineStyleListDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();

        public OfficeThemeLineStyleListDestination(DestinationAndXmlBasedImporter importer) : base(importer)
        {
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("ln", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeLineStyleListDestination.OnLineStyle));
            return table;
        }

        private static Destination OnLineStyle(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            Outline item = new Outline(importer.ActualDocumentModel);
            importer.DocumentModel.OfficeTheme.FormatScheme.LineStyleList.Add(item);
            return new OutlineDestination(importer, item);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

