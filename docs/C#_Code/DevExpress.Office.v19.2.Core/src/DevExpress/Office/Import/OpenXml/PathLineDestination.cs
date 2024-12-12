namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class PathLineDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly PathLine pathLine;
        private readonly AdjustableCoordinateCache adjustableCoordinateCache;

        public PathLineDestination(DestinationAndXmlBasedImporter importer, PathLine pathLine, AdjustableCoordinateCache adjustableCoordinateCache) : base(importer)
        {
            this.pathLine = pathLine;
            this.adjustableCoordinateCache = adjustableCoordinateCache;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("pt", new ElementHandler<DestinationAndXmlBasedImporter>(PathLineDestination.OnPoint));
            return table;
        }

        private static PathLineDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (PathLineDestination) importer.PeekDestination();

        private static Destination OnPoint(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            GetThis(importer).pathLine.Point = new AdjustablePoint();
            return new AdjustPoint2DDestination(importer, GetThis(importer).pathLine.Point, GetThis(importer).adjustableCoordinateCache);
        }

        public override void ProcessElementClose(XmlReader reader)
        {
            if (this.pathLine.Point == null)
            {
                this.Importer.ThrowInvalidFile();
            }
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

