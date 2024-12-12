namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class HSLEffectDestination : DrawingEffectDestinationBase
    {
        private int hue;
        private int saturation;
        private int luminance;

        public HSLEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override void CheckPropertyValues()
        {
            DrawingValueChecker.CheckPositiveFixedAngle(this.hue, "hue");
            DrawingValueChecker.CheckFixedPercentage(this.saturation, "saturation");
            DrawingValueChecker.CheckFixedPercentage(this.luminance, "luminance");
        }

        protected override IDrawingEffect CreateEffect() => 
            new HSLEffect(this.hue, this.saturation, this.luminance);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.hue = this.Importer.GetIntegerValue(reader, "hue", 0);
            this.saturation = this.Importer.GetIntegerValue(reader, "sat", 0);
            this.luminance = this.Importer.GetIntegerValue(reader, "lum", 0);
        }
    }
}

