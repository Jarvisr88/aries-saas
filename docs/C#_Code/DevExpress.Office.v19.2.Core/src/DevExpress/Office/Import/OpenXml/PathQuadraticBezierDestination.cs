namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class PathQuadraticBezierDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly PathQuadraticBezier pathQuadraticBezier;
        private readonly AdjustableCoordinateCache adjustableCoordinateCache;

        public PathQuadraticBezierDestination(DestinationAndXmlBasedImporter importer, PathQuadraticBezier pathQuadraticBezier, AdjustableCoordinateCache adjustableCoordinateCache) : base(importer)
        {
            this.pathQuadraticBezier = pathQuadraticBezier;
            this.adjustableCoordinateCache = adjustableCoordinateCache;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("pt", new ElementHandler<DestinationAndXmlBasedImporter>(PathQuadraticBezierDestination.OnPoint));
            return table;
        }

        private static PathQuadraticBezierDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (PathQuadraticBezierDestination) importer.PeekDestination();

        private static Destination OnPoint(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            AdjustablePoint item = new AdjustablePoint();
            GetThis(importer).pathQuadraticBezier.Points.Add(item);
            return new AdjustPoint2DDestination(importer, item, GetThis(importer).adjustableCoordinateCache);
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            if (this.pathQuadraticBezier.Points.Count != 2)
            {
                this.Importer.ThrowInvalidFile();
            }
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

