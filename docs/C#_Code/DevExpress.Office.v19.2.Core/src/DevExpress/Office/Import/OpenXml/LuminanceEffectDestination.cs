namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class LuminanceEffectDestination : DrawingEffectDestinationBase
    {
        private int bright;
        private int contrast;

        public LuminanceEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override void CheckPropertyValues()
        {
            DrawingValueChecker.CheckFixedPercentage(this.bright, "bright");
            DrawingValueChecker.CheckFixedPercentage(this.contrast, "contrast");
        }

        protected override IDrawingEffect CreateEffect() => 
            new LuminanceEffect(this.bright, this.contrast);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.bright = this.Importer.GetIntegerValue(reader, "bright", 0);
            this.contrast = this.Importer.GetIntegerValue(reader, "contrast", 0);
        }
    }
}

