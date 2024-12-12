namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public abstract class DrawingFillPartDestinationBase : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private IDrawingFill fill;

        protected DrawingFillPartDestinationBase(DestinationAndXmlBasedImporter importer) : base(importer)
        {
        }

        protected static void AddFillPartHandlers(ElementHandlerTable<DestinationAndXmlBasedImporter> table)
        {
            table.Add("gradFill", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingFillPartDestinationBase.OnGradientFill));
            table.Add("solidFill", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingFillPartDestinationBase.OnSolidFill));
            table.Add("pattFill", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingFillPartDestinationBase.OnPatternFill));
            table.Add("noFill", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingFillPartDestinationBase.OnNoFill));
        }

        protected virtual DrawingGradientFill CreateGradientFill() => 
            new DrawingGradientFill(this.Importer.ActualDocumentModel);

        private static DrawingFillPartDestinationBase GetThis(DestinationAndXmlBasedImporter importer) => 
            (DrawingFillPartDestinationBase) importer.PeekDestination();

        private static Destination OnGradientFill(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            DrawingFillPartDestinationBase @this = GetThis(importer);
            DrawingGradientFill fill = @this.CreateGradientFill();
            @this.fill = fill;
            return new DrawingGradientFillDestination(importer, fill);
        }

        private static Destination OnNoFill(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            GetThis(importer).fill = DrawingFill.None;
            return new EmptyDestination<DestinationAndXmlBasedImporter>(importer);
        }

        private static Destination OnPatternFill(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            DrawingPatternFill patternFill = new DrawingPatternFill(importer.ActualDocumentModel);
            GetThis(importer).fill = patternFill;
            return new DrawingPatternFillDestination(importer, patternFill);
        }

        private static Destination OnSolidFill(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            DrawingSolidFill fill = new DrawingSolidFill(importer.ActualDocumentModel);
            GetThis(importer).fill = fill;
            return new DrawingColorDestination(importer, fill.Color);
        }

        protected internal IDrawingFill Fill
        {
            get => 
                this.fill;
            set => 
                this.fill = value;
        }
    }
}

