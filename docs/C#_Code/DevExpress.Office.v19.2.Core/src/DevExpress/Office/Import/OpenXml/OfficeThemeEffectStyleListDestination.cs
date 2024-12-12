namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class OfficeThemeEffectStyleListDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();

        public OfficeThemeEffectStyleListDestination(DestinationAndXmlBasedImporter importer) : base(importer)
        {
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("effectStyle", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeEffectStyleListDestination.OnEffectStyle));
            return table;
        }

        private static Destination OnEffectStyle(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            DrawingEffectStyle item = new DrawingEffectStyle(importer.ActualDocumentModel);
            importer.DocumentModel.OfficeTheme.FormatScheme.EffectStyleList.Add(item);
            return new OfficeThemeEffectStyleDestination(importer, item);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

