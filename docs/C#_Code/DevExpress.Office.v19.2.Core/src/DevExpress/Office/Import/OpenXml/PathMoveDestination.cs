namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class PathMoveDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly PathMove pathMove;
        private readonly AdjustableCoordinateCache adjustableCoordinateCache;

        public PathMoveDestination(DestinationAndXmlBasedImporter importer, PathMove pathMove, AdjustableCoordinateCache adjustableCoordinateCache) : base(importer)
        {
            this.pathMove = pathMove;
            this.adjustableCoordinateCache = adjustableCoordinateCache;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("pt", new ElementHandler<DestinationAndXmlBasedImporter>(PathMoveDestination.OnPoint));
            return table;
        }

        private static PathMoveDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (PathMoveDestination) importer.PeekDestination();

        private static Destination OnPoint(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            GetThis(importer).pathMove.Point = new AdjustablePoint();
            return new AdjustPoint2DDestination(importer, GetThis(importer).pathMove.Point, GetThis(importer).adjustableCoordinateCache);
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            if (this.pathMove.Point == null)
            {
                this.Importer.ThrowInvalidFile();
            }
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

