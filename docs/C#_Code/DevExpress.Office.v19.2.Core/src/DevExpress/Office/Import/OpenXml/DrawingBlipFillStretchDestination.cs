namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class DrawingBlipFillStretchDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly DrawingBlipFill fill;

        public DrawingBlipFillStretchDestination(DestinationAndXmlBasedImporter importer, DrawingBlipFill fill) : base(importer)
        {
            this.fill = fill;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("fillRect", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingBlipFillStretchDestination.OnFillRectangle));
            return table;
        }

        private static DrawingBlipFillStretchDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (DrawingBlipFillStretchDestination) importer.PeekDestination();

        private static Destination OnFillRectangle(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            DrawingBlipFill fill = GetThis(importer).fill;
            return new RelativeRectangleDestination(importer, delegate (RectangleOffset value) {
                fill.FillRectangle = value;
            });
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.fill.Stretch = true;
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

