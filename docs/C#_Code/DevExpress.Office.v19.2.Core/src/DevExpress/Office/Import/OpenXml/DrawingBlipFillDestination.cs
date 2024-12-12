namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class DrawingBlipFillDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly DrawingBlipFill fill;

        public DrawingBlipFillDestination(DestinationAndXmlBasedImporter importer, DrawingBlipFill fill) : base(importer)
        {
            this.fill = fill;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("blip", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipFillDestination.OnBlip));
            table.Add("srcRect", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipFillDestination.OnSourceRectangle));
            table.Add("stretch", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipFillDestination.OnStretch));
            table.Add("tile", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipFillDestination.OnTile));
            return table;
        }

        private static DrawingBlipFillDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (DrawingBlipFillDestination) importer.PeekDestination();

        private static Destination OnBlip(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingBlipDestination(importer, GetThis(importer).fill.Blip);

        private static Destination OnSourceRectangle(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            DrawingBlipFill fill = GetThis(importer).fill;
            return new RelativeRectangleDestination(importer, delegate (RectangleOffset value) {
                fill.SourceRectangle = value;
            });
        }

        private static Destination OnStretch(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingBlipFillStretchDestination(importer, GetThis(importer).fill);

        private static Destination OnTile(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingBlipFillTileDestination(importer, GetThis(importer).fill);

        public override void ProcessElementClose(XmlReader reader)
        {
            this.fill.EndUpdate();
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.fill.BeginUpdate();
            this.fill.Dpi = this.Importer.GetIntegerValue(reader, "dpi", 0);
            this.fill.RotateWithShape = this.Importer.GetWpSTOnOffValue(reader, "rotWithShape", true);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

