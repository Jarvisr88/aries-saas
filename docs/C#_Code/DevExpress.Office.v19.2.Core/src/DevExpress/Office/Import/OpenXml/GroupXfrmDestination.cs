namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class GroupXfrmDestination : XfrmDestination
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly Transform2D childTransform;

        public GroupXfrmDestination(DestinationAndXmlBasedImporter importer, Transform2D mainTransform, Transform2D childTransform) : base(importer, mainTransform)
        {
            this.childTransform = childTransform;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> result = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            result.Add("chExt", new ElementHandler<DestinationAndXmlBasedImporter>(GroupXfrmDestination.OnChildExtents));
            result.Add("chOff", new ElementHandler<DestinationAndXmlBasedImporter>(GroupXfrmDestination.OnChildOffset));
            AddCommonHandlers(result);
            return result;
        }

        private static GroupXfrmDestination GetThisGroupXlfrm(DestinationAndXmlBasedImporter importer) => 
            (GroupXfrmDestination) importer.PeekDestination();

        private static Destination OnChildExtents(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ExtentsDestination(importer, GetThisGroupXlfrm(importer).childTransform);

        private static Destination OnChildOffset(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OffsetDestination(importer, GetThisGroupXlfrm(importer).childTransform);

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

