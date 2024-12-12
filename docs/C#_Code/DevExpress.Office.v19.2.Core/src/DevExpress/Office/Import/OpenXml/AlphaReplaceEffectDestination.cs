namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class AlphaReplaceEffectDestination : DrawingEffectDestinationBase
    {
        private int alpha;

        public AlphaReplaceEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override void CheckPropertyValues()
        {
            DrawingValueChecker.CheckPositiveFixedPercentage(this.alpha, "alpha");
        }

        protected override IDrawingEffect CreateEffect() => 
            new AlphaReplaceEffect(this.alpha);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.alpha = this.Importer.GetIntegerValue(reader, "a", -2147483648);
        }
    }
}

