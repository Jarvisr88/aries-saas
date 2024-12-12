namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class AlphaOutsetEffectDestination : DrawingEffectDestinationBase
    {
        private long radius;

        public AlphaOutsetEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override void CheckPropertyValues()
        {
            DrawingValueChecker.CheckPositiveCoordinate(this.radius, "radius");
        }

        protected override IDrawingEffect CreateEffect() => 
            new AlphaOutsetEffect(this.radius);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.radius = this.Importer.GetLongValue(reader, "rad", 0L);
        }
    }
}

