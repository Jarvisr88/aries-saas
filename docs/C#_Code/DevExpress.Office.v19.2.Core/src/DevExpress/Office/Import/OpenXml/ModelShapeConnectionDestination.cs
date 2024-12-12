namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class ModelShapeConnectionDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly ModelShapeConnection modelShapeConnection;
        private readonly AdjustableCoordinateCache adjustableCoordinateCache;
        private readonly AdjustableAngleCache adjustableAngleCache;

        public ModelShapeConnectionDestination(DestinationAndXmlBasedImporter importer, ModelShapeConnection modelShapeConnection, AdjustableCoordinateCache adjustableCoordinateCache, AdjustableAngleCache adjustableAngleCache) : base(importer)
        {
            this.modelShapeConnection = modelShapeConnection;
            this.adjustableCoordinateCache = adjustableCoordinateCache;
            this.adjustableAngleCache = adjustableAngleCache;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("pos", new ElementHandler<DestinationAndXmlBasedImporter>(ModelShapeConnectionDestination.OnAdjustPoint2D));
            return table;
        }

        private static ModelShapeConnectionDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (ModelShapeConnectionDestination) importer.PeekDestination();

        private static Destination OnAdjustPoint2D(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new AdjustPoint2DDestination(importer, GetThis(importer).modelShapeConnection, GetThis(importer).adjustableCoordinateCache);

        public override void ProcessElementClose(XmlReader reader)
        {
            if ((this.modelShapeConnection.Angle == null) || ((this.modelShapeConnection.X == null) || (this.modelShapeConnection.Y == null)))
            {
                this.Importer.ThrowInvalidFile();
            }
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.modelShapeConnection.Angle = this.adjustableAngleCache.GetCachedAdjustableAngle(reader.GetAttribute("ang"));
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

