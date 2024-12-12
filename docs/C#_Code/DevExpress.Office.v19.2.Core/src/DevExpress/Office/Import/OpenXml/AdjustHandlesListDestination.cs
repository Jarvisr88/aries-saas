namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class AdjustHandlesListDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly ModelAdjustHandlesList adjustHandlesList;
        private readonly AdjustableCoordinateCache adjustableCoordinateCache;
        private readonly AdjustableAngleCache adjustableAngleCache;

        public AdjustHandlesListDestination(DestinationAndXmlBasedImporter importer, ModelAdjustHandlesList adjustHandlesList, AdjustableCoordinateCache adjustableCoordinateCache, AdjustableAngleCache adjustableAngleCache) : base(importer)
        {
            this.adjustHandlesList = adjustHandlesList;
            this.adjustableCoordinateCache = adjustableCoordinateCache;
            this.adjustableAngleCache = adjustableAngleCache;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("ahXY", new ElementHandler<DestinationAndXmlBasedImporter>(AdjustHandlesListDestination.OnXYAdjustableHandle));
            table.Add("ahPolar", new ElementHandler<DestinationAndXmlBasedImporter>(AdjustHandlesListDestination.OnPolarAdjustableHandle));
            return table;
        }

        private static AdjustHandlesListDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (AdjustHandlesListDestination) importer.PeekDestination();

        private static Destination OnPolarAdjustableHandle(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            PolarAdjustHandle item = new PolarAdjustHandle();
            GetThis(importer).adjustHandlesList.Add(item);
            return new PolarAdjustableHandleDestination(importer, item, GetThis(importer).adjustableCoordinateCache, GetThis(importer).adjustableAngleCache);
        }

        private static Destination OnXYAdjustableHandle(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            XYAdjustHandle item = new XYAdjustHandle();
            GetThis(importer).adjustHandlesList.Add(item);
            return new XYAdjustableHandleDestination(importer, item, GetThis(importer).adjustableCoordinateCache);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

