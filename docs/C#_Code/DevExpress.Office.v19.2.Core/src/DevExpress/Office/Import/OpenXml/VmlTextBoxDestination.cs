namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;
    using System.Xml;

    public class VmlTextBoxDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly Destination textBoxContentDestination;

        public VmlTextBoxDestination(DestinationAndXmlBasedImporter importer, Destination textBoxContentDestination) : base(importer)
        {
            Guard.ArgumentNotNull(textBoxContentDestination, "textBoxContentDestination");
            this.textBoxContentDestination = textBoxContentDestination;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("txbxContent", new ElementHandler<DestinationAndXmlBasedImporter>(VmlTextBoxDestination.OnTextBoxContent));
            return table;
        }

        private static VmlTextBoxDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (VmlTextBoxDestination) importer.PeekDestination();

        private static Destination OnTextBoxContent(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            GetThis(importer).textBoxContentDestination;

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

