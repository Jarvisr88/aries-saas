namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;

    public class SolidColorReplacementDestination : DrawingColorEffectDestinationBase
    {
        public SolidColorReplacementDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override IDrawingEffect CreateEffect() => 
            new SolidColorReplacementEffect(this.Color);
    }
}

