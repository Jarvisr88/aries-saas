namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;

    public class AlphaInverseEffectDestination : DrawingColorEffectDestinationBase
    {
        public AlphaInverseEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override void CheckPropertyValues()
        {
        }

        protected override IDrawingEffect CreateEffect() => 
            new AlphaInverseEffect(this.Color);
    }
}

