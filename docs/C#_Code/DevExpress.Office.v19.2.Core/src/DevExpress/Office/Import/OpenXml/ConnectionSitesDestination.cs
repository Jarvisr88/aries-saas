namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class ConnectionSitesDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly ModelShapeConnectionList modelShapeConnectionList;
        private readonly AdjustableCoordinateCache adjustableCoordinateCache;
        private readonly AdjustableAngleCache adjustableAngleCache;

        public ConnectionSitesDestination(DestinationAndXmlBasedImporter importer, ModelShapeConnectionList modelShapeConnectionList, AdjustableCoordinateCache adjustableCoordinateCache, AdjustableAngleCache adjustableAngleCache) : base(importer)
        {
            this.modelShapeConnectionList = modelShapeConnectionList;
            this.adjustableCoordinateCache = adjustableCoordinateCache;
            this.adjustableAngleCache = adjustableAngleCache;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("cxn", new ElementHandler<DestinationAndXmlBasedImporter>(ConnectionSitesDestination.OnShapeConnection));
            return table;
        }

        private static ConnectionSitesDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (ConnectionSitesDestination) importer.PeekDestination();

        private static Destination OnShapeConnection(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            ModelShapeConnection item = new ModelShapeConnection();
            GetThis(importer).modelShapeConnectionList.Add(item);
            return new ModelShapeConnectionDestination(importer, item, GetThis(importer).adjustableCoordinateCache, GetThis(importer).adjustableAngleCache);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

