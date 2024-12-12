namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using System;
    using System.Xml;

    public class OfficeThemeFormatSchemeDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();

        public OfficeThemeFormatSchemeDestination(DestinationAndXmlBasedImporter importer) : base(importer)
        {
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("bgFillStyleLst", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeFormatSchemeDestination.OnBackgroundFillStyleList));
            table.Add("fillStyleLst", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeFormatSchemeDestination.OnFillStyleList));
            table.Add("effectStyleLst", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeFormatSchemeDestination.OnEffectStyleList));
            table.Add("lnStyleLst", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeFormatSchemeDestination.OnLineStyleList));
            return table;
        }

        private static Destination OnBackgroundFillStyleList(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeFillStyleListDestination(importer, importer.DocumentModel.OfficeTheme.FormatScheme.BackgroundFillStyleList);

        private static Destination OnEffectStyleList(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeEffectStyleListDestination(importer);

        private static Destination OnFillStyleList(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeFillStyleListDestination(importer, importer.DocumentModel.OfficeTheme.FormatScheme.FillStyleList);

        private static Destination OnLineStyleList(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeLineStyleListDestination(importer);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.Importer.DocumentModel.OfficeTheme.FormatScheme.Name = this.Importer.ReadAttribute(reader, "name");
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

