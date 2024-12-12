namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class OfficeThemeFillStyleListDestination : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private readonly List<IDrawingFill> list;

        public OfficeThemeFillStyleListDestination(DestinationAndXmlBasedImporter importer, List<IDrawingFill> list) : base(importer)
        {
            Guard.ArgumentNotNull(list, "list");
            this.list = list;
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("gradFill", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeFillStyleListDestination.OnGradientFill));
            table.Add("solidFill", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeFillStyleListDestination.OnSolidFill));
            table.Add("pattFill", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeFillStyleListDestination.OnPatternFill));
            table.Add("noFill", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeFillStyleListDestination.OnNoFill));
            table.Add("blipFill", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeFillStyleListDestination.OnPictureFill));
            table.Add("grpFill", new ElementHandler<DestinationAndXmlBasedImporter>(OfficeThemeFillStyleListDestination.OnGroupFill));
            return table;
        }

        private static T GetFill<T>(DestinationAndXmlBasedImporter importer, T fill) where T: IDrawingFill
        {
            ((OfficeThemeFillStyleListDestination) importer.PeekDestination()).list.Add(fill);
            return fill;
        }

        private static Destination OnGradientFill(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingGradientFillDestination(importer, GetFill<DrawingGradientFill>(importer, new DrawingGradientFill(importer.ActualDocumentModel)));

        private static Destination OnGroupFill(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            GetFill<DrawingFill>(importer, DrawingFill.Group);
            return new EmptyDestination<DestinationAndXmlBasedImporter>(importer);
        }

        private static Destination OnNoFill(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            GetFill<DrawingFill>(importer, DrawingFill.None);
            return new EmptyDestination<DestinationAndXmlBasedImporter>(importer);
        }

        private static Destination OnPatternFill(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingPatternFillDestination(importer, GetFill<DrawingPatternFill>(importer, new DrawingPatternFill(importer.ActualDocumentModel)));

        private static Destination OnPictureFill(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingBlipFillDestination(importer, GetFill<DrawingBlipFill>(importer, new DrawingBlipFill(importer.ActualDocumentModel)));

        private static Destination OnSolidFill(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            DrawingSolidFill fill = GetFill<DrawingSolidFill>(importer, new DrawingSolidFill(importer.ActualDocumentModel));
            return new DrawingColorDestination(importer, fill.Color);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

