namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public abstract class DrawingColorDestinationBase : ElementDestination<DestinationAndXmlBasedImporter>
    {
        private readonly DrawingColor color;
        private bool hasColor;

        protected DrawingColorDestinationBase(DestinationAndXmlBasedImporter importer, DrawingColor color) : base(importer)
        {
            this.color = color;
        }

        protected static void AddDrawingColorHandlers(ElementHandlerTable<DestinationAndXmlBasedImporter> table)
        {
            table.Add("hslClr", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorDestinationBase.OnHSLColors));
            table.Add("prstClr", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorDestinationBase.OnPresetColors));
            table.Add("schemeClr", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorDestinationBase.OnSchemeColors));
            table.Add("scrgbClr", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorDestinationBase.OnPercentageRGBColors));
            table.Add("srgbClr", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorDestinationBase.OnHexRGBColors));
            table.Add("sysClr", new ElementHandler<DestinationAndXmlBasedImporter>(DrawingColorDestinationBase.OnSystemColors));
        }

        protected static DrawingColor GetColor(DestinationAndXmlBasedImporter importer)
        {
            DrawingColorDestinationBase @this = GetThis(importer);
            @this.HasColor = true;
            return @this.Color;
        }

        private static DrawingColorDestinationBase GetThis(DestinationAndXmlBasedImporter importer) => 
            (DrawingColorDestinationBase) importer.PeekDestination();

        private static Destination OnHexRGBColors(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new HexRGBColorDestination(importer, GetColor(importer));

        private static Destination OnHSLColors(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new HSLColorDestination(importer, GetColor(importer));

        private static Destination OnPercentageRGBColors(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new PercentageRGBColorDestination(importer, GetColor(importer));

        private static Destination OnPresetColors(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new PresetColorDestination(importer, GetColor(importer));

        private static Destination OnSchemeColors(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new SchemeColorDestination(importer, GetColor(importer));

        private static Destination OnSystemColors(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new SystemColorDestination(importer, GetColor(importer));

        protected virtual DrawingColor Color =>
            this.color;

        protected virtual bool HasColor
        {
            get => 
                this.hasColor;
            set => 
                this.hasColor = value;
        }
    }
}

