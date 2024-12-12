namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class DrawingGradientStopListDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly DrawingGradientFill fill;

        public DrawingGradientStopListDestination(DestinationAndXmlBasedImporter importer, DrawingGradientFill fill) : base(importer)
        {
            this.fill = fill;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("gs", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingGradientStopListDestination.OnGradientStop));
            return table;
        }

        private static DrawingGradientStopListDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (DrawingGradientStopListDestination) importer.PeekDestination();

        private static Destination OnGradientStop(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            DrawingGradientFill fill = GetThis(importer).fill;
            DrawingGradientStop stop = new DrawingGradientStop(fill.DocumentModel);
            fill.AddGradientStop(stop);
            return new DrawingGradientStopDestination(importer, stop);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

