namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class BlurEffectDestination : DrawingEffectDestinationBase
    {
        private bool glow;
        private long radius;

        public BlurEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override void CheckPropertyValues()
        {
            DrawingValueChecker.CheckPositiveCoordinate(this.radius, "radius");
        }

        protected override IDrawingEffect CreateEffect() => 
            new BlurEffect(this.radius, this.glow);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.glow = this.Importer.GetOnOffValue(reader, "grow", true);
            this.radius = this.Importer.GetLongValue(reader, "rad", 0L);
        }
    }
}

