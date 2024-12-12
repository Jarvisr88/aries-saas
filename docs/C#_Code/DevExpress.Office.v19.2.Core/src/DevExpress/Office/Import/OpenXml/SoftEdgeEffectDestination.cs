namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class SoftEdgeEffectDestination : DrawingEffectDestinationBase
    {
        private long radius;

        public SoftEdgeEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override void CheckPropertyValues()
        {
            DrawingValueChecker.CheckPositiveCoordinate(this.radius, "radius");
        }

        protected override IDrawingEffect CreateEffect() => 
            new SoftEdgeEffect(this.radius);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.radius = this.Importer.GetLongValue(reader, "rad", -9223372036854775808L);
        }
    }
}

