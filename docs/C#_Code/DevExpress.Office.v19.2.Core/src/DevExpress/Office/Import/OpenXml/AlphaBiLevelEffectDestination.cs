namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;
    using System.Xml;

    public class AlphaBiLevelEffectDestination : DrawingEffectDestinationBase
    {
        private int thresh;

        public AlphaBiLevelEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override void CheckPropertyValues()
        {
            DrawingValueChecker.CheckPositiveFixedPercentage(this.thresh, "thresh");
        }

        protected override IDrawingEffect CreateEffect() => 
            new AlphaBiLevelEffect(this.thresh);

        public override void ProcessElementOpen(XmlReader reader)
        {
            this.thresh = this.Importer.GetIntegerValue(reader, "thresh", -2147483648);
        }

        protected int Thresh =>
            this.thresh;
    }
}

