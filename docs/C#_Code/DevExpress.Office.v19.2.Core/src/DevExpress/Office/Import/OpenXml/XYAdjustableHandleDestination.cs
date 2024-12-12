namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class XYAdjustableHandleDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly XYAdjustHandle xyAdjustHandle;
        private readonly AdjustableCoordinateCache adjustableCoordinateCache;

        public XYAdjustableHandleDestination(DestinationAndXmlBasedImporter importer, XYAdjustHandle xyAdjustHandle, AdjustableCoordinateCache adjustableCoordinateCache) : base(importer)
        {
            this.xyAdjustHandle = xyAdjustHandle;
            this.adjustableCoordinateCache = adjustableCoordinateCache;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("pos", new ElementHandler<DestinationAndXmlBasedImporter>(XYAdjustableHandleDestination.OnAdjustPoint2D));
            return table;
        }

        private static XYAdjustableHandleDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (XYAdjustableHandleDestination) importer.PeekDestination();

        private static Destination OnAdjustPoint2D(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new AdjustPoint2DDestination(importer, GetThis(importer).xyAdjustHandle, GetThis(importer).adjustableCoordinateCache);

        public override void ProcessElementClose(XmlReader reader)
        {
            if ((this.xyAdjustHandle.X == null) || (this.xyAdjustHandle.Y == null))
            {
                this.Importer.ThrowInvalidFile();
            }
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.xyAdjustHandle.HorizontalGuide = reader.GetAttribute("gdRefX");
            this.xyAdjustHandle.VerticalGuide = reader.GetAttribute("gdRefY");
            this.xyAdjustHandle.MinX = this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(reader.GetAttribute("minX"));
            this.xyAdjustHandle.MaxX = this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(reader.GetAttribute("maxX"));
            this.xyAdjustHandle.MinY = this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(reader.GetAttribute("minY"));
            this.xyAdjustHandle.MaxY = this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(reader.GetAttribute("maxY"));
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

