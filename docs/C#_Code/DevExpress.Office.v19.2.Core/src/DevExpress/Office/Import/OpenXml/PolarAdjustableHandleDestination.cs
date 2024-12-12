namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class PolarAdjustableHandleDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly PolarAdjustHandle polarAdjustHandle;
        private readonly AdjustableCoordinateCache adjustableCoordinateCache;
        private readonly AdjustableAngleCache adjustableAngleCache;

        public PolarAdjustableHandleDestination(DestinationAndXmlBasedImporter importer, PolarAdjustHandle polarAdjustHandle, AdjustableCoordinateCache adjustableCoordinateCache, AdjustableAngleCache adjustableAngleCache) : base(importer)
        {
            this.polarAdjustHandle = polarAdjustHandle;
            this.adjustableCoordinateCache = adjustableCoordinateCache;
            this.adjustableAngleCache = adjustableAngleCache;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("pos", new ElementHandler<DestinationAndXmlBasedImporter>(PolarAdjustableHandleDestination.OnAdjustPoint2D));
            return table;
        }

        private static PolarAdjustableHandleDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (PolarAdjustableHandleDestination) importer.PeekDestination();

        private static Destination OnAdjustPoint2D(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new AdjustPoint2DDestination(importer, GetThis(importer).polarAdjustHandle, GetThis(importer).adjustableCoordinateCache);

        public override void ProcessElementClose(XmlReader reader)
        {
            if ((this.polarAdjustHandle.X == null) || (this.polarAdjustHandle.Y == null))
            {
                this.Importer.ThrowInvalidFile();
            }
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.polarAdjustHandle.RadialGuide = reader.GetAttribute("gdRefR");
            this.polarAdjustHandle.AngleGuide = reader.GetAttribute("gdRefAng");
            this.polarAdjustHandle.MinimumRadial = this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(reader.GetAttribute("minR"));
            this.polarAdjustHandle.MaximumRadial = this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(reader.GetAttribute("maxR"));
            this.polarAdjustHandle.MinimumAngle = this.adjustableAngleCache.GetCachedAdjustableAngle(reader.GetAttribute("minAng"));
            this.polarAdjustHandle.MaximumAngle = this.adjustableAngleCache.GetCachedAdjustableAngle(reader.GetAttribute("maxAng"));
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

