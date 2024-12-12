namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;

    public class AlphaFloorEffectDestination : DrawingEffectDestinationBase
    {
        public AlphaFloorEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override IDrawingEffect CreateEffect() => 
            DrawingEffect.AlphaFloorEffect;
    }
}

