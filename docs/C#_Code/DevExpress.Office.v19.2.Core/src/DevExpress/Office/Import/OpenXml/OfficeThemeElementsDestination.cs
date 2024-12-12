namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using System;
    using System.Xml;

    public class OfficeThemeElementsDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();

        public OfficeThemeElementsDestination(DestinationAndXmlBasedImporter importer) : base(importer)
        {
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("clrScheme", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeElementsDestination.OnColorScheme));
            table.Add("fontScheme", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeElementsDestination.OnFontScheme));
            table.Add("fmtScheme", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeElementsDestination.OnFormatScheme));
            return table;
        }

        private static Destination OnColorScheme(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeColorSchemeDestination(importer);

        private static Destination OnFontScheme(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeFontSchemeDestination(importer);

        private static Destination OnFormatScheme(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeFormatSchemeDestination(importer);

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

