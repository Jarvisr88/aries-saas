namespace DevExpress.Office.Import.OpenXml
{
    using DevExpress.Office;
    using DevExpress.Office.Drawing;
    using System;

    public class BiLevelEffectDestination : AlphaBiLevelEffectDestination
    {
        public BiLevelEffectDestination(DestinationAndXmlBasedImporter importer, DrawingEffectCollection effects) : base(importer, effects)
        {
        }

        protected override IDrawingEffect CreateEffect() => 
            new BiLevelEffect(base.Thresh);
    }
}

