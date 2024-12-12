namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using System;
    using System.Xml;

    public class OfficeThemeDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private string name;

        public OfficeThemeDestination(DestinationAndXmlBasedImporter importer) : base(importer)
        {
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("themeElements", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeDestination.OnThemeElements));
            return table;
        }

        private static Destination OnThemeElements(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OfficeThemeElementsDestination(importer);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.name = this.Importer.ReadAttribute(reader, "name");
            if (!string.IsNullOrEmpty(this.name))
            {
                this.Importer.DocumentModel.OfficeTheme.Name = this.name;
            }
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

