namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class GlowEffectDestination : DrawingColorEffectDestinationBase
    {
        private long radius;

        public GlowEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override void CheckPropertyValues()
        {
            base.CheckPropertyValues();
            DrawingValueChecker.CheckPositiveCoordinate(this.radius, "radius");
        }

        protected override IDrawingEffect CreateEffect() => 
            new GlowEffect(this.Color, this.radius);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.radius = this.Importer.GetLongValue(reader, "rad", 0L);
        }
    }
}

