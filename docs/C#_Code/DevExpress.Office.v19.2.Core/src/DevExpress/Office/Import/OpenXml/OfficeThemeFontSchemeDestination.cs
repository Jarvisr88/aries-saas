namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class OfficeThemeFontSchemeDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();

        public OfficeThemeFontSchemeDestination(DestinationAndXmlBasedImporter importer) : base(importer)
        {
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("majorFont", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeFontSchemeDestination.OnMajorFont));
            table.Add("minorFont", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeFontSchemeDestination.OnMinorFont));
            return table;
        }

        private static OfficeThemeFontSchemeDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (OfficeThemeFontSchemeDestination) importer.PeekDestination();

        private static Destination OnMajorFont(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeFontCollectionSchemeDestination(importer, GetThis(importer).MajorFont);

        private static Destination OnMinorFont(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeFontCollectionSchemeDestination(importer, GetThis(importer).MinorFont);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.Importer.DocumentModel.OfficeTheme.FontScheme.Name = this.Importer.ReadAttribute(reader, "name");
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;

        protected internal ThemeFontSchemePart MajorFont =>
            this.Importer.DocumentModel.OfficeTheme.FontScheme.MajorFont;

        protected internal ThemeFontSchemePart MinorFont =>
            this.Importer.DocumentModel.OfficeTheme.FontScheme.MinorFont;
    }
}

