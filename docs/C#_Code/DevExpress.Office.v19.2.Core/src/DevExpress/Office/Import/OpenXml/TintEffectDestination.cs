namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class TintEffectDestination : DrawingEffectDestinationBase
    {
        private int hue;
        private int amount;

        public TintEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override void CheckPropertyValues()
        {
            DrawingValueChecker.CheckPositiveFixedAngle(this.hue, "hue");
            DrawingValueChecker.CheckFixedPercentage(this.amount, "amount");
        }

        protected override IDrawingEffect CreateEffect() => 
            new TintEffect(this.hue, this.amount);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.hue = this.Importer.GetIntegerValue(reader, "hue", 0);
            this.amount = this.Importer.GetIntegerValue(reader, "amt", 0);
        }
    }
}

