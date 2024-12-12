namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;

    public class GrayScaleEffectDestination : DrawingEffectDestinationBase
    {
        public GrayScaleEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override IDrawingEffect CreateEffect() => 
            DrawingEffect.GrayScaleEffect;
    }
}

