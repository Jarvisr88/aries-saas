namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class ColorChangeEffectDestination : DrawingColorEffectDestinationBase
    {
        private static readonly ElementHandlerTable<DestinationAndXmlBasedImporter> handlerTable = CreateElementHandlerTable();
        private DrawingColor colorTo;
        private bool useAlpha;

        public ColorChangeEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override IDrawingEffect CreateEffect()
        {
            if (!this.HasColor)
            {
                this.Importer.ThrowInvalidFile();
            }
            return new ColorChangeEffect(this.Color, this.colorTo, this.useAlpha);
        }

        private static ElementHandlerTable<DestinationAndXmlBasedImporter> CreateElementHandlerTable()
        {
            ElementHandlerTable<DestinationAndXmlBasedImporter> table = new ElementHandlerTable<DestinationAndXmlBasedImporter>();
            table.Add("clrFrom", new ElementHandler<DestinationAndXmlBasedImporter>(ColorChangeEffectDestination.OnColorFrom));
            table.Add("clrTo", new ElementHandler<DestinationAndXmlBasedImporter>(ColorChangeEffectDestination.OnColorTo));
            return table;
        }

        private static ColorChangeEffectDestination GetThis(DestinationAndXmlBasedImporter importer) => 
            (ColorChangeEffectDestination) importer.PeekDestination();

        private static Destination OnColorFrom(DestinationAndXmlBasedImporter importer, XmlReader reader) => 
            new DrawingColorDestination(importer, GetColor(importer));

        private static Destination OnColorTo(DestinationAndXmlBasedImporter importer, XmlReader reader)
        {
            ColorChangeEffectDestination @this = GetThis(importer);
            DrawingColor color = new DrawingColor(@this.Effects.DocumentModel);
            @this.colorTo = color;
            return new DrawingColorDestination(importer, color);
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.useAlpha = this.Importer.GetOnOffValue(reader, "useA", true);
        }

        protected override ElementHandlerTable<DestinationAndXmlBasedImporter> ElementHandlerTable =>
            handlerTable;
    }
}

