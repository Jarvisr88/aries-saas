namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class CustomGeometryDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly ModelShapeCustomGeometry shapeCustomGeometry;
        private readonly AdjustableCoordinateCache adjustableCoordinateCache;
        private readonly AdjustableAngleCache adjustableAngleCache;

        public CustomGeometryDestination(DestinationAndXmlBasedImporter importer, ModelShapeCustomGeometry shapeCustomGeometry) : base(importer)
        {
            this.shapeCustomGeometry = shapeCustomGeometry;
            this.adjustableCoordinateCache = new AdjustableCoordinateCache();
            this.adjustableAngleCache = new AdjustableAngleCache();
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("avLst", new ElementHandler<DestinationAndXmlBasedImporter>(CustomGeometryDestination.OnAdjustValuesList));
            table.Add("gdLst", new ElementHandler<DestinationAndXmlBasedImporter>(CustomGeometryDestination.OnGuidesList));
            table.Add("ahLst", new ElementHandler<DestinationAndXmlBasedImporter>(CustomGeometryDestination.OnAdjustHandlesList));
            table.Add("cxnLst", new ElementHandler<DestinationAndXmlBasedImporter>(CustomGeometryDestination.OnConnectionSitesList));
            table.Add("rect", new ElementHandler<DestinationAndXmlBasedImporter>(CustomGeometryDestination.OnShapeTextRectangle));
            table.Add("pathLst", new ElementHandler<DestinationAndXmlBasedImporter>(CustomGeometryDestination.OnShapePathsList));
            return table;
        }

        private static CustomGeometryDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (CustomGeometryDestination) importer.PeekDestination();

        private static Destination OnAdjustHandlesList(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new AdjustHandlesListDestination(importer, GetThis(importer).shapeCustomGeometry.AdjustHandles, GetThis(importer).adjustableCoordinateCache, GetThis(importer).adjustableAngleCache);

        private static Destination OnAdjustValuesList(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ModelShapeGuideListDestination(importer, GetThis(importer).shapeCustomGeometry.AdjustValues, GetThis(importer).adjustableCoordinateCache);

        private static Destination OnConnectionSitesList(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ConnectionSitesDestination(importer, GetThis(importer).shapeCustomGeometry.ConnectionSites, GetThis(importer).adjustableCoordinateCache, GetThis(importer).adjustableAngleCache);

        private static Destination OnGuidesList(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ModelShapeGuideListDestination(importer, GetThis(importer).shapeCustomGeometry.Guides, GetThis(importer).adjustableCoordinateCache);

        private static Destination OnShapePathsList(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ModelShapePathsDestination(importer, GetThis(importer).shapeCustomGeometry.Paths, GetThis(importer).adjustableCoordinateCache, GetThis(importer).adjustableAngleCache);

        private static Destination OnShapeTextRectangle(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new ShapeTextRectangleDestination(importer, GetThis(importer).shapeCustomGeometry.ShapeTextRectangle, GetThis(importer).adjustableCoordinateCache);

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

