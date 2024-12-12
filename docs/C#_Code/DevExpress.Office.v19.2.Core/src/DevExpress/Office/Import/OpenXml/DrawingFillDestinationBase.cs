namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public abstract class DrawingFillDestinationBase : DrawingFillPartDestinationBase
    {
        protected DrawingFillDestinationBase(DestinationAndXmlBasedImporter importer) : base(importer)
        {
        }

        protected static void AddFillHandlers(ElementHandlerTable<DestinationAndXmlBasedImporter> table)
        {
            table.Add("blipFill", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingFillDestinationBase.OnPictureFill));
            table.Add("grpFill", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingFillDestinationBase.OnGroupFill));
            AddFillPartHandlers(table);
        }

        private static DrawingFillDestinationBase GetThis(DestinationAndXmlBasedImporter importer) => 
            (DrawingFillDestinationBase) importer.PeekDestination();

        private static Destination OnGroupFill(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            GetThis(importer).Fill = DrawingFill.Group;
            return new EmptyDestination<DestinationAndXmlBasedImporter>(importer);
        }

        private static Destination OnPictureFill(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            DrawingBlipFill fill = new DrawingBlipFill(importer.ActualDocumentModel);
            GetThis(importer).Fill = fill;
            return new DrawingBlipFillDestination(importer, fill);
        }
    }
}

