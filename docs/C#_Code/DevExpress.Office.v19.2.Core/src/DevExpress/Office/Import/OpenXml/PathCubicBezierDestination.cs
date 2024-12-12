namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class PathCubicBezierDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly PathCubicBezier pathCubicBezier;
        private readonly AdjustableCoordinateCache adjustableCoordinateCache;

        public PathCubicBezierDestination(DestinationAndXmlBasedImporter importer, PathCubicBezier pathCubicBezier, AdjustableCoordinateCache adjustableCoordinateCache) : base(importer)
        {
            this.pathCubicBezier = pathCubicBezier;
            this.adjustableCoordinateCache = adjustableCoordinateCache;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("pt", new ElementHandler<DestinationAndXmlBasedImporter>(PathCubicBezierDestination.OnPoint));
            return table;
        }

        private static PathCubicBezierDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (PathCubicBezierDestination) importer.PeekDestination();

        private static Destination OnPoint(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            AdjustablePoint item = new AdjustablePoint();
            GetThis(importer).pathCubicBezier.Points.Add(item);
            return new AdjustPoint2DDestination(importer, item, GetThis(importer).adjustableCoordinateCache);
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            if (this.pathCubicBezier.Points.Count != 3)
            {
                this.Importer.ThrowInvalidFile();
            }
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

