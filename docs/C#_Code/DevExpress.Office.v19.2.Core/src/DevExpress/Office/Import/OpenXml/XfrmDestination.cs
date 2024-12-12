namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class XfrmDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly Transform2D xfrm;

        public XfrmDestination(DestinationAndXmlBasedImporter importer, Transform2D xfrm) : base(importer)
        {
            this.xfrm = xfrm;
        }

        protected static void AddCommonHandlers(ElementHandlerTable<DestinationAndXmlBasedImporter> result)
        {
            result.Add("ext", new ElementHandler<DestinationAndXmlBasedImporter>(XfrmDestination.OnExtents));
            result.Add("off", new ElementHandler<DestinationAndXmlBasedImporter>(XfrmDestination.OnOffset));
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> result = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            AddCommonHandlers(result);
            return result;
        }

        private static XfrmDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (XfrmDestination) importer.PeekDestination();

        private static Destination OnExtents(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ExtentsDestination(importer, GetThis(importer).xfrm);

        private static Destination OnOffset(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new OffsetDestination(importer, GetThis(importer).xfrm);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.xfrm.Rotation = this.Importer.DocumentModel.UnitConverter.AdjAngleToModelUnits(this.Importer.GetWpSTIntegerValue(reader, "rot", 0));
            this.xfrm.FlipH = this.Importer.GetWpSTOnOffValue(reader, "flipH", false);
            this.xfrm.FlipV = this.Importer.GetWpSTOnOffValue(reader, "flipV", false);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

